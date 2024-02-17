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
using JovDK.GamePlay;

// from project
// ...


namespace JovDK.GamePlay.Testing.Showcase
{
    public partial class Asmb_CameraShakeViewShowcaseScene : MonoBehaviour
    {
        void HandleDEBUGInputs()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _cameraShakeView.SetShakeForceFactor(1f);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                _cameraShakeView.SetShakeForceFactor(0.75f);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                _cameraShakeView.SetShakeForceFactor(0.5f);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                _cameraShakeView.SetShakeForceFactor(0.25f);

            if (Input.GetKeyDown(KeyCode.Alpha5))
                _cameraShakeView.SetShakeForceFactor(0.1f);
        }
    }
}
