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

// from project
// ...


namespace JovDK.Animations.Tweening
{
    public abstract partial class LoopAnimation : MonoBehaviour, ILoopAnimation
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        protected bool _hasInitialTransformValues = false;
        protected Vector3 _initialPosition = new Vector3();
        protected Vector3 _initialScale = new Vector3();
        protected Quaternion _initialRotation = new Quaternion();


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

        void OnEnable()
        {
            if (_hasInitialTransformValues)
                ApplyInitialTransfortValues();
            else
                SaveInitialTransfortValues();

            StartAnimation();
        }

        void OnDisable()
        {
            StopAnimation();
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
