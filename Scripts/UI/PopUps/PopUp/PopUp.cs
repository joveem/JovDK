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

// from project
// ...


public partial class PopUp : MonoBehaviour
{

    [Space(5), Header("[ State ]"), Space(10)]

    Action _positiveCallback;
    Action _negativeCallback;
    Action _closeCallback;
    Action _postPositiveCallback;
    Action _postNegativeCallback;
    Action _postCloseCallback;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] TextMeshProUGUI _titleText, _descriptionText;
    [SerializeField] Button _positiveButton, _negativeButton, _closeButton;


    private void Awake()
    {
        SetupComponent();
    }

    private void Start()
    {
        SetupButtons();
    }

}
