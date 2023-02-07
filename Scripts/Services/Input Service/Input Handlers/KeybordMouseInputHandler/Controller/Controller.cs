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
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Services.Input;

// from project
// ...


public partial class KeybordMouseInputHandler : MonoBehaviour
{
    void EmitInputEvent(
        InputType inputType,
        InputSubType inputSubType,
        float value = -2f)
    {
        InputEvent inputEvent = new InputEvent();

        inputEvent.InputType = inputType;
        inputEvent.InputSubType = inputSubType;
        inputEvent.Value = value;

        _inputEmitter.InputCallback?.Invoke(inputEvent);
    }
}
