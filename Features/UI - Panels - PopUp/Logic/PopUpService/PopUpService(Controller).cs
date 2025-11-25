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


namespace JovDK.Services
{
    public partial class PopUpService : MonoBehaviour
    {
        public PopUp ShowPopUp(PopUpOptions popUpOptions)
        {
            // prefab selection
            PopUp basePrefab = _confirmationPopUpPrefab;

            if (popUpOptions.ShowPositiveButton && !popUpOptions.ShowNegativeButton)
                basePrefab = _informationPopUpPrefab;

            // instance
            PopUp popUpInstance = Instantiate(basePrefab, _popUpContainer);

            // content text
            popUpInstance.SetTexts(popUpOptions.Title, popUpOptions.Description);

            // buttons view state
            popUpInstance.SetPositiveButtonViewState(popUpOptions.ShowPositiveButton);
            popUpInstance.SetNegativeButtonViewState(popUpOptions.ShowNegativeButton);
            popUpInstance.SetCloseButtonViewState(popUpOptions.ShowCloseButton);

            // buttons texts
            string positiveButtonText = popUpOptions.ShowPositiveButton ? popUpOptions.PositiveButtonText : null;
            string negativeButtonText = popUpOptions.ShowNegativeButton ? popUpOptions.NegativeButtonText : null;
            string closeButtonText = null;

            popUpInstance.SetButtonsText(positiveButtonText, negativeButtonText, closeButtonText);

            // button callbacks
            Action positiveCallback2 = () =>
            {
                GlobalPositiveActionCallback?.Invoke();
                popUpOptions.PositiveButtonCallback?.Invoke();
            };
            Action negativeCallback2 = () =>
            {
                GlobalNegativeActionCallback?.Invoke();
                popUpOptions.NegativeButtonCallback?.Invoke();
            };
            Action closeCallback2 = () =>
            {
                GlobalCloseActionCallback?.Invoke();
                popUpOptions.CloseButtonCallback?.Invoke();
            };

            popUpInstance.SetConfirmationAction(positiveCallback2);
            popUpInstance.SetCancelAction(negativeCallback2);
            popUpInstance.SetCloseAction(closeCallback2);

            // handle view state and animations
            popUpInstance.HidePanelInstantaneously();
            popUpInstance.PlayShowAnimation();

            return popUpInstance;
        }

        public PopUp ShowPopUpInformation(
            string title = null,
            string description = null,
            string positiveButtonText = null,
            Action positiveCallback = null,
            Action negativeCallback = null,
            Action closeCallback = null)
        {
            PopUp popUpInstance = Instantiate(_informationPopUpPrefab, _popUpContainer);

            popUpInstance.SetTexts(title, description);
            popUpInstance.SetButtonsText(positiveButtonText);

            Action positiveCallback2 = () =>
            {
                GlobalPositiveActionCallback?.Invoke();
                positiveCallback?.Invoke();
            };
            Action closeCallback2 = () =>
            {
                GlobalCloseActionCallback?.Invoke();
                closeCallback?.Invoke();
            };

            popUpInstance.SetConfirmationAction(positiveCallback2);
            popUpInstance.SetCloseAction(closeCallback2);

            popUpInstance.HidePanelInstantaneously();
            popUpInstance.PlayShowAnimation();

            return popUpInstance;
        }

        public PopUp ShowPopUpConfirmation(
            string title = null,
            string description = null,
            string positiveButtonText = null,
            string negativeButtonText = null,
            Action positiveCallback = null,
            Action negativeCallback = null,
            Action closeCallback = null)
        {
            PopUp popUpInstance = Instantiate(_confirmationPopUpPrefab, _popUpContainer);

            popUpInstance.SetTexts(title, description);
            popUpInstance.SetButtonsText(positiveButtonText, negativeButtonText);

            Action positiveCallback2 = () =>
            {
                GlobalPositiveActionCallback?.Invoke();
                positiveCallback?.Invoke();
            };
            Action negativeCallback2 = () =>
            {
                GlobalNegativeActionCallback?.Invoke();
                negativeCallback?.Invoke();
            };
            Action closeCallback2 = () =>
            {
                GlobalCloseActionCallback?.Invoke();
                closeCallback?.Invoke();
            };

            popUpInstance.SetConfirmationAction(positiveCallback2);
            popUpInstance.SetCancelAction(negativeCallback2);
            popUpInstance.SetCloseAction(closeCallback2);

            popUpInstance.HidePanelInstantaneously();
            popUpInstance.PlayShowAnimation();

            return popUpInstance;
        }

        public PopUp ShowPopUpCover(
            string title = null,
            string description = null)
        {
            PopUp popUpInstance = Instantiate(_coverPopUpPrefab, _popUpContainer);

            popUpInstance.SetTexts(title, description);

            popUpInstance.HidePanelInstantaneously();
            popUpInstance.PlayShowAnimation();

            return popUpInstance;
        }

        public void ShowLoadingCover()
        {
            _loadingCoverPopup.DoIfNull(() =>
            {
                _loadingCoverPopup = ShowPopUpCover("Loading...");
            });
        }

        public void HideLoadingCover()
        {
            _loadingCoverPopup.DoIfNotNull(() =>
            {
                _loadingCoverPopup.ClosePanel();
                _loadingCoverPopup = null;
            });
        }
    }

    public class PopUpOptions
    {
        public string Title = null;
        public string Description = null;

        public string PositiveButtonText = "Ok";
        public string NegativeButtonText = "No";

        public bool ShowPositiveButton = true;
        public bool ShowNegativeButton = false;
        public bool ShowCloseButton = true;

        public Action PositiveButtonCallback = null;
        public Action NegativeButtonCallback = null;
        public Action CloseButtonCallback = null;
    }
}
