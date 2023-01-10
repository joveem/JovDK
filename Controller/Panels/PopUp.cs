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

public class PopUp : MonoBehaviour
{

    [Space(5), Header("[ State ]"), Space(10)]

    Action _positiveCallback;
    Action _negativeCallback;
    Action _closeCallback;
    Action _postPositiveCallback;
    Action _postNegativeCallback;
    Action _postCloseCallback;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] TextMeshProUGUI _titleText, _descriptionText;
    [SerializeField] Button _positiveButton, _negativeButton, _closeButton;


    private void Awake()
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

    private void Start()
    {
        SetupButtons();
    }

    void PositiveButton()
    {
        _positiveCallback?.Invoke();
        _postPositiveCallback?.Invoke();
    }

    void NegativeButton()
    {
        _negativeCallback?.Invoke();
        _postNegativeCallback?.Invoke();
    }

    void CloseButton()
    {
        _closeCallback?.Invoke();
        _postCloseCallback?.Invoke();
    }

    void SetupButtons()
    {
        _positiveButton.SetOnClickIfNotNull(PositiveButton);
        _negativeButton.SetOnClickIfNotNull(NegativeButton);
        _closeButton.SetOnClickIfNotNull(CloseButton);
    }

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
            this._titleText.text = title;

        if (!String.IsNullOrWhiteSpace(description))
            this._descriptionText.text = description;
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

