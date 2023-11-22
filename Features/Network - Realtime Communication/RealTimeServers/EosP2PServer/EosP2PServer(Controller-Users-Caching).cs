// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using Epic.OnlineServices;
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
        ProductUserId GetProductUserId(string userId)
        {
            ProductUserId value = null;

            EosP2PUser user = null;
            bool hasUser = _currentRoomUsersById.TryGetValue(userId, out user);

            if (hasUser)
                value = user.ProductUserId;

            if (value == null)
            {
                string debugText =
                    "ERROR Trying to GetProductUserId" + "\n" +
                    "hasUser = " + hasUser.ToString() + "\n" +
                    "userId = " + userId.TextIfIsNull("NULL" + userId) +
                    "";
                DebugExtension.DevLogWarning(debugText);
            }

            return value;
        }
    }
}
