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
        // TODO: remove this, after inherites from DefaultPanel 
        private void ClosePanel()
        {
            Destroy(gameObject);
        }

        public void SetConfirmationAction(Action confirmationCallback)
        {
            _positiveCallback = confirmationCallback;
        }

        public void SetCancelAction(Action negativeCallback)
        {
            _negativeCallback = negativeCallback;
        }

        public void SetCloseAction(Action closeCallback)
        {
            _closeCallback = closeCallback;
        }

        public void SetPostConfirmationAction(Action confirmationCallback)
        {
            _postPositiveCallback = confirmationCallback;
        }

        public void SetPostCancelAction(Action postNegativeCallback)
        {
            _postNegativeCallback = postNegativeCallback;
        }

        public void SetPostCloseAction(Action postCloseCallback)
        {
            _postCloseCallback = postCloseCallback;
        }

        public void SetTexts(string title, string description)
        {
            if (!String.IsNullOrWhiteSpace(title))
            {
                _titleText.gameObject.SetActive(true);
                _titleText.text = title;
            }
            else
            {
                _titleText.text = "";
                _titleText.gameObject.SetActive(false);
            }

            if (!String.IsNullOrWhiteSpace(description))
            {
                _descriptionText.gameObject.SetActive(true);
                _descriptionText.text = description;
            }
            else
            {
                _descriptionText.text = "";
                _descriptionText.gameObject.SetActive(false);
            }
        }

        public void SetButtonsTexts(string positiveText = null, string negativeText = null, string closeText = null)
        {
            if (!string.IsNullOrWhiteSpace(positiveText))
                _positiveButton.SetTextInButton(positiveText);

            if (!string.IsNullOrWhiteSpace(negativeText))
                _negativeButton.SetTextInButton(negativeText);

            if (!string.IsNullOrWhiteSpace(closeText))
                _closeButton.SetTextInButton(closeText);
        }
    }
}
