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

// from project
// ...


namespace JovDK.Debugging.Input
{
    public partial class AxysDebugView : MonoBehaviour
    {
        void SetInitialValues()
        {
            _initialContainerSize = _axysContainerRect.sizeDelta;
        }

        public void SetAxysFactor(float axysFactor)
        {
            _axysFactor = axysFactor;
            _factorText.text = "factor=" + _axysFactor.ToString("0.##");
        }

        public void SetInputValue(Vector3 inputValue)
        {
            _positionText.text =
                "x=" + inputValue.x.ToString("0.##") + "\n" +
                "y=" + inputValue.y.ToString("0.##");

            Vector3 finalPosition = inputValue / _axysFactor;

            finalPosition =
                new Vector3(
                    finalPosition.x * (_initialContainerSize.x / 2f),
                    finalPosition.y * (_initialContainerSize.y / 2f),
                    finalPosition.z * (_initialContainerSize.z / 2f));

            _axysIndicatorRect.localPosition = finalPosition;
        }
    }
}
