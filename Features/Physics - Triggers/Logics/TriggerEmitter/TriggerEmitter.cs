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


namespace JovDK.Physics.Triggers
{
    public partial class TriggerEmitter : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        public Action<Collider> OnTriggerEnterCallback = null;
        public Action<Collider> OnTriggerExitCallback = null;
        public Action<Collider> OnTriggerStayCallback = null;


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

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
