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
    public partial class CountdownRectView : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] RectTransform _rectTransform;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] bool _setMaxSizeOnStart = false;
        [SerializeField] float _maxRectSize = 50f;


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
