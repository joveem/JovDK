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


namespace JovDK.GamePlay
{
    public partial class CameraShakeView : MonoBehaviour
    {
        public void SetShakeForceFactor(float shakeForceFactor)
        {
            _currentShakeForceFact = shakeForceFactor;
            HandleAnimatorForceFactorUpdate();
        }

        void HandleShakeFixedUpdate()
        {
            _currentShakeForceFact -= _shakeForceDeacreaseFactor * Time.fixedDeltaTime * _currentShakeForceFact;
            _currentShakeForceFact = Mathf.Clamp(_currentShakeForceFact, 0f, 10f);
            HandleAnimatorForceFactorUpdate();
        }

        void HandleAnimatorForceFactorUpdate()
        {
            if (_currentShakeForceFact != _lastAnimatorShakeForceFact)
            {
                _shakeAnimator.SetFloat("shake-force-factor", _currentShakeForceFact);
                _lastAnimatorShakeForceFact = _currentShakeForceFact;
            }
        }
    }
}
