// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;
using PlayEveryWare.EpicOnlineServices;
using TMPro;
using static PlayEveryWare.EpicOnlineServices.EOSManager;

// from company
using JovDK.Debug;
using JovDK.Services;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Networking.Realtime
{
    public partial class EosP2PServer : GenericRealtimeServer /*, GenericRealtimeServer*/
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        P2PInterface _p2pHandler;
        [SerializeField] PopUpService _popUpService;


        [Space(5), Header("[ State ]"), Space(10)]

        // ProductUserId _localProductUserId = null;
        bool _isConnected = false;
        Dictionary<string, EosP2PUser> _currentRoomUsersById = new Dictionary<string, EosP2PUser>();


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


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
            HandleIncomingMessages();
        }

        // void FixedUpdate()
        // {

        // }
    }
}
