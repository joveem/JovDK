// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
// ...

// from company
using JovDK.Debugging;
using JovDK.Debugging.Input;
using JovDK.Control.Touch;

// from project
// ...


namespace JovDK.Testing.Showcase
{
    public partial class Asmb_TouchControllerAnalogsShowcaseScene : MonoBehaviour
    {
        void SubscribeAllListeners()
        {
            // left debug view <- left analog
            _leftAnalog.OnTouchBeginCallback += OnLeftAnalogTouchBegin;
            _leftAnalog.OnDragAsInputCallback += OnLeftAnalogDragInput;
            _leftAnalog.OnTouchFinishCallback += OnLeftAnalogTouchFinish;

            // right debug view <- right drag area
            _rightTouchDragArea.OnTouchBeginCallback += OnRightDragAreaTouchBegin;
            _rightTouchDragArea.OnDragAsInputCallback += OnRightDragAreaDragAsInput;
            _rightTouchDragArea.OnTouchFinishCallback += OnRightDragAreaTouchFinish;
        }

        void UnsubscribeAllListeners()
        {
            // left debug view <- left analog
            _leftAnalog.OnTouchBeginCallback -= OnLeftAnalogTouchBegin;
            _leftAnalog.OnDragAsInputCallback -= OnLeftAnalogDragInput;
            _leftAnalog.OnTouchFinishCallback -= OnLeftAnalogTouchFinish;

            // right debug view <- right drag area
            _rightTouchDragArea.OnTouchBeginCallback -= OnRightDragAreaTouchBegin;
            _rightTouchDragArea.OnDragAsInputCallback -= OnRightDragAreaDragAsInput;
            _rightTouchDragArea.OnTouchFinishCallback -= OnRightDragAreaTouchFinish;
        }

        #region Left Analog
        void OnLeftAnalogTouchBegin(Vector3 screenPosition)
        {
            DebugExtension.DevLog("screenPosition = " + screenPosition.ToString());

            _leftAnalogDebugView.SetInputValue(Vector3.zero);
        }

        void OnLeftAnalogDragInput(Vector3 dragInput)
        {
            _leftAnalogDebugView.SetInputValue(dragInput);
        }

        void OnLeftAnalogTouchFinish(Vector3 screenPosition)
        {
            DebugExtension.DevLog("screenPosition = " + screenPosition.ToString());

            _leftAnalogDebugView.SetInputValue(Vector3.zero);
        }
        #endregion Left Analog

        #region Right Analog

        void OnRightDragAreaTouchBegin(Vector3 screenPosition)
        {
            DebugExtension.DevLog("screenPosition = " + screenPosition.ToString());

            _rightDragAreaDebugView.SetInputValue(Vector3.zero);
        }

        void OnRightDragAreaDragAsInput(Vector3 dragInput)
        {
            _rightDragAreaDebugView.SetInputValue(dragInput);
        }

        void OnRightDragAreaTouchFinish(Vector3 screenPosition)
        {
            DebugExtension.DevLog("screenPosition = " + screenPosition.ToString());

            _rightDragAreaDebugView.SetInputValue(Vector3.zero);
        }
        #endregion Right Analog
    }
}
