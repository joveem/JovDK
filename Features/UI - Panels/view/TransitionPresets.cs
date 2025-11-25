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
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.Generic.UnityEngineExtensions;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.UI.Generic
{
    public static class TransitionPresets
    {
        // 1 part
        static TransitionOptions _horizontalNegativeTransition_For1Element =
            new TransitionOptions()
            {
                AnimationDuration = 0.35f,
                OverrideShowAnimation = AnimationType.SlideToOrFromRight,
                OverrideHideAnimation = AnimationType.SlideToOrFromLeft,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.OutBack,
                HideSlideAnimationEase = Ease.InBack,
                // background fade
                ShowAnimationEase = Ease.OutBack,
                HideAnimationEase = Ease.InBack,
            };
        public static TransitionOptions HorizontalNegativeTransition_For1Element { get { return _horizontalNegativeTransition_For1Element; } }

        static TransitionOptions _horizontalPositiveTransition_For1Element =
            new TransitionOptions()
            {
                AnimationDuration = 0.35f,
                OverrideShowAnimation = AnimationType.SlideToOrFromLeft,
                OverrideHideAnimation = AnimationType.SlideToOrFromRight,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.OutBack,
                HideSlideAnimationEase = Ease.InBack,
                // background fade
                ShowAnimationEase = Ease.OutBack,
                HideAnimationEase = Ease.InBack,
            };
        public static TransitionOptions HorizontalPositiveTransition_For1Element { get { return _horizontalPositiveTransition_For1Element; } }

        static TransitionOptions _verticalPositiveTransition_For1Element =
            new TransitionOptions()
            {
                AnimationDuration = 0.35f,
                OverrideShowAnimation = AnimationType.SlideToOrFromDown,
                OverrideHideAnimation = AnimationType.SlideToOrFromUp,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.OutBack,
                HideSlideAnimationEase = Ease.InBack,
                // background fade
                ShowAnimationEase = Ease.OutBack,
                HideAnimationEase = Ease.InBack,
            };
        public static TransitionOptions VerticalPositiveTransition_For1Element { get { return _verticalPositiveTransition_For1Element; } }

        static TransitionOptions _verticalNegativeTransition_For1Element =
            new TransitionOptions()
            {
                AnimationDuration = 0.35f,
                OverrideShowAnimation = AnimationType.SlideToOrFromUp,
                OverrideHideAnimation = AnimationType.SlideToOrFromDown,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.OutBack,
                HideSlideAnimationEase = Ease.InBack,
                // background fade
                ShowAnimationEase = Ease.OutBack,
                HideAnimationEase = Ease.InBack,
            };
        public static TransitionOptions VerticalNegativeTransition_For1Element { get { return _verticalNegativeTransition_For1Element; } }

        // 2 parts
        static TransitionOptions _HorizontalNegativeTransition_For2Elements =
            new TransitionOptions()
            {
                AnimationDuration = 0.6f,
                OverrideShowAnimation = AnimationType.SlideToOrFromRight,
                OverrideHideAnimation = AnimationType.SlideToOrFromLeft,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.InOutBack,
                HideSlideAnimationEase = Ease.InOutBack,
                // background fade
                ShowAnimationEase = Ease.InOutBack,
                HideAnimationEase = Ease.InOutBack,
            };
        public static TransitionOptions HorizontalNegativeTransition_For2Elements { get { return _HorizontalNegativeTransition_For2Elements; } }

        static TransitionOptions _horizontalPositiveTransition_For2Elements =
            new TransitionOptions()
            {
                AnimationDuration = 0.6f,
                OverrideShowAnimation = AnimationType.SlideToOrFromLeft,
                OverrideHideAnimation = AnimationType.SlideToOrFromRight,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.InOutBack,
                HideSlideAnimationEase = Ease.InOutBack,
                // background fade
                ShowAnimationEase = Ease.InOutBack,
                HideAnimationEase = Ease.InOutBack,
            };
        public static TransitionOptions HorizontalPositiveTransition_For2Elements { get { return _horizontalPositiveTransition_For2Elements; } }

        static TransitionOptions _verticalPositiveTransition_For2Elements =
            new TransitionOptions()
            {
                AnimationDuration = 0.6f,
                OverrideShowAnimation = AnimationType.SlideToOrFromDown,
                OverrideHideAnimation = AnimationType.SlideToOrFromUp,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.InOutBack,
                HideSlideAnimationEase = Ease.InOutBack,
                // background fade
                ShowAnimationEase = Ease.InOutBack,
                HideAnimationEase = Ease.InOutBack,
            };
        public static TransitionOptions VerticalPositiveTransition_For2Elements { get { return _verticalPositiveTransition_For2Elements; } }

        static TransitionOptions _verticalNegativeTransition_For2Elements =
            new TransitionOptions()
            {
                AnimationDuration = 0.6f,
                OverrideShowAnimation = AnimationType.SlideToOrFromUp,
                OverrideHideAnimation = AnimationType.SlideToOrFromDown,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
                // slide
                ShowSlideAnimationEase = Ease.InOutBack,
                HideSlideAnimationEase = Ease.InOutBack,
                // background fade
                ShowAnimationEase = Ease.InOutBack,
                HideAnimationEase = Ease.InOutBack,
            };
        public static TransitionOptions VerticalNegativeTransition_For2Elements { get { return _verticalNegativeTransition_For2Elements; } }
    }
}
