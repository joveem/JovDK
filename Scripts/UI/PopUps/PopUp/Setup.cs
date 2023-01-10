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

// from project
// ...


public partial class PopUp : MonoBehaviour
{
    void SetupComponent()
    {
        _positiveButton.SetTextInButton(LanguageManager.GetTextById("PopUp.Ok"));
        _negativeButton.SetTextInButton(LanguageManager.GetTextById("PopUp.Cancel"));
        _closeButton.SetTextInButton(LanguageManager.GetTextById("PopUp.Close"));

        _titleText.text = "";
        _titleText.gameObject.SetActive(false);
        _descriptionText.text = "";
        _descriptionText.gameObject.SetActive(false);

        _postPositiveCallback = () => ClosePanel();
        _postNegativeCallback = () => ClosePanel();
        _postCloseCallback = () => ClosePanel();
    }
}
