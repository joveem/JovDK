// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.UI.CountDown
{
    public partial class CountdownRectView : MonoBehaviour
    {
        void SetInitialState()
        {
            if (_setMaxSizeOnStart)
                _rectTransform.DoIfNotNull(() => _maxRectSize = _rectTransform.sizeDelta.x);
        }
    }
}
