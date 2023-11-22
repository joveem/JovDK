// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using Epic.OnlineServices;
using PlayEveryWare.EpicOnlineServices;
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
    public partial class EosP2PServer : GenericRealtimeServer
    {

        void SetupComponent()
        {
            _p2pHandler = EOSManager.Instance.GetEOSPlatformInterface().GetP2PInterface();
        }

    }
}
