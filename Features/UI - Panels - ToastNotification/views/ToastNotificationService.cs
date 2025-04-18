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
    public partial class ToastNotificationService : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // [SerializeField] bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Transform _notifictionsContainer;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] ToastNotificationView _toastNotificationViewPrefab = null;



        #region MonoBehaviour
        void Awake()
        {
            SetInitialState();
        }

        // void OnEnable()
        // {
        //     // // TODO: review this!
        //     // SubscribeAllListenersOnEnable();
        // }

        // void Start()
        // {
        //     // // TODO: review this!
        //     // SubscribeAllListenersOnStart();
        // }

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

        #region Buttons
        // void SetupButtons()
        // {
        //     _mainButton.SetOnClickIfNotNull(MainButton);
        // }

        // void MainButton()
        // {

        // }
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
        //     //     "#>".ToColor(GoodColors.Pink) +
        //     //     "newValue = " + newValue.SerializeObjectToJSON() + "\n" +
        //     //     "");
        // }
        #endregion Subscriptions

        #region Controller
        void SetInitialState()
        {
            // DebugExtension.DevLog(">".ToColor(GoodColors.Orange));
            DestroyAllCurrentContent();
        }

        void DestroyAllCurrentContent()
        {
            foreach (Transform chield in _notifictionsContainer)
                Destroy(chield.gameObject);
        }

        public void _INTERNAL_AddNotification(
            string descriptionText,
            Action onContentClickCallback = null,
            Action onCloseButtonClickCallback = null,
            float? durationInSeconds = null,
            Color? backgroundColor = null)
        {
            ToastNotificationView instance = Instantiate(_toastNotificationViewPrefab, _notifictionsContainer);

            instance.SetDescriptionText(descriptionText);
            instance.SetDuration(durationInSeconds);
            instance.SetOnContentClickCallback(onContentClickCallback);
            instance.SetOnCloseButtonClickCallback(onCloseButtonClickCallback);

            if (backgroundColor != null)
                instance.SetBackgroundColor((Color)backgroundColor);

            instance.HidePanelInstantaneously();
            instance.PlayShowAnimation();
        }
        #endregion Controller
    }

    public static class ToastNotificationServiceExtension
    {
        public static void AddNotification(
            this ToastNotificationService baseToasNotificationService,
            string descriptionText,
            Action onContentClickCallback = null,
            Action onCloseButtonClickCallback = null,
            float? durationInSeconds = null,
            Color? backgroundColor = null)
        {
            baseToasNotificationService.DoIfNotNull(() =>
            {
                baseToasNotificationService._INTERNAL_AddNotification(
                    descriptionText,
                    onContentClickCallback,
                    onCloseButtonClickCallback,
                    durationInSeconds,
                    backgroundColor);
            });
        }
    }
}
