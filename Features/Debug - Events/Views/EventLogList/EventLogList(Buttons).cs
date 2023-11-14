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

// from project
// ...


namespace JovDK.Debug.Events
{
    public partial class EventLogList : MonoBehaviour
    {
        void SetupButtons()
        {
            _clearListButton.SetOnClickIfNotNull(ClearListButton);
        }

        void ClearListButton()
        {
            ClearList();
        }
    }
}
