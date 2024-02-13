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
    public partial class TouchAnalog : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        Vector3 _stickInitialPosition;
        float _stickHeight = 100f;
        float _canvasHeight = 1080f;

        Vector3 _touchDownPosition;


        /// <summary>
        /// ...
        /// </summary>
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
        /// <summary>
        /// ...
        /// </summary>
        public Action<Vector3> OnTouchFinishCallback = null;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Image _stickBaseImage;
        [SerializeField] Image _stickCenterImage;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        void Awake()
        {
            SetupComponent();
        }

        void Start()
        {
            SetInitialValue();
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
