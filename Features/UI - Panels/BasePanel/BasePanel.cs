// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;
using DG.Tweening;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class BasePanel : MonoBehaviour
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    // bool _dependencies;


    [Space(5), Header("[ State ]"), Space(10)]

    bool _isShowing = false;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] Image _fadeBackground;
    [SerializeField] RectTransform _bodyContainer;


    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] Ease _backgroundPanelShowAnimationEase = Ease.OutBack;
    [SerializeField] Ease _backgroundPanelHideAnimationEase = Ease.OutExpo;
    [SerializeField] float _maxFadeOpacity = 0.4f;
    [SerializeField] float _showAnimationDelay = 0.35f;


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
