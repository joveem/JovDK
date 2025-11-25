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
using JovDK.Services;
using JovDK.UI.ToastNotification;

// from project
// ...


namespace PackageName.MajorContext.MinorContext
{
    public partial class Asmb_ToastNotificationServiceShowcaseScene : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] PopUpService _popUpService;
        [SerializeField] ToastNotificationService _toastNotificationService;


        // [Space(5), Header("[ State ]"), Space(10)]

        // [SerializeField] bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Transform _buttonsContainer;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] Button _buttonPrefab = null;


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
        // void OnIdk()
        // {
        //        // DebugExtension.DefaultGenericLog();
        // }
        #endregion Callbacks

        #region Buttons
        void SetupButtons()
        {
            float defaultDuration = 3f;
            string currentTitle = "UNDEFINED";

            currentTitle = "[ " + defaultDuration + " segs  ] " + "normal notification";
            InstantiateButton(
                currentTitle,
                null,
                null,
                defaultDuration);

            currentTitle = "[infinite] normal notification";
            InstantiateButton(
                currentTitle,
                null,
                null);

            currentTitle = "[ " + defaultDuration + " segs  ] " + "content click callback notification";
            InstantiateButton(
                currentTitle,
                () => _popUpService.ShowPopUpInformation("content click callback notification"),
                null,
                defaultDuration);

            currentTitle = "[ " + defaultDuration + " segs  ] " + "close click callback notification";
            InstantiateButton(
                currentTitle,
                null,
                () => _popUpService.ShowPopUpInformation("close click callback notification"),
                defaultDuration);

            currentTitle = "[ " + defaultDuration + " segs  ] " + "both clicks callback notification";
            InstantiateButton(
                currentTitle,
                () => _popUpService.ShowPopUpInformation("[both clicks] content click callback notification"),
                () => _popUpService.ShowPopUpInformation("[both clicks] close click callback notification"),
                defaultDuration);

            currentTitle = "[ " + defaultDuration + " segs  ] " + "randon color notification";
            InstantiateButton(
                currentTitle,
                () =>
                {
                    string title = currentTitle;
                    float r = UnityRandom.Range(0f, 1f);
                    float g = UnityRandom.Range(0f, 1f);
                    float b = 1f - r - g;

                    Color randomColor = new Color(r, g, b);

                    _toastNotificationService.AddNotification(
                        "[ " + defaultDuration + " segs  ] " + "randon color notification",
                        null,
                        null,
                        defaultDuration,
                        randomColor);
                });
        }

        void InstantiateButton(string textContent, UnityEngine.Events.UnityAction clickCallback)
        {
            Button instance = Instantiate(_buttonPrefab, _buttonsContainer);

            instance.SetTextInButton(textContent);
            instance.SetOnClickIfNotNull(clickCallback);
        }

        void InstantiateButton(
            string title,
            Action onContentClickCallback = null,
            Action onCloseButtonClickCallback = null,
            float? durationInSeconds = null,
            Color? backgroundColor = null)
        {
            Button instance = Instantiate(_buttonPrefab, _buttonsContainer);

            instance.SetTextInButton(title);
            instance.SetOnClickIfNotNull(
                () =>
                {
                    _toastNotificationService.AddNotification(
                        title,
                        onContentClickCallback,
                        onCloseButtonClickCallback,
                        durationInSeconds,
                        backgroundColor);
                }
            );
        }
        #endregion Buttons

        #region Subscriptions
        // // AWAKE/START <-> destroy
        // // inverse of UnsubscribeAllListenersOnDestroy
        // void SubscribeAllListenersOnStart()
        // {
        //     // this scripts -> other script            
        //     // ! destroy
        //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDestroy(gameObject).Subscribe(OnIdkPropertyUpdate);
        // }

        // // awake/start <-> DESTROY
        // // inverse of SubscribeAllListenersOnStart
        // void UnsubscribeAllListenersOnDestroy()
        // {
        //     // this scripts -> other script
        //     // ! destroy
        //     // REVIEW THIS IdkProperty <- OnIdkPropertyUpdate is unsubscribed on destroy automatically
        // }

        // // ENABLE <-> disable
        // // inverse of UnsubscribeAllListenersOnDisable
        // void SubscribeAllListenersOnEnable()
        // {
        //     // this scripts -> other script            
        //     // ! disable
        //     // _randomDataBus.IdkProperty.AsObservable().TakeUntilDisable(gameObject).Subscribe(OnIdkPropertyUpdate);
        // }

        // // enable <-> DISABLE
        // // inverse of SubscribeAllListenersOnEnable
        // void UnsubscribeAllListenersOnDisable()
        // {
        //     // this scripts -> other script
        //     // ! disable
        //     // REVIEW THIS IdkProperty <- OnIdkPropertyUpdate is unsubscribed on disable automatically
        // }

        // void OnIdkPropertyUpdate(int newValue)
        // {
        //     // DebugExtension.DevLog(
        //     //     "#> ".ToColor(GoodColors.Pink) +
        //     //     "newValue = " + newValue.SerializeObjectToJSON() + "\n" +
        //     //     "");
        // }
        #endregion Subscriptions

        #region Controller
        // void SetInitialState()
        // {
        //     // DebugExtension.DefaultGenericLog();
        // }
        #endregion Controller

        #region View
        // public void ShowPanel()
        // {

        // }
        #endregion View
    }
}
