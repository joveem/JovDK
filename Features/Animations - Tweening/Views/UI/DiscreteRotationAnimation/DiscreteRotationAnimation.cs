// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


/// <summary>
/// An discrete (inverse of "continuos") loopping
/// rotation animation (driven by Coroutine) to
/// objects that is NEVER showed without
/// animation/stopped
/// </summary>
namespace JovDK.Animations.Tweening
{
    public class DiscreteRotationAnimation : LoopAnimation
    {
        Coroutine _rotationCoroutine = null;

        [SerializeField] Vector3 _eulerRotationDirection = new Vector3(0, 0, -15f);
        [SerializeField] float _stepDelay = 0.2f;

        public override void StartAnimation()
        {
            Rotate();
        }

        public override void StopAnimation()
        {
            _rotationCoroutine.DoIfNotNull(
                () => StopCoroutine(_rotationCoroutine));
        }

        void Rotate()
        {
            _rotationCoroutine =
                StartCoroutine(RotationStep(_eulerRotationDirection, _stepDelay));
        }


        IEnumerator RotationStep(Vector3 eulerRotation, float stepDelay)
        {
            yield return new WaitForSeconds(stepDelay);
            ApplyRotation(eulerRotation);
            Rotate();
        }

        void ApplyRotation(Vector3 eulerRotation)
        {
            transform.Rotate(eulerRotation);
        }
    }
}
