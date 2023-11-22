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


public partial class EOSDebugger : MonoBehaviour
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    // bool _dependencies;


    // [Space(5), Header("[ State ]"), Space(10)]

    // bool _state;


    // [Space(5), Header("[ Parts ]"), Space(10)]

    // bool _parts;


    // [Space(5), Header("[ Configs ]"), Space(10)]

    // bool _configs;


    // void Awake()
    // {

    // }

    // void Start()
    // {

    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // DebugExtension.DevLog("[0]");

            // string debugText01 =
            //     "GetProductId = " +
            //     EOSManager.Instance.GetProductId().ToString();
            // DebugExtension.DevLog(debugText01);

            // string debugText02 =
            //     "GetProductUserId = " +
            //     EOSManager.Instance.GetProductUserId().ToString();
            // DebugExtension.DevLog(debugText02);
        }
    }

    // void FixedUpdate()
    // {

    // }

}
