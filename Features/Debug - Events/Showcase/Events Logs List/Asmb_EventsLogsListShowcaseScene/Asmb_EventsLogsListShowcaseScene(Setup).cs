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
using JovDK.Debugging.Events;

// from project
// ...


public partial class Asmb_EventsLogsListShowcaseScene : MonoBehaviour
{

    void SetupComponent()
    {
        _eventLogList.ClearList();
    }

}
