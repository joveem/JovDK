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

        void CaptureRestPosition()
        {
            if (_hasRestPosition || _stickBaseImage == null || _stickCenterImage == null)
                return;

            // Anchor-relative coordinates preserve the authored offset across canvas resizing.
            _baseRestAnchoredPosition = _stickBaseImage.rectTransform.anchoredPosition3D;
            _centerRestAnchoredPosition = _stickCenterImage.rectTransform.anchoredPosition3D;
            _hasRestPosition = true;
        }

        void ApplyInitialStickPosition()
        {
            // Disable may occur before initialization or while dependencies are being destroyed.
            if (!_hasRestPosition) return;
            if (_stickBaseImage != null)
                _stickBaseImage.rectTransform.anchoredPosition3D = _baseRestAnchoredPosition;
            if (_stickCenterImage != null)
                _stickCenterImage.rectTransform.anchoredPosition3D = _centerRestAnchoredPosition;
        }
    }
}
