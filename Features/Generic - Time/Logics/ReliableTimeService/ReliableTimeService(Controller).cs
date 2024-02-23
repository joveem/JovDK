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
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Generic.TimeManagement
{
    public partial class ReliableTimeService : MonoBehaviour
    {
        public void SetInitialState()
        {
            _startUTCTime = DateTime.UtcNow;
            StartCoroutine(GetNTPTime());
        }

        IEnumerator GetNTPTime()
        {
            UnityWebRequest www = UnityWebRequest.Get(_worldTimeApiUrl);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to GetNTPTime!" + "\n" +
                    "www.result = " + www.result.ToString() + "\n" +
                    "www.error = " + "\n" +
                    www.error + "\n" +
                    "";
                Debug.LogError("Erro na requisição NTP: " + www.error);

                _startUTCTime = DateTime.UtcNow;
            }
            else
            {
                string rawJsonResponse = www.downloadHandler.text;
                WorldTimeResponse worldTimeResponse = rawJsonResponse.DeserializeJsonToObject<WorldTimeResponse>();

                Debug.Log("response = " + "\n" + rawJsonResponse);
                _startUTCTime = worldTimeResponse.DateTime.ToUniversalTime();
                Debug.Log("UTC now =  " + "\n" + _startUTCTime);
            }

            _isInitialized = true;
            OnInitialized();
        }

        public DateTime ReliableUTCTimeNow()
        {
            DateTime value;

            value = _startUTCTime.AddMinutes(Time.time);

            return value;
        }
    }
}
