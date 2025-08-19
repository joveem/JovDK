// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using PhoneNumbers;
using R3;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;
using JovDK.UI.Generic;

// from project
// ...


namespace JovDK.Forms.PhoneNumber
{
    public partial class DdiListPanel : BaseGenericSimplePanel
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        // [SerializeField] bool _state1;
        // ReactiveProperty<bool> _state2 = new ReactiveProperty<bool>(false);
        // public ReactiveProperty<bool> State => _state2;
        // Tween _curretBackgroundTween = null;
        // public Func<bool> State3Getter = null;
        // public Action OnIdkCallback = null;
        // public Action<bool> OnIdkCallback = null;
        // List<ISubscription> _onStartSubscriptions = new List<ISubscription>();
        // List<ISubscription> _externalOnStartSubscriptions = new List<ISubscription>();
        // public List<ISubscription> ExternalOnStartSubscriptions => _externalOnStartSubscriptions;
        // List<ISubscription> _onEnableSubscriptions = new List<ISubscription>();
        // List<ISubscription> _externalOnEnableSubscriptions = new List<ISubscription>();
        // public List<ISubscription> ExternalOnEnableSubscriptions => _externalOnEnableSubscriptions;

        Coroutine _resetScrollPositionCoroutine = null;
        // callbacks
        public Action<string> OnCountrySelectionCallback = null;

        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Button _closeButton;
        [SerializeField] ScrollRect _listScrollRect;
        // [SerializeField] TextMeshProUGUI _mainText;
        // [SerializeField] Image _mainImage;
        // [SerializeField] Transform _mainContainer;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] DdiListItem _ddiListItemPrefab;



        #region MonoBehaviour
        // void Awake()
        // {
        //     // SetInitialState();
        // }

        void OnEnable()
        {
            // // TODO: review this!
            // SubscribeAllListenersOnEnable();

            InstantiateDdiListItems();
        }

        void Start()
        {
            // // TODO: review this!
            // SubscribeAllListenersOnStart();
            SetupButtons();
        }

        // void FixedUpdate()
        // {

        // }

        // void Update()
        // {

        // }

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
        // // void OnIdk(bool value)
        // void OnIdk()
        // {
        //     // DebugExtension.DefaultCallbackLog();

        //     // OnIdkCallback?.Invoke(value);
        //     OnIdkCallback?.Invoke();
        // }
        #endregion Callbacks

        #region Buttons
        void SetupButtons()
        {
            _closeButton.SetOnClickIfNotNull(CloseButton);
        }

        void CloseButton()
        {
            // DebugExtension.DefaultButtonLog();

            HidePanel();
        }
        #endregion Buttons

        #region Subscriptions
        // // AWAKE/START <-> destroy
        // // inverse of UnsubscribeAllListenersOnDestroy
        // void SubscribeAllListenersOnStart()
        // {
        //     // ! REVIEW THIS
        //     // ! start / destroy

