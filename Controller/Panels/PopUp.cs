using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PopUp : MonoBehaviour
{

    public TextMeshProUGUI title, description;
    public Button confirmationButton, cancelButton, closeButton;

    Action confirmationAction, cancelAction, closeAction, postConfirmationAction, postCancelAction, postCloseAction;

    private void Awake()
    {

        confirmationButton.SetTextInButton(LanguageManager.GetTextById("PopUp.Ok"));
        cancelButton.SetTextInButton(LanguageManager.GetTextById("PopUp.Cancel"));
        closeButton.SetTextInButton(LanguageManager.GetTextById("PopUp.Close"));

        title.text = "";
        title.gameObject.SetActive(false);
        description.text = "";
        description.gameObject.SetActive(false);

        postConfirmationAction = () => ClosePanel();
        postCancelAction = () => ClosePanel();
        postCloseAction = () => ClosePanel();

    }

    private void Start()
    {

        if (confirmationButton != null)
        {

            confirmationButton.onClick.AddListener(() =>
            {
                confirmationAction();
                postConfirmationAction();

            });

        }

        if (cancelButton != null)
        {

            cancelButton.onClick.AddListener(() =>
            {
                cancelAction();
                postCancelAction();

            });

        }

        if (closeButton != null)
        {

            closeButton.onClick.AddListener(() =>
            {
                closeAction();
                postCloseAction();

            });

        }

    }

    private void ClosePanel()
    {

        Destroy(gameObject);

    }

    public void SetConfirmationAction(Action _action)
    {

        confirmationAction = _action;

    }
    public void SetCancelAction(Action _action)
    {

        cancelAction = _action;

    }
    public void SetCloseAction(Action _action)
    {

        closeAction = _action;

    }

    public void SetPostConfirmationAction(Action _action)
    {

        postConfirmationAction = _action;

    }
    public void SetPostCancelAction(Action _action)
    {

        postCancelAction = _action;

    }
    public void SetPostCloseAction(Action _action)
    {

        postCloseAction = _action;

    }

    public void SetTexts(string _title, string _description)
    {

        if (!String.IsNullOrWhiteSpace(_title))
        {

            title.gameObject.SetActive(true);
            title.text = _title;

        }
        else
        {

            title.gameObject.SetActive(false);

        }

        if (!String.IsNullOrWhiteSpace(_description))
        {

            description.gameObject.SetActive(true);
            description.text = _description;

        }
        else
        {

            description.gameObject.SetActive(false);

        }

    }

    public void SetButtonsTexts(string _confirmText = null, string _cancelText = null, string _closeText = null)
    {

        if (!string.IsNullOrWhiteSpace(_confirmText))
        {

            confirmationButton.gameObject.SetActive(true);
            confirmationButton.SetTextInButton(_confirmText);

        }

        if (!string.IsNullOrWhiteSpace(_cancelText))
        {

            cancelButton.gameObject.SetActive(true);
            cancelButton.SetTextInButton(_cancelText);

        }

        if (!string.IsNullOrWhiteSpace(_closeText))
        {

            closeButton.gameObject.SetActive(true);
            closeButton.SetTextInButton(_closeText);

        }

    }


}

