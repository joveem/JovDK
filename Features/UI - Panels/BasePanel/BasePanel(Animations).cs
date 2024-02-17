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
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class BasePanel : MonoBehaviour
{
    public void PlayShowAnimation(Action onFinishCallback = null)
    {
        if (!_isShowing)
        {
            _isShowing = true;

            _fadeBackground.DoIfNotNull(
                () =>
                {
                    _fadeBackground.gameObject.SetActive(true);
                    _fadeBackground
                        .DOFade(_maxFadeOpacity, _showAnimationDelay)
                        .SetEase(_backgroundPanelShowAnimationEase);
                });

            _bodyContainer.DoIfNotNull(
                () =>
                {
                    _bodyContainer.gameObject.SetActive(true);

                    Tween bodyTween = _bodyContainer
                        .DOScale(Vector3.one, _showAnimationDelay)
                        .SetEase(_backgroundPanelShowAnimationEase);

                    bodyTween.onComplete += () => onFinishCallback?.Invoke();
                });
        }
    }

    public void PlayHideAnimation(Action onFinishCallback = null)
    {
        if (_isShowing)
        {
            _isShowing = false;

            _fadeBackground.DoIfNotNull(
                () =>
                {
                    Tween fadeTween = _fadeBackground
                        .DOFade(0f, _showAnimationDelay)
                        .SetEase(_backgroundPanelHideAnimationEase);

                    fadeTween.onComplete += () => _fadeBackground.gameObject.SetActive(true);
                });

            _bodyContainer.DoIfNotNull(
                () =>
                {
                    Action finalOnFinishCallback =
                        () =>
                        {
                            _bodyContainer.gameObject.SetActive(false);
                            onFinishCallback?.Invoke();
                        };

                    Tween bodyTween = _bodyContainer
                        .DOScale(Vector3.zero, _showAnimationDelay)
                        .SetEase(_backgroundPanelHideAnimationEase);

                    bodyTween.onComplete += () => finalOnFinishCallback?.Invoke();
                });
        }
    }
}
