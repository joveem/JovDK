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


namespace JovDK.Debugging.Events
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
