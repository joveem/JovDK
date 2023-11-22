// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;
using PlayEveryWare.EpicOnlineServices;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class EosP2PTestingShowcase : MonoBehaviour
{

    void ApplyAuthState()
    {
        // enable
        _authContainer.SetActiveIfNotNull(true);
        // disable
        _interactionContainer.SetActiveIfNotNull(false);
    }

    void ApplyInteractionState()
    {
        // enable
        _interactionContainer.SetActiveIfNotNull(true);
        // disable
        _authContainer.SetActiveIfNotNull(false);
    }
}
