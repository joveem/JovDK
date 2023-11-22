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
using PlayEveryWare.EpicOnlineServices;
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class EosP2PTestingShowcase : MonoBehaviour
{
    void SubscribeAllListeners()
    {
        GetRealtimeServer().OnConnectionSucceedCallback += OnConnectionSucceed;
        GetRealtimeServer().OnReceiveMessageCallback += OnReceiveMessage;
    }

    void UnsubscribeAllListeners()
    {
        GetRealtimeServer().OnConnectionSucceedCallback -= OnConnectionSucceed;
        GetRealtimeServer().OnReceiveMessageCallback -= OnReceiveMessage;
    }

    void OnConnectionSucceed()
    {
        DebugExtension.DevLog("#> ".ToColor(GoodCollors.pink) + "OnConnectionSucceed");

        ApplyInteractionState();

        ProductUserId localProductUserId = EOSManager.Instance.GetProductUserId();

        if (localProductUserId != null)
            _localUserIdText.DoIfNotNull(() => _localUserIdText.text = _localUserIdPrefix + " " + localProductUserId.ToString().ToShortId());
        else
        {
            string debugText =
                "ERROR Trying to CopyUserId!" + "\n" +
                "localProductUserId IS NULL!" +
                "";
            DebugExtension.DevLogError(debugText);
        }
    }

    void OnReceiveMessage(string userProductId, byte[] rawData)
    {
        string title = "message from " + userProductId.ToShortId();
        string content = Encoding.UTF8.GetString(rawData);
        _eventLogList.AddEventLog(title, content, Color.green);
    }
}
