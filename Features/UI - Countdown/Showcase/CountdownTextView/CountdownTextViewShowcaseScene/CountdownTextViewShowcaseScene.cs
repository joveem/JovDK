// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
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


namespace JovDK.UI.CountDown.Testing.Showcase
{
    public partial class CountdownTextViewShowcaseScene : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        DateTime _deadLineTime;
        float _elapsedTime = 0f;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] CountdownTextView _notNegativeCountdownTextView;
        [SerializeField] CountdownTextView _countdownTextView;
        [SerializeField] CountdownRectView _countdownRectView;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] int _testDeadlineDurationInSeconds = 120;


        // void Awake()
        // {

        // }

        void Start()
        {
            SetInitialState();
        }

        void Update()
        {
            HandleCountDownUpdate(Time.deltaTime);
        }

        // void FixedUpdate()
        // {

        // }
    }
}
