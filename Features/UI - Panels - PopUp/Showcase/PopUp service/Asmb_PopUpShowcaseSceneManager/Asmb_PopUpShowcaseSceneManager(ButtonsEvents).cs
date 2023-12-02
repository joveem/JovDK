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
using JovDK.Services;

// from project
// ...


public partial class Asmb_PopUpShowcaseSceneManager : MonoBehaviour
{
    void SetupButtons()
    {
        // information
        _showTitleOnlyPopUpButton.SetOnClickIfNotNull(ShowTitleOnlyPopUpButton);
        _showDescriptionOnlyPopUpButton.SetOnClickIfNotNull(ShowDescriptionOnlyPopUpButton);
        _showTitleAndDescriptionPopUpButton.SetOnClickIfNotNull(ShowTitleAndDescriptionPopUpButton);
        _showInformationPopUpWithCallbackButton.SetOnClickIfNotNull(ShowInformationPopUpWithCallbackButton);
        // confirmation
        _showConfirmationPopUpButton.SetOnClickIfNotNull(ShowConfirmationPopUpButton);
        _showConfirmationPopUpWithTwoCallbacksButton.SetOnClickIfNotNull(ShowConfirmationPopUpWithTwoCallbacksButton);
        // confirmation
        _showLoadingPopUpButton.SetOnClickIfNotNull(ShowLoadingPopUpButton);
    }

    void ShowTitleOnlyPopUpButton()
    {
        ShowTitleOnlyPopUp();
    }

    void ShowDescriptionOnlyPopUpButton()
    {
        ShowDescriptionOnlyPopUp();
    }

    void ShowTitleAndDescriptionPopUpButton()
    {
        ShowTitleAndDescriptionPopUp();
    }

    void ShowConfirmationPopUpButton()
    {
        ShowConfirmationPopUp();
    }

    void ShowInformationPopUpWithCallbackButton()
    {
        ShowInformationPopUpWithCallback();
    }

    void ShowConfirmationPopUpWithTwoCallbacksButton()
    {
        ShowConfirmationPopUpWithTwoCallbacks();
    }

    void ShowLoadingPopUpButton()
    {
        ShowLoadingPopUp();
    }
}
