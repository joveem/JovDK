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
            UnityWebRequest webRequest = UnityWebRequest.Get(_worldTimeApiUrl);
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // string debugText =
                //     "$ > ".ToColor(GoodColors.Red) +
                //     "ERROR trying to GetNTPTime!" + "\n" +
                //     "webRequest.result = " + webRequest.result.ToString() + "\n" +
                //     "webRequest.error = " + "\n" +
                //     webRequest.error + "\n" +
                //     "";
                // Debug.LogError(debugText);

                if (webRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError("Data Processing Error: " + webRequest.error);
                }
                else
                {
                    Debug.LogError("Network Error: " + "\"" + webRequest.error + "\"");
                    Debug.LogError("Received: " + "\"" + webRequest.downloadHandler.text + "\"");
                    Debug.LogError(
                        "downloadHandler.data = " + "\n" +
                        webRequest.downloadHandler.data.SerializeObjectToJSON(true) + "\n" +
                        "");
                }

                _startUTCTime = DateTime.UtcNow;
            }
            else
            {
                string rawJsonResponse = webRequest.downloadHandler.text;
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

            value = _startUTCTime.AddSeconds(Time.time);

            return value;
        }
    }
}
