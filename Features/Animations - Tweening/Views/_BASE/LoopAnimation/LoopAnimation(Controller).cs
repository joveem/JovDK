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
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Animations.Tweening
{
    public abstract partial class LoopAnimation : MonoBehaviour, ILoopAnimation
    {
        void SaveInitialTransfortValues()
        {
            _hasInitialTransformValues = true;
            _initialPosition = transform.localPosition;
            _initialScale = transform.localScale;
            _initialRotation = transform.localRotation;
        }

        void ApplyInitialTransfortValues()
        {
            transform.localPosition = _initialPosition;
            transform.localScale = _initialScale;
            transform.localRotation = _initialRotation;
        }
    }
}
