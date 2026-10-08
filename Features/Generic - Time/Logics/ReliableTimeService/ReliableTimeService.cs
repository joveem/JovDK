using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using JovDK.SerializingTools.Json;

namespace JovDK.Generic.TimeManagement
{
    public partial class ReliableTimeService : MonoBehaviour
    {
        SampledUtcClock _clock;
        bool _started;
        bool _isInitialized;
        public bool HasRemoteTime { get; private set; }
        public Action OnInitializedCallback;
        const string TimeUrl = "https://www.worldtimeapi.org/api/ip";
        void Awake() => EnsureClock();
        void Start() => SetInitialState();
        void EnsureClock()
        {
            if (_clock != null) return;
            _clock = new SampledUtcClock(() => Time.realtimeSinceStartupAsDouble);
            _clock.SetSample(DateTime.UtcNow);
        }
        public void SetInitialState()
        {
            EnsureClock();
            if (_started) return;
            _started = true;
            StartCoroutine(GetNTPTime());
        }
        IEnumerator GetNTPTime()
        {
            using (var request = UnityWebRequest.Get(TimeUrl))
            {
                request.timeout = RemoteTimeSample.TimeoutSeconds;
                yield return request.SendWebRequest();
                if (RemoteTimeSample.TryRead(request.result == UnityWebRequest.Result.Success,
                    request.downloadHandler == null ? null : request.downloadHandler.text, out var utc))
                { _clock.SetSample(utc); HasRemoteTime = true; }
                if (!HasRemoteTime) Debug.LogWarning("Reliable time unavailable; using local UTC with monotonic elapsed time. Access data is preserved.");
            }
            _isInitialized = true;
            OnInitializedCallback?.Invoke();
        }
        public DateTime ReliableUTCTimeNow() { EnsureClock(); return _clock.UtcNow; }
        public bool IsInitialized() => _isInitialized;
    }
}
