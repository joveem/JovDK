// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.UI.ToastNotification
{
    public partial class ToastNotificationView : BasePanel
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        bool _hasDuration = false;
        bool _hasReachedDurationEnd = false;
        float _ramaingDuration = -1f;
        Action _onContentClickCallback = null;
        Action _onCloseButtonClickCallback = null;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Button _mainButton;
        [SerializeField] Button _closeButton;
        [SerializeField] TextMeshProUGUI _mainText;
        [SerializeField] Image _backgroundImage;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;


        #region MonoBehaviour
        // void Awake()
        // {
        //     // SetInitialState();
        // }

        // void OnEnable()
        // {
        //     // // TODO: review this!
        //     // SubscribeAllListenersOnEnable();
        // }

        void Start()
        {
            // ! DEBUG ONLY!!!
            // ! DEBUG ONLY!!!
            // ! DEBUG ONLY!!!
            // HidePanelInstantaneously();
            // PlayShowAnimation();
            // ! DEBUG ONLY!!!
            // ! DEBUG ONLY!!!
            // ! DEBUG ONLY!!!

            // // TODO: review this!
            // SubscribeAllListenersOnStart();
            SetupButtons();
        }

        // void FixedUpdate()
        // {

        // }

        void Update()
        {
            HandleDurationUpdate(Time.deltaTime);
        }

        // void OnDisable()
        // {
        //     // // TODO: review this!
        //     // UnsubscribeAllListenersOnDisable();
        // }

        // void OnDestroy()
        // {
        //     // // TODO: review this!
        //     // UnsubscribeAllListenersOnDestroy();
        // }
        #endregion MonoBehaviour

        #region Callbacks
        void OnContentClick()
        {
            _onContentClickCallback?.Invoke();
        }

        void OnCloseButtonClick()
        {
            _onCloseButtonClickCallback?.Invoke();
        }
        #endregion Callbacks


        #region Buttons
        void SetupButtons()
        {
            _mainButton.SetOnClickIfNotNull(MainButton);
            _closeButton.SetOnClickIfNotNull(CloseButton);
        }

        void MainButton()
        {
            // DebugExtension.DefaultButtonLog();

            if (_isShowing && _onContentClickCallback != null)
            {
                OnContentClick();
                ClosePanel();
            }
        }

        void CloseButton()
        {
            // DebugExtension.DefaultButtonLog();

            if (_isShowing)
            {
                OnCloseButtonClick();
                ClosePanel();
            }
        }
        #endregion Buttons

        #region Subscriptions
        // // AWAKE/START <-> destroy
        // // inverse of UnsubscribeAllListenersOnDestroy
        // void SubscribeAllListenersOnStart()
        // {
        //     // this scripts -> other script            
        //     // ! destroy
        //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnQuantumRegionUpdate);
        // }

        // // awake/start <-> DESTROY
        // // inverse of SubscribeAllListenersOnStart
        // void UnsubscribeAllListenersOnDestroy()
        // {
        //     // this scripts -> other script
        //     // ! destroy
        //     // REVIEW THIS quantumRegionReactive <- OnQuantumRegionUpdate is unsubscribed on destroy automatically
        // }

        // // ENABLE <-> disable
        // // inverse of UnsubscribeAllListenersOnDisable
        // void SubscribeAllListenersOnEnable()
        // {
        //     // this scripts -> other script            
        //     // ! disable
        //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDisable(gameObject).Subscribe(OnQuantumRegionUpdate);
        // }

        // // enable <-> DISABLE
        // // inverse of SubscribeAllListenersOnEnable
        // void UnsubscribeAllListenersOnDisable()
        // {
        //     // this scripts -> other script
        //     // ! disable
        //     // REVIEW THIS quantumRegionReactive <- OnQuantumRegionUpdate is unsubscribed on disable automatically
        // }
        #endregion Subscriptions

        #region Controller
        // void SetInitialState()
        // {

        // }

        void HandleDurationUpdate(float deltaTime)
        {
            if (_hasDuration && !_hasReachedDurationEnd)
            {
                _hasReachedDurationEnd = _ramaingDuration <= 0f;

                if (!_hasReachedDurationEnd)
                    _ramaingDuration -= deltaTime;

                _hasReachedDurationEnd = _ramaingDuration <= 0f;

                if (_hasReachedDurationEnd)
                {
                    if (_isShowing)
                        ClosePanel();
                }
            }
        }

        public void SetDescriptionText(string content)
        {
            _mainText.DoIfNotNull(() =>
            {
                _mainText.text = content;
                LayoutRebuilder.ForceRebuildLayoutImmediate(_mainText.rectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            });
        }

        public void SetDuration(float? durationInSeconds)
        {
            if (durationInSeconds != null)
            {
                _hasDuration = true;
                _hasReachedDurationEnd = false;

                _ramaingDuration = (float)durationInSeconds;
            }
        }

        public void SetBackgroundColor(Color color)
        {
            // DebugExtension.DefaultGenericLog();

            _backgroundImage.DoIfNotNull(() =>
            {
                float previousAlpha = _backgroundImage.color.a;
                Color newColor = new Color(color.r, color.g, color.b, previousAlpha);
                _backgroundImage.color = newColor;
            });
        }

        public void SetOnContentClickCallback(Action callback)
        {
            _onContentClickCallback = callback;
        }

        public void SetOnCloseButtonClickCallback(Action callback)
        {
            _onCloseButtonClickCallback = callback;
        }
        #endregion Controller

        #region View
        // public void ShowPanel()
        // {

        // }
        #endregion View
    }
}
