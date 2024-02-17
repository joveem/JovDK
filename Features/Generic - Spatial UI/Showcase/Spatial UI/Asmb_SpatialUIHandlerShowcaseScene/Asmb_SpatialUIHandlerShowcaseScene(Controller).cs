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


namespace JovDK.Generic.SpatialUI.Testing.Showcase
{
    public partial class Asmb_SpatialUIHandlerShowcaseScene : MonoBehaviour
    {
        void SetInitialState()
        {
            PlaySphereRotationAnimation();
            RegisterSpatialUIItems();
        }

        void PlaySphereRotationAnimation()
        {
            Tween sphereTween =
                _sphereRotationPivot.DORotate(
                    new Vector3(0f, 360, 0f),
                    2f,
                    RotateMode.FastBeyond360).SetEase(Ease.Linear);

            sphereTween.SetLoops(-1);
        }

        void RegisterSpatialUIItems()
        {
            _spatialUIHandler.DoIfNotNull(() =>
            {
                Vector3 positionDelta = Vector3.up * 0.8f;
                _spatialUIHandler.RegisterSpatialUIItems(_sphereTransform, _sphereHudTransform, positionDelta);
                _spatialUIHandler.RegisterSpatialUIItems(_cubeTransform, _cubeHudTransform, positionDelta);
            });
        }
    }
}
