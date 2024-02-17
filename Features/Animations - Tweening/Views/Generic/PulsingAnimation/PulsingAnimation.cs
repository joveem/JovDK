// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Animations.Tweening
{
    public partial class PulsingAnimation : TweenLoopAnimation
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] float _minScale = 0.9f;
        [SerializeField] float _maxScale = 1.1f;
        float _horizontalScaleIncreaseDuration = 0.3f;
        float _horizontalScaleDeacreaseDuration = 0.6f;
        float _verticalScaleIncreaseDuration = 0.3f;
        float _verticalScaleDeacreaseDuration = 0.6f;
        float _verticalMoveUpDuration = 0.3f;
        float _verticalMoveDowDuration = 0.6f;
        [SerializeField] float _veritacalMoveDistance = 20;


        // void Awake()
        // {

        // }

        // void Start()
        // {

        // }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
