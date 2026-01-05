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
using DG.Tweening;
using R3;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Animations.Tools
{
    public static partial class AnimationTools
    {
        #region Controller
        public static int MillisecondsBySeconds(float seconds)
        {
            int value = 0;

            float Milliseconds = seconds * 1000f;
            value = (int)Milliseconds;

            return value;
        }
        #endregion Controller
    }
}
