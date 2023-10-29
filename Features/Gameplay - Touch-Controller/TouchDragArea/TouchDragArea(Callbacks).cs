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
    public partial class TouchDragArea : MonoBehaviour
    {
        void OnTouchBegin(Vector3 screenPosition)
        {
            OnTouchBeginCallback?.Invoke(screenPosition);
        }

        void OnDrag(Vector3 screenPosition, Vector3 dragInput)
        {
            OnDragAsInputCallback?.Invoke(dragInput);
            OnDragAsScreenPositionCallback?.Invoke(screenPosition);
        }

        void OnTouchFinish(Vector3 screenPosition)
        {
            OnTouchFinishCallback?.Invoke(screenPosition);
        }
    }
}