        //     // _randomDataBus.DoIfNotNull(() =>
        //     // {
        //     //     // this scripts -> other script
        //     //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnIdkPropertyUpdate);
        //     //     _onStartSubscriptions.Register(
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction,
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction);
        //     //     _onStartSubscriptions.RegisterFrom(_randomDataBus.RandonProperty, OnRandonPropertyUpdate);
        //     // });
        // }

        // // awake/start <-> DESTROY
        // // inverse of SubscribeAllListenersOnStart
        // void UnsubscribeAllListenersOnDestroy()
        // {
        //     // ! REVIEW THIS
        //     // ! start / destroy

        //     // // this scripts -> other script
        //     // _onStartSubscriptions.UnsubscribeAllAndClear();
        //     // // this scripts -> external
        //     // _externalOnStartSubscriptions.UnsubscribeAllAndClear();
        // }

        // // ENABLE <-> disable
        // // inverse of UnsubscribeAllListenersOnDisable
        // void SubscribeAllListenersOnEnable()
        // {
        //     // ! REVIEW THIS
        //     // ! enable / disable

        //     // _randomDataBus.DoIfNotNull(() =>
        //     // {
        //     //     // this scripts -> other script
        //     //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnIdkPropertyUpdate);
        //     //     _onEnableSubscriptions.Register(
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction,
        //     //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction);
        //     //     _onEnableSubscriptions.RegisterFrom(_randomDataBus.RandonProperty, OnRandonPropertyUpdate);
        //     // });
        // }

        // // enable <-> DISABLE
        // // inverse of SubscribeAllListenersOnEnable
        // void UnsubscribeAllListenersOnDisable()
        // {
        //     // ! REVIEW THIS
        //     // ! enable / disable

        //     // // this scripts -> other script
        //     // _onEnableSubscriptions.UnsubscribeAllAndClear();
        //     // // this scripts -> external
        //     // _externalOnEnableSubscriptions.UnsubscribeAllAndClear();
        // }

        // void OnIdkPropertyUpdate(int newValue)
        // {
        //     // DebugExtension.DefaultSubscriptionLog();
        //     // DebugExtension.DefaultSubscriptionLog(
        //     //     "newValue = ", newValue.SerializeObjectToJSON(), "\n",
        //     //     "");


        // }
        #endregion Subscriptions

        #region Controller
        // void SetInitialState()
        // {
        //     DebugExtension.DefaultGenericLog();


        // }

        void InstantiateDdiListItems()
        {
            // Instantiate DDI list items here

            RectTransform baseContainer = null;
            _listScrollRect.DoIfNotNull(() => baseContainer = _listScrollRect.content);

            baseContainer.DoIfNotNull(() =>
            {
                foreach (Transform child in baseContainer)
                    Destroy(child.gameObject);

                HashSet<string> sortedRegions = new HashSet<string>()
                                                {
                                                    "BR", // Brazil
                                                    "PT", // Portugal
                                                    "US", // United States
                                                    "CA", // Canada
                                                    "GB", // United Kingdom
                                                    "AU", // Australia
                                                    "MX", // Mexico
                                                    "ES", // Spain
                                                    "IT", // Italy
                                                    "IN", // India

                                                    // South America

                                                    "AR", // Argentina
                                                    "BO", // Bolivia
                                                    // "BR", // Brazil
                                                    "CL", // Chile
                                                    "CO", // Colombia
                                                    "EC", // Ecuador
                                                    "GY", // Guyana
                                                    "PY", // Paraguay
                                                    "PE", // Peru
                                                    "SR", // Suriname
                                                    "UY", // Uruguay
                                                    "VE", // Venezuela
                                                    "FK", // Falkland Islands (geographically South America, UK overseas territory)

                                                    // European Union (27 member states)

                                                    "AT", // Austria
                                                    "BE", // Belgium
                                                    "BG", // Bulgaria
                                                    "HR", // Croatia
                                                    "CY", // Cyprus
                                                    "CZ", // Czechia
                                                    "DK", // Denmark
                                                    "EE", // Estonia
                                                    "FI", // Finland
                                                    "FR", // France
                                                    "DE", // Germany
                                                    "GR", // Greece
                                                    "HU", // Hungary
                                                    "IE", // Ireland
                                                    // "IT", // Italy
                                                    "LV", // Latvia
                                                    "LT", // Lithuania
                                                    "LU", // Luxembourg
                                                    "MT", // Malta
                                                    "NL", // Netherlands
                                                    "PL", // Poland
                                                    // "PT", // Portugal
                                                    "RO", // Romania
                                                    "SK", // Slovakia
                                                    "SI", // Slovenia
                                                    // "ES", // Spain
                                                    "SE", // Sweden
                                                };

                HashSet<string> supportedRegions = PhoneNumberUtil.GetInstance().GetSupportedRegions();
                sortedRegions.UnionWith(supportedRegions);

                foreach (var regionIso in sortedRegions)
                {
                    var ddiListItem = Instantiate(_ddiListItemPrefab, baseContainer);
                    ddiListItem.SetCountryIsoCode(regionIso);
                    ddiListItem.OnClickCallback = OnItemClick;
                }

                ResetScrollPosition();
            });
        }

        void OnItemClick(string countryIsoCode)
        {
            // DebugExtension.DefaultButtonLog();

            OnCountrySelectionCallback?.Invoke(countryIsoCode);
            HidePanel();
        }


        #endregion Controller

        #region View
        // protected virtual void TryToKillBackgroundTween()
        // {
        //     if (_curretBackgroundTween.IsActive())
        //         _curretBackgroundTween.Kill();
        // }

        void ResetScrollPosition()
        {
            // DebugExtension.DevLog();

            if (_resetScrollPositionCoroutine is not null)
                StopCoroutine(_resetScrollPositionCoroutine);

            _resetScrollPositionCoroutine = StartCoroutine(ResetScrollPositionCoroutine());
        }

        IEnumerator ResetScrollPositionCoroutine()
        {
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_listScrollRect.content);

            _listScrollRect.DoIfNotNull(() => _listScrollRect.verticalNormalizedPosition = 1f);
        }
        #endregion View
    }
}
