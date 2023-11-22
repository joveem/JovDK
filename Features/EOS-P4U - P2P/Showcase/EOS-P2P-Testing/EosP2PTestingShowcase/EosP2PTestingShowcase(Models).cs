// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;
using PlayEveryWare.EpicOnlineServices;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class EosP2PTestingShowcase : MonoBehaviour
{

    [Serializable]
    public class GenericRealtimeMessage
    {
        public string MessageType = "UNDEFINED";
        public int[] IntegerContent = new int[] { };
        public string[] TextContent = new string[] { };
        public float[] RealContent = new float[] { };
        public Vector3[] PositionContent = new Vector3[] { };
    }

}
