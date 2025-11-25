// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
// using R3;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Generic.TimeManagement
{
    public partial class ReliableTimeService : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        bool _isInitialized = false;
        DateTime _startUTCTime;

        public Action OnInitializedCallback = null;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // const string _worldTimeApiUrl = "https://worldtimeapi.org/api/ip";
        // const string _worldTimeApiUrl = "http://www.worldtimeapi.org/api/ip";
        // const string _worldTimeApiUrl = "http://worldtimeapi.org/api/ip";
        // const string _worldTimeApiUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";
        const string _worldTimeApiUrl = "https://www.worldtimeapi.org/api/ip";



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
            SetInitialState();
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
        void OnInitialized()
        {
            OnInitializedCallback?.Invoke();
        }

        // // void OnIdk(bool value)
        // void OnIdk()
        // {
        //     // DebugExtension.DefaultCallbackLog();

        //     // OnIdkCallback?.Invoke(value);
        //     OnIdkCallback?.Invoke();
        // }
        #endregion Callbacks

        #region Buttons
        // void SetupButtons()
        // {
        //     _mainButton.SetOnClickIfNotNull(MainButton);
        // }

        // void MainButton()
        // {
        //     DebugExtension.DefaultButtonLog();


        // }
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
        public void SetInitialState()
        {
            _startUTCTime = DateTime.UtcNow;
            StartCoroutine(GetNTPTime());
        }

        IEnumerator GetNTPTime()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(_worldTimeApiUrl);
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // string debugText =
                //     "$ > ".ToColor(GoodColors.Red) +
                //     "ERROR trying to GetNTPTime!" + "\n" +
                //     "webRequest.result = " + webRequest.result.ToString() + "\n" +
                //     "webRequest.error = " + "\n" +
                //     webRequest.error + "\n" +
                //     "";
                // Debug.LogError(debugText);

                if (webRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError("Data Processing Error: " + webRequest.error);
                }
                else
                {
                    Debug.LogError("Network Error: " + "\"" + webRequest.error + "\"");
                    Debug.LogError("Received: " + "\"" + webRequest.downloadHandler.text + "\"");
                    Debug.LogError(
                        "downloadHandler.data = " + "\n" +
                        webRequest.downloadHandler.data.SerializeObjectToJSON(true) + "\n" +
                        "");
                }

                _startUTCTime = DateTime.UtcNow;
            }
            else
            {
                string rawJsonResponse = webRequest.downloadHandler.text;
                WorldTimeResponse worldTimeResponse = rawJsonResponse.DeserializeJsonToObject<WorldTimeResponse>();

                Debug.Log("response = " + "\n" + rawJsonResponse);
                _startUTCTime = worldTimeResponse.DateTime.ToUniversalTime();
                Debug.Log("UTC now =  " + "\n" + _startUTCTime);
            }

            _isInitialized = true;
            OnInitialized();
        }

        public DateTime ReliableUTCTimeNow()
        {
            DateTime value;

            value = _startUTCTime.AddSeconds(Time.time);

            return value;
        }
        #endregion Controller

        #region Controller - Meta Data
        public bool IsInitialized()
        {
            return _isInitialized;
        }
        #endregion Controller - Meta Data

        #region View
        // protected virtual void TryToKillBackgroundTween()
        // {
        //     if (_curretBackgroundTween.IsActive())
        //         _curretBackgroundTween.Kill();
        // }
        #endregion View
    }
}
