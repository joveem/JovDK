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

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        List<EventLogView> _eventLogsList = new List<EventLogView>();


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Button _clearListButton;
        [SerializeField] RectTransform _eventsContainer;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] EventLogView _eventLogPrefab;


        // void Awake()
        // {

        // }

        void Start()
        {
            SetupButtons();
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }

    }
}
