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
using JovDK.Debugging;
using JovDK.Generic.Assets;
using JovDK.Localization.Countries;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Forms.PhoneNumber
{
    public partial class DdiListItem : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        // [SerializeField] bool _state1;
        // ReactiveProperty<bool> _state2 = new ReactiveProperty<bool>(false);
        // public ReactiveProperty<bool> State => _state2;
        // Tween _curretBackgroundTween = null;
        string _countryIsoCode = null;
        bool _hasLoadedFlag = false;
        bool _isLoadingFlag = false;
        bool _isFirstLoading = true;
        bool _hasReachedStartBeginning = false;
        bool _hasReachedStartEnd = false;
        // name font loading
        bool _hasLoadedName = false;
        bool _isLoadingName = false;
        string _loadingCountryNameIsoCode = null;
        Coroutine _loadCountryNameCoroutine = null;
        // public Action OnIdkCallback = null;
        // public Action<bool> OnIdkCallback = null;
        // List<ISubscription> _onStartSubscriptions = new List<ISubscription>();
        // List<ISubscription> _externalOnStartSubscriptions = new List<ISubscription>();
        // public List<ISubscription> ExternalOnStartSubscriptions => _externalOnStartSubscriptions;
        // List<ISubscription> _onEnableSubscriptions = new List<ISubscription>();
        // List<ISubscription> _externalOnEnableSubscriptions = new List<ISubscription>();
        // public List<ISubscription> ExternalOnEnableSubscriptions => _externalOnEnableSubscriptions;
        // callbacks
        public Action<string> OnClickCallback = null;

        [Space(5), Header("[ Parts ]"), Space(10)]

        // [SerializeField] bool _parts;
        [SerializeField] Button _mainButton;
        [SerializeField] RectTransform _loadingContent;
        [SerializeField] Image _countryFlagIconImage;
        [SerializeField] TextMeshProUGUI _countryNameText;
        [SerializeField] TextMeshProUGUI _ddiText;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;




        #region MonoBehaviour
        // void Awake()
        // {
        //     // SetInitialState();
        // }

        void OnEnable()
        {
            // // TODO: review this!
            // SubscribeAllListenersOnEnable();

            HandleCountryFlagLoading();
            RefreshViewState(instantaneously: true);

            _isFirstLoading = false;
        }

        void Start()
        {
            _hasReachedStartBeginning = true;

            // // TODO: review this!
            // SubscribeAllListenersOnStart();
            SetupButtons();

            HandleCountryFlagLoading();
            RefreshViewState(instantaneously: true);

            _hasReachedStartEnd = true;
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
        void OnClick()
        {
            // DebugExtension.DefaultCallbackLog();

            OnClickCallback?.Invoke(_countryIsoCode);
        }
        #endregion Callbacks

        #region Buttons
        void SetupButtons()
        {
            _mainButton.SetOnClickIfNotNull(MainButton);
        }

        void MainButton()
        {
            // DebugExtension.DefaultButtonLog();

            OnClick();
        }
        #endregion Buttons

        #region Controller
        public void SetCountryIsoCode(string isoCode)
        {
            _countryIsoCode = isoCode;

            if (_hasReachedStartEnd)
            {
                HandleCountryFlagLoading();
                RefreshViewState();
            }
        }

        void HandleCountryFlagLoading()
        {
            if (!_hasLoadedFlag)
            {
                if (!_isLoadingFlag)
                {
                    if (!String.IsNullOrWhiteSpace(_countryIsoCode))
                    {
                        _isLoadingFlag = true;

                        string finalFlagPath = FlagIconsResourceKeys.SquareFlagIconsFolderResourcePath_128x128 + _countryIsoCode;
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
                                    else if (!_isFirstLoading)
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
                    else if (!_isFirstLoading)
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
            string ddiTextValue = PhoneNumberFormsUtils.GetDdiTextByIsoCode(_countryIsoCode);
            _ddiText.SetTextIfNotNull(ddiTextValue);

            HandleCountryNameTextLoading();
        }

        void HandleCountryNameTextLoading()
        {
            if (!_hasLoadedName && !_isLoadingName)
                _countryNameText.SetTextIfNotNull("...");

            if (_countryIsoCode != null && _countryIsoCode != _loadingCountryNameIsoCode)
            {
                _isLoadingName = true;
                _loadingCountryNameIsoCode = _countryIsoCode;

                if (_loadCountryNameCoroutine != null)
                    StopCoroutine(_loadCountryNameCoroutine);

                _loadCountryNameCoroutine = StartCoroutine(LoadCountryNameCoroutine());
            }
        }

        IEnumerator LoadCountryNameCoroutine()
        {
            yield return null;

            string countryNameValue = CountryNamesUtils.GetCountryNameByIsoCode(_countryIsoCode, debugIfNull: !_isFirstLoading);
            _countryNameText.SetTextIfNotNull(countryNameValue);
            _hasLoadedName = true;
        }
        #endregion View
    }
}
