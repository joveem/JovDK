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

        public override void Connect(RealtimeServerConnectionParameters connectionParameters)
        {
            TryToAuthenticate(connectionParameters.LocalUserName, connectionParameters.LocalUserCredential);
        }

        public override void Disconnect()
        {

        }

        public override void EmitToUser(string userProductId, byte[] rawMessage)
        {
            ProductUserId productUserId = GetProductUserId(userProductId);
            SendMessage(productUserId, rawMessage);
        }

        public override void EmitToUsersList(List<string> usersProductIdsList, byte[] rawMessage)
        {
            foreach (string userId in usersProductIdsList)
                EmitToUser(userId, rawMessage);
        }

        public override void EmitToRoom(byte[] rawMessage)
        {
            foreach (KeyValuePair<string, EosP2PUser> pair in _currentRoomUsersById)
            {
                EosP2PUser user = pair.Value;
                ProductUserId productUserId = user.ProductUserId;
                SendMessage(productUserId, rawMessage);
            }
        }

        public override void UpdateMembersList(List<GenericNetworkMember> membersList)
        {
            _currentRoomUsersById = new Dictionary<string, EosP2PUser>();

            foreach (GenericNetworkMember member in membersList)
            {
                member.DoIfNotNull(() =>
                {
                    EosP2PUser eosMember = new EosP2PUser(member.UserId, member.UserName);
                    bool success = _currentRoomUsersById.TryAdd(member.UserId, eosMember);

                    if (!success)
                    {
                        DebugExtension.DevLogError(
                            "$ > ".ToColor(GoodColors.Pink) +
                            "ERROR trying to UpdateMembersList!" + "\n" +
                            "member.UserId = " + member.UserId);
                    }
                });
            }
        }

    }
}
