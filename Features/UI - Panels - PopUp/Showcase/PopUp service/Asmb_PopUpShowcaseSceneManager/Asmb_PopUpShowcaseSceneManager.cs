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
using JovDK.Services;

// from project
// ...


public partial class Asmb_PopUpShowcaseSceneManager : MonoBehaviour
{

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    [SerializeField] PopUpService _popUpService;


    // [Space(5), Header("[ State ]"), Space(10)]

    // bool _state;


    [Space(5), Header("[ Parts ]"), Space(10)]

    // information
    [SerializeField] Button _showTitleOnlyPopUpButton;
    [SerializeField] Button _showDescriptionOnlyPopUpButton;
    [SerializeField] Button _showTitleAndDescriptionPopUpButton;
    [SerializeField] Button _showInformationPopUpWithCallbackButton;
    // confirmation
    [SerializeField] Button _showConfirmationPopUpButton;
    [SerializeField] Button _showConfirmationPopUpWithTwoCallbacksButton;
    // loading
    [SerializeField] Button _showLoadingPopUpButton;


    // [Space(5), Header("[ Configs ]"), Space(10)]

    // bool _configs;


    // void Awake()
    // {

    // }

    void Start()
    {
        SetupButtons();
    }

    // void Update()
    // {

    // }

    // void FixedUpdate()
    // {

    // }

}
