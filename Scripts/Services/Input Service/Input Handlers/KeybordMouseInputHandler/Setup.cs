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
    public void Setup(InputEmitter inputEmitter)
    {
        _inputEmitter = inputEmitter;
    }
}
