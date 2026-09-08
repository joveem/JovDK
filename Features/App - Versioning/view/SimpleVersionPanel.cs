// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using R3;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.App.Versioning
{
    public partial class SimpleVersionPanel : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // [SerializeField] bool _state1;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] TextMeshProUGUI _versionText;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] string _versionPrefix = "v";
        const string _monospacePrefix = "<mspace=0.6em>";
        const string _monospaceSufix = "</mspace>";



        #region MonoBehaviour
        void Awake()
        {
            SetInitialState();
        }
        #endregion MonoBehaviour

        #region Controller
        void SetInitialState()
        {
            // DebugExtension.DefaultGenericLog();

            Version version = new Version(Application.version);

            string finalVersion =
                _versionPrefix +
                $"{_monospacePrefix}{version.Major}{_monospaceSufix}" +
                "." +
                $"{_monospacePrefix}{version.Minor}{_monospaceSufix}" +
                "." +
                $"{_monospacePrefix}{version.Build}{_monospaceSufix}" +
                "";

            _versionText.SetTextIfNotNull(finalVersion);
        }
        #endregion Controller
    }
}
