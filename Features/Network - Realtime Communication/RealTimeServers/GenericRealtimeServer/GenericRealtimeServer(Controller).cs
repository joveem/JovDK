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


namespace JovDK.Networking.Realtime
{
    public abstract partial class GenericRealtimeServer
    {
        // connection
        public abstract void Connect(RealtimeServerConnectionParameters connectionParameters);
        public abstract void Disconnect();

        // send message
        public abstract void EmitToUser(string userProductId, byte[] rawMessage);
        public abstract void EmitToUsersList(List<string> usersProductIdsList, byte[] rawMessage);
        public abstract void EmitToRoom(byte[] rawMessage);

        // users caching
        public abstract void UpdateMembersList(List<GenericNetworkMember> membersList);
    }
}
