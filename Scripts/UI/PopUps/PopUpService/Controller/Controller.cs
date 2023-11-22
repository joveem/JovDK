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
using JovDK.Debug;
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
        public PopUp ShowPopUpInformation(
            string title = null,
            string description = null,
            string positiveButtonText = null,
            Action positiveCallback = null)
        {
            PopUp popUpInstance = Instantiate(_informationPopUpPrefab, _popUpContainer);

            popUpInstance.SetTexts(title, description);
            popUpInstance.SetButtonsText(positiveButtonText);

            positiveCallback.DoIfNotNull(() => popUpInstance.SetConfirmationAction(positiveCallback), false);

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
            Action negativeCallback = null)
        {
            PopUp popUpInstance = Instantiate(_confirmationPopUpPrefab, _popUpContainer);

            popUpInstance.SetTexts(title, description);
            popUpInstance.SetButtonsText(positiveButtonText, negativeButtonText);

            positiveCallback.DoIfNotNull(() => popUpInstance.SetConfirmationAction(positiveCallback), false);
            negativeCallback.DoIfNotNull(() => popUpInstance.SetCancelAction(positiveCallback), false);

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
}
