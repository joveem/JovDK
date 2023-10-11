using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.Debug;
using JovDK.LEGACY.Localization;


public class MultiLanguageText : MonoBehaviour
{


    public string textId = "undefined";

    private void Start()
    {

        ApplyText();

    }

    public void ApplyText()
    {

        if (textId == "undefined")
        {
            DebugExtension.DevLogWarning("undefined textId on object \"" + gameObject.name + "\"!");
        }

        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = LocalizationService.GetTextById(textId);
        else if (GetComponent<TextMeshProUGUI>() != null)
            GetComponent<TextMeshProUGUI>().text = LocalizationService.GetTextById(textId);
        else
            DebugExtension.DevLogError("undefined Text / TextMeshProUGUI COMPONENT on object \"" + gameObject.name + "\"!");

    }

}
