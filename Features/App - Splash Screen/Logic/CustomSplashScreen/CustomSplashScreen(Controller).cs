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
using DG.Tweening;

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


        void ShowLogo()
        {
            _logoImage.SetActiveIfNotNull(true);
        }

        void HideLogo()
        {
            _logoImage.SetActiveIfNotNull(false);
        }

        void ShowContent()
        {
            _contentCanvasGroup.DoIfNotNull(() =>
            {
                _contentCanvasGroup.gameObject.SetActive(true);
                _contentCanvasGroup.alpha = 1;
            });
        }

        void PlayHideContent()
        {
            _contentCanvasGroup.DoIfNotNull(() =>
            {
                Tween tween = _contentCanvasGroup.DOFade(0, _hideFadeDuration);

                TweenCallback onFinishCallback = () => Destroy(gameObject);
                tween.onComplete += onFinishCallback;
            });
        }
    }
}
