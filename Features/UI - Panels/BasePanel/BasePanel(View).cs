// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;
using DG.Tweening;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class BasePanel : MonoBehaviour
{
    public void ShowPanelInstantaneously()
    {
        _isShowing = true;

        gameObject.SetActive(true);
        _fadeBackground.SetActiveIfNotNull(true);
        _bodyContainer.SetActiveIfNotNull(true);

        if (_fadeBackground != null)
        {
            Color fadeColor = _fadeBackground.color;
            fadeColor.a = _maxFadeOpacity;
            _fadeBackground.color = fadeColor;
        }

        _bodyContainer.localScale = Vector3.one;
    }

    public void HidePanelInstantaneously()
    {
        _isShowing = false;

        _fadeBackground.SetActiveIfNotNull(false);
        _bodyContainer.SetActiveIfNotNull(false);
        gameObject.SetActive(false);

        if (_fadeBackground != null)
        {
            Color fadeColor = _fadeBackground.color;
            fadeColor.a = 0f;
            _fadeBackground.color = fadeColor;
        }

        _bodyContainer.localScale = Vector3.zero;
    }
}
