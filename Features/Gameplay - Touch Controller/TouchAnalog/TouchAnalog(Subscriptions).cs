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
    public partial class TouchAnalog : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        // void SubscribeAllListeners()
        // {

        // }

        // void UnsubscribeAllListeners()
        // {

        // }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            // DebugExtension.DevLog("> ".ToColor(GoodCollors.green) + "OnPointerDown");

            _touchDownPosition = eventData.position;
            ApplyStickPosition(_touchDownPosition);

            OnTouchBegin(_touchDownPosition);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            // DebugExtension.DevLog("> ".ToColor(GoodCollors.orange) + "OnDrag");

            Vector3 touchPosition = eventData.position;
            Vector3 positionDelta = touchPosition - _touchDownPosition;
            float screenFactor = ((float)Screen.height) / 1080f;
            float scaledStickHeight = _stickHeight * screenFactor;

            if (positionDelta.magnitude > (scaledStickHeight / 2f))
                positionDelta = positionDelta.normalized * (scaledStickHeight / 2f);

            Vector3 dragInput = positionDelta / (scaledStickHeight / 2f);

            ApplyCenterStickPosition(_touchDownPosition + positionDelta);
            OnDrag(touchPosition, dragInput);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            // DebugExtension.DevLog("> ".ToColor(GoodCollors.red) + "OnPointerUp");

            ApplyInitialStickPosition();

            Vector3 screenPosition = eventData.position;
            OnTouchFinish(screenPosition);
        }
    }
}
