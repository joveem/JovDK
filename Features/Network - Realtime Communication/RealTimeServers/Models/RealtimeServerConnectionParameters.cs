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
    public class RealtimeServerConnectionParameters
    {
        public string ServerType = "UNDEFINED";

        // auth
        public string LocalUserId = "UNDEFINED";
        public string LocalUserName = "UNDEFINED";
        public string LocalUserCredential = "UNDEFINED";

        public string RoomName = "UNDEFINED";
        public List<GenericNetworkMember> NetworkMembersList = new List<GenericNetworkMember>();
    }

    public class GenericNetworkMember
    {
        public string UserId = "UNDEFINED";
        public string UserName = "UNDEFINED";

        public GenericNetworkMember() { }

        public GenericNetworkMember(string userId)
        {
            UserId = userId;
        }

        public GenericNetworkMember(
            string userId,
            string username)
        {
            UserId = userId;
            UserName = username;
        }
    }
}
