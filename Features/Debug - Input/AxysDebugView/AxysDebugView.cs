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


namespace JovDK.Debugging.Input
{
    public partial class AxysDebugView : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        float _axysFactor;
        [SerializeField] Vector3 _initialContainerSize = new Vector3(150, 150);


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] RectTransform _axysContainerRect;
        [SerializeField] RectTransform _axysIndicatorRect;
        [SerializeField] TextMeshProUGUI _positionText;
        [SerializeField] TextMeshProUGUI _factorText;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        // void Awake()
        // {

        // }

        void Start()
        {
            SetInitialValues();
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
