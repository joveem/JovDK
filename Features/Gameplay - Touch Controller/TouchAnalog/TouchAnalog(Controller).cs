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
using UnityEngine.EventSystems;

// from project
// ...


namespace JovDK.Control.Touch
{
    public partial class TouchAnalog : MonoBehaviour
    {
        void SetInitialValue()
        {
            _stickInitialPosition = _stickBaseImage.rectTransform.position;
            _stickHeight = _stickBaseImage.rectTransform.sizeDelta.y;
            CanvasScaler stickCanvasScaler = _stickBaseImage.rectTransform.GetComponentInParent<CanvasScaler>();
            stickCanvasScaler.DoIfNotNull(() => _canvasHeight = stickCanvasScaler.referenceResolution.y);
        }
    }
}
