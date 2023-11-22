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
    public partial interface IGenericRealtimeServer
    {
        // callbacks
        public Action OnConnectionSucceedCallback { get; set; }
        public Action<string> OnConnectionFailCallback { get; set; }
        public Action<string> OnDisconnectedCallback { get; set; }
        public Action<string, byte[]> OnReceiveMessageCallback { get; set; }

        // connection
        public void Connect(RealtimeServerConnectionParameters connectionParameters);
        public void Disconnect();

        // send message
        public void EmitToUser(string userProductId, byte[] rawMessage);
        public void EmitToUsersList(List<string> usersProductIdsList, byte[] rawMessage);
        public void EmitToRoom(byte[] rawMessage);

        // users caching
        public void UpdateMembersList(List<GenericNetworkMember> membersList);
    }
}
