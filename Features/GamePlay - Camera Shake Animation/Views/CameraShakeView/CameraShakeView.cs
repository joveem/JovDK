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


namespace JovDK.GamePlay
{
    public partial class CameraShakeView : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        float _lastAnimatorShakeForceFact = 0f;
        float _currentShakeForceFact = 0f;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Animator _shakeAnimator;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] float _shakeForceDeacreaseFactor = 2f;


        // void Awake()
        // {

        // }

        // void Start()
        // {

        // }

        // void Update()
        // {

        // }

        void FixedUpdate()
        {
            HandleShakeFixedUpdate();
        }

    }
}
