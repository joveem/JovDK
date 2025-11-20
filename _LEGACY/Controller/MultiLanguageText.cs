using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.Debugging;
using JovDK.LEGACY.Localization;
using JovDK.SafeActions;


public class MultiLanguageText : MonoBehaviour
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    // [SerializeField] bool _dependencies;


    // [Space(5), Header("[ State ]"), Space(10)]

    // [SerializeField] bool _state;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] TextMeshProUGUI _baseTextMeshProUGUI;


    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] string _textId = UndefinedIdContent;
    const string UndefinedIdContent = "UNDEFINED";



    #region MonoBehaviour
    void Awake()
    {
        SetInitialState();
    }
    #endregion MonoBehaviour

    #region Controller
    void SetInitialState()
    {
        // DebugExtension.DevLog();

        ApplyText();
    }

    public void ApplyText()
    {
        if (_textId == UndefinedIdContent)
        {
            DebugExtension.DevLogWarning("$$> ".ToColor(GoodColors.Orange), "Undefined textId on object \"", gameObject.name, "\"!");
            return;
        }

        _baseTextMeshProUGUI.DoIfNotNull(() =>
        {
            _baseTextMeshProUGUI.text = LocalizationService.GetTextById(_textId);
        });
    }
    #endregion Controller
}
