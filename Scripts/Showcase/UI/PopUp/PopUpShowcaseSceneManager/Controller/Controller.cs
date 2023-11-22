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


public partial class PopUpShowcaseSceneManager : MonoBehaviour
{
    void ShowTitleOnlyPopUp()
    {
        string title = "title title title title title title";
        string positiveText = "ok";

        _popUpService.ShowPopUpInformation(
            title,
            null,
            positiveText);
    }

    void ShowDescriptionOnlyPopUp()
    {
        string description =
            "description description description description " +
            "description description description description " +
            "description description";
        string positiveText = "ok";

        _popUpService.ShowPopUpInformation(
            null,
            description,
            positiveText);
    }

    void ShowTitleAndDescriptionPopUp()
    {
        string title = "title title title title title title";
        string description =
            "description description description description " +
            "description description description description " +
            "description description";
        string positiveText = "ok";

        _popUpService.ShowPopUpInformation(
            title,
            description,
            positiveText);
    }

    void ShowInformationPopUpWithCallback()
    {
        string title = "Callback!";
        string description = "Click on \"ok\" to show another PopUp with a callback";
        string positiveText = "ok";
        Action positiveCallback = () =>
        {
            ShowCallbackPopUp();
        };

        _popUpService.ShowPopUpInformation(
            title,
            description,
            positiveText,
            positiveCallback);
    }

    void ShowConfirmationPopUp()
    {
        string description = "Do you wanna see a PopUp callback?";
        string positiveText = "yes";
        string negativeText = "no";
        Action positiveCallback = () =>
        {
            ShowCallbackPopUp();
        };

        _popUpService.ShowPopUpConfirmation(
            null,
            description,
            positiveText,
            negativeText,
            positiveCallback);
    }

    void ShowConfirmationPopUpWithTwoCallbacks()
    {
        string description =
            "You HAVE to see a PopUp " +
            "callback, that's not an option!";
        string positiveText = "ok";
        string negativeText = "sure!";
        Action positiveCallback = () =>
        {
            ShowCallbackPopUp();
        };
        Action negativeCallback = () =>
        {
            ShowCallbackPopUp();
        };

        _popUpService.ShowPopUpConfirmation(
            null,
            description,
            positiveText,
            negativeText,
            positiveCallback,
            negativeCallback);
    }

    async void ShowLoadingPopUp()
    {
        _popUpService.ShowLoadingCover();
        int waitingSeconds = 2;
        await Task.Delay(waitingSeconds * 1000);
        _popUpService.HideLoadingCover();
    }

    void ShowCallbackPopUp()
    {
        string description = "That's a callback PopUp";
        string positiveText = "nice!";

        _popUpService.ShowPopUpInformation(
            null,
            description,
            positiveText);
    }
}
