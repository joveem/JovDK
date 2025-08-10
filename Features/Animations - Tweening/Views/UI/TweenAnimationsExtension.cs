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


namespace JovDK.Animations.Tweening
{
    public static class TweenAnimationsExtension
    {

        [Space(5), Header("[ Configs ]"), Space(10)]

        static float _animationsDuration = 0.35f;
        static Ease _openAnimationEase = Ease.OutBack;
        static Ease _closeAnimationEase = Ease.InBack;



        #region View
        public static void TryToApplyViewState(
            this Component baseComponent,
            bool hasToShow,
            bool applyInstantaneously,
            Ease? overrideShowEase = null,
            Ease? overrideHideEase = null)
        {
            baseComponent.DoIfNotNull(() =>
            {
                baseComponent.transform.TryToApplyViewState(
                    hasToShow,
                    applyInstantaneously,
                    overrideShowEase,
                    overrideHideEase);
            });
        }

        public static void TryToApplyViewState(
            this Transform baseTransform,
            bool hasToShow,
            bool applyInstantaneously,
            Ease? overrideShowEase = null,
            Ease? overrideHideEase = null)
        {
            baseTransform.DoIfNotNull(() =>
            {
                if (applyInstantaneously)
                    baseTransform.TryToApplyViewStateInstantaneously(hasToShow);
                else
                    baseTransform.TryToApplyViewState(hasToShow, overrideShowEase, overrideHideEase);
            });
        }

        public static void TryToApplyViewState(
            this Transform baseTransform,
            bool hasToShow,
            Ease? overrideShowEase = null,
            Ease? overrideHideEase = null)
        {
            if (hasToShow)
                baseTransform.TryToPlayDefaultShowAnimation(overrideShowEase);
            else
                baseTransform.TryToPlayDefaultHideAnimation(overrideHideEase);
        }

        public static void TryToApplyViewStateInstantaneously(
            this Transform baseTransform,
            bool hasToShow)
        {
            baseTransform.DoIfNotNull(() =>
            {
                if (hasToShow)
                    baseTransform.TryToShowObjectInstantaneously();
                else
                    baseTransform.TryToHideObjectInstantaneously();
            });
        }

        public static void TryToShowObjectInstantaneously(this Transform baseTransform)
        {
            baseTransform.DoIfNotNull(() =>
            {
                baseTransform.DOKill();
                baseTransform.gameObject.SetActive(true);
                baseTransform.localScale = Vector3.one;
            });
        }

        public static void TryToHideObjectInstantaneously(this Transform baseTransform)
        {
            baseTransform.DoIfNotNull(() =>
            {
                baseTransform.DOKill();
                baseTransform.gameObject.SetActive(false);
                baseTransform.localScale = Vector3.zero;
            });
        }

        public static void TryToPlayDefaultShowAnimation(this Transform baseTransform, Ease? overrideEase = null)
        {
            baseTransform.DoIfNotNull(() =>
            {
                // handle previous view state
                baseTransform.DOKill();
                baseTransform.gameObject.SetActive(true);
                // play animation
                baseTransform.DOScale(1, _animationsDuration).SetEase(overrideEase == null ? _openAnimationEase : (Ease)overrideEase);
            });
        }

        public static void TryToPlayDefaultHideAnimation(this Transform baseTransform, Ease? overrideEase = null)
        {
            baseTransform.DoIfNotNull(() =>
            {
                if (baseTransform.gameObject.activeSelf)
                {
                    // handle previous view state
                    baseTransform.DOKill();
                    // play animation
                    Tween tween = baseTransform.DOScale(0, _animationsDuration).SetEase(overrideEase == null ? _closeAnimationEase : (Ease)overrideEase);
                    // disable object after animation end
                    tween.onComplete += () => baseTransform.gameObject.SetActive(false);
                }
            });
        }
        #endregion View
    }
}
