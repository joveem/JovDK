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


namespace JovDK.UI.CountDown
{
    public partial class CountdownTextView : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        Color _initialColor = default;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] TextMeshProUGUI _text;


        [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] int _timeFractionAmount = 2; // TODO
        [SerializeField] char _separatorCharacter = ':';
        [SerializeField] TimeFraction _maxTimeFraction = TimeFraction.Minute;
        // [SerializeField] TimeFraction _minTimeFraction = TimeFraction.Second; // TODO


        // void Awake()
        // {

        // }

        void Start()
        {
            SetInitialState();
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
