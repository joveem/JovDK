// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// third
using R3;

// from company
using JovDK.Animations.Tweening;
using JovDK.Core;
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

namespace JovDK.UI.Reactive
{
    /// <summary>
    /// Generic reactive scroll list of UI items (TItem) populated from a reactive state (Dictionary<Guid, TData>).
    /// - Observes a reactive getter (ReactiveProperty<Func<ReactiveProperty<Dictionary<Guid, TData>>>>).
    /// - When the getter changes, it disposes the previous subscription and subscribes to the new inner ReactiveProperty.
    /// - When the inner dictionary changes, it rebuilds the UI items using the prefab.
    /// Design goals: KISS, SOLID (binding injected via abstract BindItem), low mutation, clear lifecycle.
    /// </summary>
    public abstract partial class Reactive_ScrollList<TItem, TData> : MonoBehaviour
        where TItem : MonoBehaviour, IIdentifiable
    {
        [Space(5), Header("[ State ]"), Space(10)]
        bool _isFirstReset = true;
        bool _isFirstListReset = true;
        List<TItem> _currentListItems = new List<TItem>();
        Coroutine _resetScrollPositionCoroutine = null;

        // Local reactive getter. You may set .Value directly or mirror an external source.
        readonly ReactiveProperty<Func<ReactiveProperty<Dictionary<Guid, TData>>>> _baseListStateGetter =
            new ReactiveProperty<Func<ReactiveProperty<Dictionary<Guid, TData>>>>(null);
        public ReactiveProperty<Func<ReactiveProperty<Dictionary<Guid, TData>>>> BaseListStateGetter => _baseListStateGetter;

        // Optional binding to an external reactive getter source (when you want this list to mirror another RP).
        IDisposable _externalGetterSubscription;

        // Subscriptions
        IDisposable _getterSubscription;
        IDisposable _innerStateSubscription;

        [Space(5), Header("[ Parts ]"), Space(10)]
        [SerializeField] ScrollRect _listScrollRect = null;
        [SerializeField] Transform _emptyListContent = null;

        [Space(5), Header("[ Configs ]"), Space(10)]
        [SerializeField] TItem _listItemPrefab = null;

        #region MonoBehaviour
        void OnEnable()
        {
            _isFirstReset = true;
            _isFirstListReset = true;
            SubscribeAllListenersOnEnable();

            _isFirstListReset = false;
        }

        void OnDisable()
        {
            UnsubscribeAllListenersOnDisable();
        }
        #endregion MonoBehaviour

        #region Public API (wiring)
        /// <summary>
        /// Binds this list to an external reactive getter source. Whenever the external source emits,
        /// this list updates its own BaseListStateGetter.Value, triggering (re)subscription to the inner property.
        /// </summary>
        public void BindGetterSource(ReactiveProperty<Func<ReactiveProperty<Dictionary<Guid, TData>>>> externalGetter)
        {
            _externalGetterSubscription?.Dispose();
            _externalGetterSubscription = null;

            if (externalGetter == null)
            {
                _baseListStateGetter.Value = null;
                return;
            }

            _externalGetterSubscription = externalGetter
                .AsObservable()
                .Subscribe(fn => _baseListStateGetter.Value = fn);

            _baseListStateGetter.Value = externalGetter.Value;
        }
        #endregion Public API (wiring)

        #region Subscriptions
        // ENABLE <-> disable
        // inverse of UnsubscribeAllListenersOnDisable
        void SubscribeAllListenersOnEnable()
        {
            _getterSubscription = _baseListStateGetter
                .AsObservable()
                .Subscribe(OnListGetterChanged);

            OnListGetterChanged(_baseListStateGetter.Value);
        }

        // enable <-> DISABLE
        // inverse of SubscribeAllListenersOnEnable
        void UnsubscribeAllListenersOnDisable()
        {
            _innerStateSubscription?.Dispose();
            _innerStateSubscription = null;

            _getterSubscription?.Dispose();
            _getterSubscription = null;

            _externalGetterSubscription?.Dispose();
            _externalGetterSubscription = null;
        }

        void OnListGetterChanged(Func<ReactiveProperty<Dictionary<Guid, TData>>> newGetter)
        {
            _innerStateSubscription?.Dispose();
            _innerStateSubscription = null;

            if (newGetter == null)
            {
                InstantiateItemsList(new Dictionary<Guid, TData>());
                return;
            }

            ReactiveProperty<Dictionary<Guid, TData>> inner = null;

            try { inner = newGetter.Invoke(); }
            catch (Exception e)
            {
                DebugExtension.DefaultGenericLog(
                    "$$> ".ToColor(GoodColors.Red),
                    "List getter threw an exception: ", e.ToString(), "\n", ""
                );
            }

            if (inner == null)
            {
                InstantiateItemsList(new Dictionary<Guid, TData>());
                return;
            }

            _innerStateSubscription = inner.AsObservable().Subscribe(OnListStateUpdate);
            InstantiateItemsList(inner.Value ?? new Dictionary<Guid, TData>());
        }

        void OnListStateUpdate(Dictionary<Guid, TData> newState)
        {
            InstantiateItemsList(newState ?? new Dictionary<Guid, TData>());
        }
        #endregion Subscriptions

        #region Controller
        void InstantiateItemsList(Dictionary<Guid, TData> itemsById)
        {
            InstantiateItemsList(itemsById, false);
        }

        void InstantiateItemsList(Dictionary<Guid, TData> itemsById, bool instantaneously)
        {
            RectTransform baseContainer = null;
            _listScrollRect.DoIfNotNull(() => baseContainer = _listScrollRect.content);

            _currentListItems = new List<TItem>();

            List<Transform> childsToDestroy = new List<Transform>();

            baseContainer.DoIfNotNull(() =>
            {
                foreach (Transform child in baseContainer)
                {
                    if (child != _emptyListContent)
                        childsToDestroy.Add(child);
                }

                foreach (var child in childsToDestroy)
                    Destroy(child.gameObject);

                foreach (var kv in itemsById)
                {
                    var id = kv.Key;
                    var data = kv.Value;

                    TItem instance = Instantiate(_listItemPrefab, baseContainer);
                    SafeBindItem(instance, id, data);
                    _currentListItems.Add(instance);
                }
            });

            bool isEmpty = itemsById == null || itemsById.Count == 0;
            _emptyListContent.TryToApplyViewState(isEmpty, instantaneously || !isEmpty || _isFirstListReset);

            ResetScrollPosition();
        }

        void SafeBindItem(TItem instance, Guid id, TData data)
        {
            try { BindItem(instance, id, data); }
            catch (Exception e)
            {
                DebugExtension.DefaultGenericLog(
                    "$$> ".ToColor(GoodColors.Red),
                    "BindItem exception for id=", id.ToString(), " => ", e.ToString(), "\n", ""
                );
            }
        }

        /// <summary>
        /// Concrete classes must define how to bind data to the item UI.
        /// </summary>
        protected abstract void BindItem(TItem instance, Guid id, TData data);
        #endregion Controller

        #region View
        void ResetScrollPosition(Action onFinishCallback = null)
        {
            if (_resetScrollPositionCoroutine is not null)
                StopCoroutine(_resetScrollPositionCoroutine);

            _resetScrollPositionCoroutine = StartCoroutine(ResetScrollPositionCoroutine(onFinishCallback));
        }

        IEnumerator ResetScrollPositionCoroutine(Action onFinishCallback = null)
        {
            yield return new WaitForEndOfFrame();
            _listScrollRect.DoIfNotNull(() => LayoutRebuilder.ForceRebuildLayoutImmediate(_listScrollRect.content));

            _listScrollRect.DoIfNotNull(() => _listScrollRect.verticalNormalizedPosition = 1f);

            _isFirstReset = false;
            onFinishCallback?.Invoke();
        }
        #endregion View
    }
}
