// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;
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
        public void SendMessage(ProductUserId remoteUserId, byte[] rawMessage)
        {
            if (remoteUserId.IsValid())
            {
                // Send Message
                SocketId socketId = new SocketId()
                {
                    SocketName = "CHAT"
                };

                SendPacketOptions options = new SendPacketOptions()
                {
                    LocalUserId = EOSManager.Instance.GetProductUserId(),
                    RemoteUserId = remoteUserId,
                    SocketId = socketId,
                    AllowDelayedDelivery = true,
                    Channel = 0,
                    Reliability = PacketReliability.ReliableOrdered,
                    Data = rawMessage,
                };

                Result result = _p2pHandler.SendPacket(ref options);

                switch (result)
                {
                    // ignore list
                    case Result.Success:
                        break;

                    default:
                        {
                            string debugText =
                                "$$$ > ".ToColor(GoodCollors.orange) +
                                "UNEXPECTED message sending result!" + "\n" +
                                "result = " + result.ToString();
                            DebugExtension.DevLogWarning(debugText);
                            break;
                        }
                }

                return;
            }
            else
            {
                DebugExtension.DevLogError(
                    "$ > ".ToColor(GoodCollors.pink) +
                    "INVALID friendId!" + "\n" +
                    "remoteUserId = " + remoteUserId.SerializeObjectToJSON(true));
            }
        }
    }
}
