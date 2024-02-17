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
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Generic.SpatialUI.Testing.Showcase
{
    public partial class Asmb_SpatialUIHandlerShowcaseScene : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] SpatialUIHandler _spatialUIHandler;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] RectTransform _cubeHudTransform;
        [SerializeField] RectTransform _sphereHudTransform;

        [SerializeField] Transform _cubeTransform;
        [SerializeField] Transform _sphereTransform;
        [SerializeField] Transform _sphereRotationPivot;

        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


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
