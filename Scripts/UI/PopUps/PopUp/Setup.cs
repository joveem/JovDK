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

namespace JovDK.UI.PopUp
{
    public partial class PopUp : MonoBehaviour
    {
        void SetupComponent()
        {
            // TODO: REVIEW THIS
            // TODO: check if it's text setup is needed, if
            // TODO: the localization handling will not bee
            // TODO: on the text component
            // _positiveButton.SetTextInButton(LocalizationService.GetTextById("PopUp.Ok"));
            // _negativeButton.SetTextInButton(LocalizationService.GetTextById("PopUp.Cancel"));
            // _closeButton.SetTextInButton(LocalizationService.GetTextById("PopUp.Close"));

            _titleText.text = "";
            _titleText.gameObject.SetActive(false);
            _descriptionText.text = "";
            _descriptionText.gameObject.SetActive(false);

            _postPositiveCallback = () => ClosePanel();
            _postNegativeCallback = () => ClosePanel();
            _postCloseCallback = () => ClosePanel();
        }
    }
}
