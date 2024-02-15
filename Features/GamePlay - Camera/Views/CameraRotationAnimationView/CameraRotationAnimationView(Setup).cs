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
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.GamePlay.Camera
{
    public partial class CameraRotationAnimationView : MonoBehaviour
    {
        void SetupComponent()
        {
            _baseRigidbody.DoIfNull(() =>
            {
                _baseRigidbody = gameObject.GetComponent<Rigidbody>();
            });
        }
    }
}
