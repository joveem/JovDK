// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Animations.Tweening
{
    public partial class TweenLoopAnimation : LoopAnimation
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        protected List<Sequence> _sequencesList = new List<Sequence>();


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] protected Ease _fastEase = Ease.InOutCirc;
        [SerializeField] protected Ease _slowEase = Ease.InOutCirc;


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
