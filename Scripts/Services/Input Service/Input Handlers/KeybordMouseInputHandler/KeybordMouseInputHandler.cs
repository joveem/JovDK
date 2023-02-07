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
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Services.Input;

// from project
// ...


public partial class KeybordMouseInputHandler : MonoBehaviour
{

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    InputEmitter _inputEmitter;



    [Space(5), Header("[ State ]"), Space(10)]

    bool _state;


    [Space(5), Header("[ Parts ]"), Space(10)]

    bool _parts;


    [Space(5), Header("[ Configs ]"), Space(10)]

    bool _configs;


    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }

}
