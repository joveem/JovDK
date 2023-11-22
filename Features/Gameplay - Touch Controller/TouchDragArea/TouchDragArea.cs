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
using JovDK.Debug.Input;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using UnityEngine.EventSystems;

// from project
// ...


namespace JovDK.Control.Touch
{
    public partial class TouchDragArea : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        public Action<Vector3> OnTouchBeginCallback = null;
        /// <summary>
        /// The touch delta position of a
        /// drag as a nomalized vector
        /// (1f == Screen WIDTH)
        /// </summary>
        public Action<Vector3> OnDragAsInputCallback = null;
        /// <summary>
        /// The touch position of a drag
        /// </summary>
        public Action<Vector3> OnDragAsScreenPositionCallback = null;
        public Action<Vector3> OnTouchFinishCallback = null;


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
