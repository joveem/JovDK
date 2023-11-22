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
    public abstract partial class GenericRealtimeServer
    {
        protected void OnConnectionSucceed()
        {
            ((IGenericRealtimeServer)this).OnConnectionSucceedCallback?.Invoke();
        }

        protected void OnConnectionFail(string reason)
        {
            ((IGenericRealtimeServer)this).OnConnectionFailCallback?.Invoke(reason);
        }

        protected void OnDisconnected(string reason)
        {
            ((IGenericRealtimeServer)this).OnDisconnectedCallback?.Invoke(reason);
        }

        protected void OnReceiveMessage(string senderId, byte[] rawMessage)
        {
            ((IGenericRealtimeServer)this).OnReceiveMessageCallback?.Invoke(senderId, rawMessage);
        }
    }
}
