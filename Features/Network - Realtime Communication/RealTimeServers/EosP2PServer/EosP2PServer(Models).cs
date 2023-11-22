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

        public class EosP2PUser : GenericNetworkMember
        {
            public ProductUserId ProductUserId = null;


            public EosP2PUser() { }

            public EosP2PUser(string userId)
            {
                UserId = userId;

                ProductUserId productUserId = ProductUserId.FromString(userId);

                if (productUserId.IsValid())
                    ProductUserId = productUserId;
                else
                {
                    DebugExtension.DevLogWarning(
                        "$$$ > ".ToColor(GoodColors.Pink) +
                        "productUserId IS INVALID!" + "\n" +
                        "productUserId = " +
                        productUserId.ToString());
                }
            }

            public EosP2PUser(string userId, string userName)
            {
                UserId = userId;
                UserName = userName;

                ProductUserId productUserId = ProductUserId.FromString(userId);

                if (productUserId.IsValid())
                    ProductUserId = productUserId;
                else
                {
                    DebugExtension.DevLogWarning(
                        "$$$ > ".ToColor(GoodColors.Pink) +
                        "productUserId IS INVALID!" + "\n" +
                        "productUserId = " +
                        productUserId.ToString());
                }
            }
        }
    }
}
