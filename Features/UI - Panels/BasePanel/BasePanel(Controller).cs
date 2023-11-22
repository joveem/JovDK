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
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class BasePanel : MonoBehaviour
{
    public void ClosePanel(Action onFinishCallback = null)
    {
        Action finalOnFinishCallback =
            () =>
            {
                onFinishCallback?.Invoke();
                Destroy(gameObject);
            };

        if (_isShowing)
            PlayHideAnimation(finalOnFinishCallback);
        else
            onFinishCallback?.Invoke();
    }
}
