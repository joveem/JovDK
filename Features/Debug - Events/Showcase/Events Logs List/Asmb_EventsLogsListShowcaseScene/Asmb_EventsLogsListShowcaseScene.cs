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

    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    // bool _dependencies;


    // [Space(5), Header("[ State ]"), Space(10)]

    // bool _state;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] EventLogList _eventLogList;


    // [Space(5), Header("[ Configs ]"), Space(10)]

    // bool _configs;


    // void Awake()
    // {

    // }

    void Start()
    {
        SetupComponent();
    }

    void Update()
    {
        HandleDebugInputs();
    }

    // void FixedUpdate()
    // {

    // }

}
