// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using Newtonsoft.Json;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Generic.TimeManagement
{
    public class WorldTimeResponse
    {
        // [JsonProperty("abbreviation")] public string abbreviation { get; set; }
        // [JsonProperty("client_ip")] public string client_ip { get; set; }
        [JsonProperty("datetime")] public DateTime DateTime { get; set; }
        // [JsonProperty("day_of_week")] public int day_of_week { get; set; }
        // [JsonProperty("day_of_year")] public int day_of_year { get; set; }
        // [JsonProperty("dst")] public bool dst { get; set; }
        // [JsonProperty("dst_from")] public object dst_from { get; set; }
        // [JsonProperty("dst_offset")] public int dst_offset { get; set; }
        // [JsonProperty("dst_until")] public object dst_until { get; set; }
        // [JsonProperty("raw_offset")] public int raw_offset { get; set; }
        // [JsonProperty("timezone")] public string timezone { get; set; }
        // [JsonProperty("unixtime")] public int unixtime { get; set; }
        // [JsonProperty("utc_datetime")] public DateTime utc_datetime { get; set; }
        // [JsonProperty("utc_offset")] public string utc_offset { get; set; }
        // [JsonProperty("week_number")] public int week_number { get; set; }
    }
}
