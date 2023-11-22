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

    void SetupComponent()
    {

    }

    void ApplyInitialState()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _authToolAddressInputField.SetActiveIfNotNull(false);
        _usernameInputField.SetActiveIfNotNull(false);
#endif

        _localUserIdText.DoIfNotNull(() => _localUserIdText.text = _localUserIdPrefix + "(...)");
        _eventLogList.DoIfNotNull(() => _eventLogList.ClearList());

        ApplyAuthState();
    }

}
