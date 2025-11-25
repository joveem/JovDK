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
using R3;
using TMPro;

// from company
using JovDK.Animations.Tweening;
using JovDK.Core.Subscription;
using JovDK.Debugging;
using JovDK.Generic.Assets;
using JovDK.Localization.Countries;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Forms.PhoneNumber
{
    public partial class DdiDropdown : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] DdiListPanel _ddiListPanel;


        [Space(5), Header("[ State ]"), Space(10)]

        ReactiveProperty<string> _countryIsoCode = new ReactiveProperty<string>(null);
        string _lastCountryIsoCode = null;
        public ReactiveProperty<string> CountryIsoCode => _countryIsoCode;
        bool _hasLoadedFlag = false;
        bool _isLoadingFlag = false;
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
        List<ISubscription> _onEnableSubscriptions = new List<ISubscription>();
        // List<ISubscription> _externalOnEnableSubscriptions = new List<ISubscription>();
        // public List<ISubscription> ExternalOnEnableSubscriptions => _externalOnEnableSubscriptions;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Button _mainButton;
        [SerializeField] RectTransform _loadingContent;
        [SerializeField] Image _countryFlagIconImage;
        [SerializeField] TextMeshProUGUI _ddiText;
        // [SerializeField] TextMeshProUGUI _mainText;
        // [SerializeField] Image _mainImage;
        // [SerializeField] Transform _mainContainer;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] bool _forceInitialCountryIsoCode = false;
        [SerializeField] string _initialCountryIsoCodeToForce = "us";



        #region MonoBehaviour
        void Awake()
        {
            SetInitialState();
        }

        void OnEnable()
        {
            SubscribeAllListenersOnEnable();

            HandleCountryFlagLoading();
            RefreshViewState(instantaneously: true);
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

        void OnDisable()
        {
            UnsubscribeAllListenersOnDisable();
        }

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
            _mainButton.SetOnClickIfNotNull(MainButton);
        }

        void MainButton()
        {
            DebugExtension.DefaultButtonLog();

            _ddiListPanel.DoIfNotNull(() =>
            {
                _ddiListPanel.OnCountrySelectionCallback = OnCountrySelection;
                _ddiListPanel.ShowPanel();
            });
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

        // ENABLE <-> disable
        // inverse of UnsubscribeAllListenersOnDisable
        void SubscribeAllListenersOnEnable()
        {
            // ! enable / disable

            // _randomDataBus.DoIfNotNull(() =>
            // {
            //     // this scripts -> other script
            //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnIdkPropertyUpdate);
            //     _onEnableSubscriptions.Register(
            //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction,
            //         () => _randomDataBus.OnRandonActionCallback += OnRandonAction);
            //     _onEnableSubscriptions.RegisterFrom(_randomDataBus.RandonProperty, OnRandonPropertyUpdate);
            // });






            // DDI dropdown -> country ISO code
            _onEnableSubscriptions.RegisterFrom(_countryIsoCode, OnCountryIsoCodeUpdate);
        }

        // enable <-> DISABLE
        // inverse of SubscribeAllListenersOnEnable
        void UnsubscribeAllListenersOnDisable()
        {
            // ! enable / disable

            // // this scripts -> other script
            // _onEnableSubscriptions.UnsubscribeAllAndClear();
            // // this scripts -> external
            // _externalOnEnableSubscriptions.UnsubscribeAllAndClear();











            // DDI dropdown -> country ISO code
            _onEnableSubscriptions.UnsubscribeAllAndClear();
        }

        void OnCountryIsoCodeUpdate(string newValue)
        {
            // DebugExtension.DefaultSubscriptionLog(
            //     "newValue = ", newValue.SerializeObjectToJSON(), "\n",
            //     "");

            bool hasDiffers = _lastCountryIsoCode != newValue;
            _lastCountryIsoCode = newValue;

            if (hasDiffers)
            {
                _hasLoadedFlag = false;
                _isLoadingFlag = false;
            }

            HandleCountryFlagLoading();
            RefreshViewState();
        }
        #endregion Subscriptions

        #region Controller
        void SetInitialState()
        {
            // DebugExtension.DefaultGenericLog();

            if (_forceInitialCountryIsoCode)
                _countryIsoCode.Value = _initialCountryIsoCodeToForce.Trim().ToUpper();
        }

        void HandleCountryFlagLoading()
        {
            if (!_hasLoadedFlag)
            {
                if (!_isLoadingFlag)
                {
                    if (!String.IsNullOrWhiteSpace(_countryIsoCode.Value))
                    {
                        _isLoadingFlag = true;

                        string finalFlagPath = FlagIconsResourceKeys.SquareFlagIconsFolderResourcePath_128x128 + _countryIsoCode.Value;
                        StartCoroutine(
                            ResourceUtils.LoadSpriteCoroutine(
                                finalFlagPath,
                                (sprite) =>
                                {
                                    _isLoadingFlag = false;

                                    if (sprite != null)
                                    {
                                        _hasLoadedFlag = true;
                                        _countryFlagIconImage.DoIfNotNull(() => _countryFlagIconImage.sprite = sprite);
                                    }
                                    else
                                    {
                                        DebugExtension.DevLogWarning(
                                            "$> ".ToColor(GoodColors.Red),
                                            "Error loading country flag sprite!", "\n",
                                            "finalFlagPath = ", finalFlagPath.SerializeObjectToJSON(), "\n",
                                            "");
                                    }

                                    RefreshViewState();
                                }));
                    }
                    else
                    {
                        DebugExtension.DevLogError(
                            "$> ".ToColor(GoodColors.Red),
                            "Country ISO code is null or empty. Cannot load flag.", "\n",
                            "_countryIsoCode = ", _countryIsoCode.SerializeObjectToJSON(), "\n",
                            "");
                    }
                }
            }

            RefreshViewState();
        }

        void OnCountrySelection(string countryIsoCode)
        {
            DebugExtension.DefaultGenericLog("countryIsoCode = ", countryIsoCode.SerializeObjectToJSON());

            _hasLoadedFlag = false;
            _isLoadingFlag = false;
            _countryIsoCode.Value = countryIsoCode;

            // HandleCountryFlagLoading();
            // RefreshViewState();
        }
        #endregion Controller

        #region View
        void RefreshViewState(bool instantaneously = false)
        {
            RefreshTextsValues();

            _loadingContent.TryToApplyViewState(_isLoadingFlag, applyInstantaneously: instantaneously);
            _countryFlagIconImage.TryToApplyViewState(_hasLoadedFlag, applyInstantaneously: instantaneously);
        }

        void RefreshTextsValues()
        {
            string ddiTextValue = PhoneNumberFormsUtils.GetDdiTextByIsoCode(_countryIsoCode.Value);

            _ddiText.SetTextIfNotNull(ddiTextValue);
        }
        #endregion View
    }
}
