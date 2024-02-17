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
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Debugging.Events
{
    public partial class EventLogList : MonoBehaviour
    {
        public void AddEventLog(
            string title = null,
            string content = null,
            Color color = default)
        {
            _eventsContainer.DoIfNotNull(
                () =>
                {
                    EventLogView eventLog = Instantiate(_eventLogPrefab, _eventsContainer);

                    eventLog.SetTitle(title);
                    eventLog.SetContent(content);
                    eventLog.SetBackgroundColor(color);

                    _eventLogsList.Add(eventLog);
                });
        }

        public void ClearList()
        {
            for (int i = _eventLogsList.Count - 1; i >= 0; i--)
            {
                EventLogView eventLog = _eventLogsList[i];
                _eventLogsList.Remove(eventLog);
                Destroy(eventLog.gameObject);
            }

            foreach (Transform childTransform in _eventsContainer)
                Destroy(childTransform.gameObject);
        }
    }
}
