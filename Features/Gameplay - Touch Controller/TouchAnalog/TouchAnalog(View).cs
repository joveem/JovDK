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
using JovDK.Debugging;
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

        void ApplyStickPosition(Vector3 screenPosition)
        {
            _stickBaseImage.rectTransform.position = screenPosition;
        }

        void ApplyCenterStickPosition(Vector3 screenPosition)
        {
            _stickCenterImage.rectTransform.position = screenPosition;
        }

        void ApplyInitialStickPosition()
        {
            _stickBaseImage.rectTransform.position = _stickInitialPosition;
            _stickCenterImage.rectTransform.localPosition = Vector3.zero;
        }
    }
}
