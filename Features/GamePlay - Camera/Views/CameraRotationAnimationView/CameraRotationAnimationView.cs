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


namespace JovDK.GamePlay.Camera
{
    public partial class CameraRotationAnimationView : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] Animator _animator;


        [Space(5), Header("[ State ]"), Space(10)]

        Vector3 _lastPosition = default;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] Rigidbody _baseRigidbody;
        [SerializeField] float _velocityMultiplierFactor = 1f;


        // void Awake()
        // {

        // }

        void Start()
        {
            SetupComponent();
        }

        // void Update()
        // {

        // }

        void FixedUpdate()
        {
            HandleAnimation(Time.fixedDeltaTime);
        }
    }
}
