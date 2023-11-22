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


namespace JovDK.Networking.Realtime
{
    public abstract partial class GenericRealtimeServer : MonoBehaviour, IGenericRealtimeServer
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        bool _state;
        Action IGenericRealtimeServer.OnConnectionSucceedCallback { get; set; }
        Action<string> IGenericRealtimeServer.OnConnectionFailCallback { get; set; }
        Action<string> IGenericRealtimeServer.OnDisconnectedCallback { get; set; }
        Action<string, byte[]> IGenericRealtimeServer.OnReceiveMessageCallback { get; set; }


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

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
