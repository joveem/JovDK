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


namespace JovDK.GamePlay.Camera
{
    public partial class CameraRotationAnimationView : MonoBehaviour
    {
        void HandleAnimation(float deltaTime)
        {
            _baseRigidbody.DoIfNotNull(() =>
            {
                Vector3 lastPosition = _lastPosition;
                Vector3 realVelocity = (_baseRigidbody.position - lastPosition) / deltaTime;
                float velocity = realVelocity.magnitude * _velocityMultiplierFactor;
                _lastPosition = _baseRigidbody.position;

                ApplyVelocity(velocity);
            });
        }

        public void SetVelocityMultiplierFactor(float velocityMultiplierFactor)
        {
            _velocityMultiplierFactor = velocityMultiplierFactor;
        }
    }
}
