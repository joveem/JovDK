// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.UI.PopUp;

// from project
// ...

namespace JovDK.Services.PopUpExtensions
{
    public static partial class PopUpServiceExtensions
    {

        #region Controller
        /// <summary>
        /// Try to show information pop-up. If the
        /// PopUpService is null, the action will
        /// trigger automatically, without showing
        /// the information pop-up.
        /// </summary>
        /// <param name="basePopUpService"></param> <summary>
        ///
        /// </summary>
        /// <param name="basePopUpService"></param>
        public static void ShowPopUpInformation_DoPositiveCallbackEvenIfNull(
            this PopUpService basePopUpService,
            string title = null,
            string description = null,
            string positiveButtonText = null,
            Action positiveCallback = null,
            Action negativeCallback = null,
            Action closeCallback = null,
            bool debugIfNull = true)
        {
            Action mainActionCallback = () =>
            {
                basePopUpService.ShowPopUpInformation(
                    title: title,
                    description: description,
                    positiveButtonText: positiveButtonText,
                    positiveCallback: positiveCallback,
                    negativeCallback: negativeCallback,
                    closeCallback: closeCallback);
            };

            Action ifNullAction = () =>
            {
                if (debugIfNull)
                {
                    DebugExtension.DevLogWarning(
                        "$$> ".ToColor(GoodColors.Red),
                        "basePopUpService is null!", "\n",
                        "");
                }

                positiveCallback?.Invoke();
            };

            basePopUpService.DoIfNotNull(
                action: mainActionCallback,
                ifNullAction: ifNullAction);
        }

        /// <summary>
        /// Try to show confirmation pop-up. If the
        /// PopUpService is null, the action will
        /// trigger automatically, without showing
        /// the confirmation pop-up.
        /// </summary>
        /// <param name="basePopUpService"></param> <summary>
        ///
        /// </summary>
        /// <param name="basePopUpService"></param>
        public static void ShowPopUpConfirmation_DoPositiveCallbackEvenIfNull(
            this PopUpService basePopUpService,
            string title = null,
            string description = null,
            string positiveButtonText = null,
            string negativeButtonText = null,
            Action positiveCallback = null,
            Action negativeCallback = null,
            Action closeCallback = null,
            bool debugIfNull = true)
        {
            Action mainActionCallback = () =>
            {
                basePopUpService.ShowPopUpConfirmation(
                    title: title,
                    description: description,
                    positiveButtonText: positiveButtonText,
                    negativeButtonText: negativeButtonText,
                    positiveCallback: positiveCallback,
                    negativeCallback: negativeCallback,
                    closeCallback: closeCallback);
            };

            Action ifNullAction = () =>
            {
                if (debugIfNull)
                {
                    DebugExtension.DevLogWarning(
                        "$$> ".ToColor(GoodColors.Red),
                        "basePopUpService is null!", "\n",
                        "");
                }

                positiveCallback?.Invoke();
            };

            basePopUpService.DoIfNotNull(
                action: mainActionCallback,
                ifNullAction: ifNullAction);
        }
        #endregion Controller
    }
}
