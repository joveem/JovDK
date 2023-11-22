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
// using Palmmedia.ReportGenerator.Core.Parser.Analysis;

// from project
// ...


namespace JovDK.Networking.Realtime
{
    public partial class EosP2PServer : GenericRealtimeServer
    {

        void HandleIncomingMessages()
        {
            ProductUserId senderId = HandleReceivedMessages();

            if (senderId != null)
            {
                // ...
            }
        }

        ProductUserId HandleReceivedMessages()
        {
            ProductUserId senderId = null;

            if (_p2pHandler == null)
                return null;


            if (!_isConnected)
                return null;

            ReceivePacketOptions options = new ReceivePacketOptions()
            {
                LocalUserId = EOSManager.Instance.GetProductUserId(),
                MaxDataSizeBytes = 4096,
                RequestedChannel = null
            };

            GetNextReceivedPacketSizeOptions getNextReceivedPacketSizeOptions =
                new GetNextReceivedPacketSizeOptions
                {
                    LocalUserId = EOSManager.Instance.GetProductUserId(),
                    RequestedChannel = null
                };

            _p2pHandler.GetNextReceivedPacketSize(ref getNextReceivedPacketSizeOptions, out uint nextPacketSizeBytes);

            byte[] rawData = new byte[nextPacketSizeBytes];
            var dataSegment = new ArraySegment<byte>(rawData);
            Result result = _p2pHandler.ReceivePacket(ref options, out senderId, out SocketId socketId, out byte outChannel, dataSegment, out uint bytesWritten);

            switch (result)
            {
                case Result.Success:
                    {
                        // try deliver the received raw message
                        string debugText =
                            "#> ".ToColor(GoodColors.Orange) +
                            "Message received: senderId=" + senderId + "\n" +
                            "socketId=" + socketId + "\n" +
                            "data=" + Encoding.UTF8.GetString(rawData);
                        DebugExtension.DevLog(debugText);

                        if (senderId.IsValid())
                        {
                            string userId = senderId.ToString();
                            OnReceiveMessage(userId, rawData);
                        }
                        else
                        {
                            DebugExtension.DevLogError("EOS P2PNAT HandleReceivedMessages: ProductUserId senderId is not valid!");
                            return null;
                        }

                        break;
                    }

                // no packets
                case Result.NotFound:
                    return null;

                default:
                    {
                        string debugText =
                            "$$$ > ".ToColor(GoodColors.Orange) +
                            "UNEXPECTED message receiving result!" + "\n" +
                            "result = " + result.ToString();
                        DebugExtension.DevLogWarning(debugText);
                        break;
                    }
            }

            return senderId;
        }
    }
}
