// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnitySplashScreen = UnityEngine.Rendering.SplashScreen;

// third
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.App.SplashScreen
{
    public partial class CustomSplashScreen : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Image _logoImage;
        [SerializeField] Image _backgroundImage;
        [SerializeField] CanvasGroup _contentCanvasGroup;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] float _splashDuration = 2f;
        [SerializeField] float _hideFadeDuration = 1f;
        [SerializeField] float _enableClickGapAfterFadeStart = 0.2f;
        [SerializeField] bool _DEBUG_simulateUnitySplashOnEditor = true;


        void Awake()
        {
            bool hasToShowSplashScreen = true;

#if UNITY_EDITOR
            hasToShowSplashScreen = _DEBUG_simulateUnitySplashOnEditor;
#endif

            if (hasToShowSplashScreen)
            {
                ShowContent();
                HideLogo();
            }
            else
                Destroy(gameObject);
        }

        IEnumerator Start()
        {
#if UNITY_EDITOR
            if (_DEBUG_simulateUnitySplashOnEditor)
            {
                UnitySplashScreen.Begin();
                UnitySplashScreen.Draw();
            }
#endif

            while (!UnitySplashScreen.isFinished)
                yield return null;

            ShowLogo();

            yield return new WaitForSeconds(_splashDuration);
            PlayHideContent();

            yield return new WaitForSeconds(_enableClickGapAfterFadeStart);
            _backgroundImage.raycastTarget = false;
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
