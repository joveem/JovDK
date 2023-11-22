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
    public partial class TouchDragArea : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
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

            Vector3 screenPosition = eventData.position;
            OnTouchBegin(screenPosition);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            // DebugExtension.DevLog("> ".ToColor(GoodCollors.orange) + "OnDrag");

            Vector3 screenPosition = eventData.position;
            Vector3 positionDelta = eventData.delta;
            float screenWidth = Screen.width;
            Vector3 dragInput = positionDelta / screenWidth;

            OnDrag(screenPosition, dragInput);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            // DebugExtension.DevLog("> ".ToColor(GoodCollors.red) + "OnPointerUp");

            Vector3 screenPosition = eventData.position;
            OnTouchFinish(screenPosition);
        }
    }
}