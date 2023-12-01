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
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;
using JovDK.GamePlay;

// from project
// ...


namespace JovDK.GamePlay.Testing.Showcase
{
    public partial class Asmb_CameraShakeViewShowcaseScene : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] CameraShakeView _cameraShakeView;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        // void Awake()
        // {

        // }

        // void Start()
        // {

        // }

        void Update()
        {
#if UNITY_EDITOR
            HandleDEBUGInputs();
#endif
        }
    }
}
