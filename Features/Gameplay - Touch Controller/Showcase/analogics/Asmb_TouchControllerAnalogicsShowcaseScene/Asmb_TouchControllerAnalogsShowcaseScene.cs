// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
// ...

// from company
using JovDK.Debugging;
using JovDK.Debugging.Input;
using JovDK.Control.Touch;

// from project
// ...


namespace JovDK.Testing.Showcase
{
    public partial class Asmb_TouchControllerAnalogsShowcaseScene : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] AxysDebugView _leftAnalogDebugView;
        [SerializeField] AxysDebugView _rightDragAreaDebugView;


        [SerializeField] TouchAnalog _leftAnalog;
        [SerializeField] TouchDragArea _rightTouchDragArea;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        void Awake()
        {
            SubscribeAllListeners();
        }

        void Start()
        {
            SetInitialValues();
        }

        void OnDestroy()
        {
            UnsubscribeAllListeners();
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }

    }
}
