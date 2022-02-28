using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.Debug;

public class MultiLanguageText : MonoBehaviour
{


    public string textId = "undefined";

    private void Start()
    {

        if (textId == "undefined")
        {
            DebugExtension.DevLogWarning("undefined textId on object \"" + gameObject.name + "\"!");
        }

        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = LanguageManager.GetTextById(textId);
        else if (GetComponent<TextMeshProUGUI>() != null)
            GetComponent<TextMeshProUGUI>().text = LanguageManager.GetTextById(textId);
        else
            DebugExtension.DevLogWarning("undefined TEXT COMPONENT on object \"" + gameObject.name + "\"!");


    }

}
