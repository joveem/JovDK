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
using JovDK.UI.PopUp;

// from project
// ...

namespace JovDK.Services
{
    public partial class PopUpService : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        PopUp _loadingCoverPopup = null;



        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Transform _popUpContainer;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] PopUp _informationPopUpPrefab;
        [SerializeField] PopUp _confirmationPopUpPrefab;
        [SerializeField] PopUp _coverPopUpPrefab;


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
}
