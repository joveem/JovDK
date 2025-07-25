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
    public partial class BaseGenericSimplePanel : DisabledAwake_Monobehavior
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        protected bool _isShowingPanel = false;
        protected bool _hasShowAndHideTransitionEnd = true;
        Tween _curretBackgroundTween = null;
        Tween _movingContentTween = null;
        // show/hide start callbacks
        List<Action> _onShowStartCallbackList = new List<Action>();
        List<Action> _onShowStartOnceCallbackList = new List<Action>();
        List<Action> _onHideStartCallbackList = new List<Action>();
        List<Action> _onHideStartOnceCallbackList = new List<Action>();
        // show/hide finish callbacks
        List<Action> _onShowFinishCallbackList = new List<Action>();
        List<Action> _onShowFinishOnceCallbackList = new List<Action>();
        List<Action> _onHideFinishCallbackList = new List<Action>();
        List<Action> _onHideFinishOnceCallbackList = new List<Action>();
        bool _hasSettedInitialMovingContainerPosition = false;
        Vector2 _initialMovingAnimationContainerPosition = default;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] protected CanvasGroup _fullContentCanvasGroup;
        [SerializeField] protected RectTransform _fullContentMovingAnimationContainer;
        [SerializeField] protected RectTransform _overrideContentMovingAnimationSizeContainer;


        [Space(5), Header("[ Configs ]"), Space(10)]

        // protected float _coverAnimationDuration = 0.35f;
        protected float _coverAnimationDuration = 0.6f;

        public static TransitionOptions HorizontalNegativeTransition =
            new TransitionOptions()
            {
                OverrideShowAnimation = AnimationType.SlideToOrFromRight,
                OverrideHideAnimation = AnimationType.SlideToOrFromLeft,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
            };

        public static TransitionOptions HorizontalPositiveTransition =
            new TransitionOptions()
            {
                OverrideShowAnimation = AnimationType.SlideToOrFromLeft,
                OverrideHideAnimation = AnimationType.SlideToOrFromRight,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
            };

        public static TransitionOptions VerticalPositiveTransition =
            new TransitionOptions()
            {
                OverrideShowAnimation = AnimationType.SlideToOrFromDown,
                OverrideHideAnimation = AnimationType.SlideToOrFromUp,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
            };

        public static TransitionOptions VerticalNegativeTransition =
            new TransitionOptions()
            {
                OverrideShowAnimation = AnimationType.SlideToOrFromUp,
                OverrideHideAnimation = AnimationType.SlideToOrFromDown,
                OverrideSlideFactorType = SlideFactorType.MovingContainerSize,
            };



        #region MonoBehaviour
        public override void DisabledAwake()
        {
            SetInitialState();
        }

        void Start()
        {
            HandleMovingContainerPositionSetting();
        }
        #endregion MonoBehaviour

        #region Callbacks - "Show/Hide
        void OnShowStart()
        {
            // DebugExtension.DevLog();

            foreach (var callback in _onShowStartCallbackList)
                callback?.Invoke();

            foreach (var callback in _onShowStartOnceCallbackList)
                callback?.Invoke();

            _onShowStartOnceCallbackList = new List<Action>();
        }

        void OnHideStart()
        {
            // DebugExtension.DevLog();

            foreach (var callback in _onHideStartCallbackList)
                callback?.Invoke();

            foreach (var callback in _onHideStartOnceCallbackList)
                callback?.Invoke();

            _onHideStartOnceCallbackList = new List<Action>();
        }

        void OnShowFinish()
        {
            // DebugExtension.DevLog();

            foreach (var callback in _onShowFinishCallbackList)
                callback?.Invoke();

            foreach (var callback in _onShowFinishOnceCallbackList)
                callback?.Invoke();

            _onShowFinishOnceCallbackList = new List<Action>();
        }

        void OnHideFinish()
        {
            // DebugExtension.DevLog();

            foreach (var callback in _onHideFinishCallbackList)
                callback?.Invoke();

            foreach (var callback in _onHideFinishOnceCallbackList)
                callback?.Invoke();

            _onHideFinishOnceCallbackList = new List<Action>();
        }
        #endregion Callbacks - Show/Hide

        #region Controller - Show/Hide Callbacks
        /// <summary>
        /// Registers a one-time callback for when the panel is shown.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="invokeImmediatelyIfInTargetState">
        /// If true, invokes the callback immediately if the panel is already shown.
        /// </param>
        /// <param name="waitAnimationEnd">
        /// If true, callback is triggered after the show animation finishes; otherwise, at start.
        /// </param>
        public void AddOnShowOnceCallback(
            Action callback,
            bool invokeImmediatelyIfInTargetState = false,
            bool waitAnimationEnd = true)
        {
            AddOnShowCallback(callback, true, invokeImmediatelyIfInTargetState, waitAnimationEnd);
        }

        /// <summary>
        /// Registers a persistent or one-time callback for when the panel is shown.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="executeOnce">Whether to execute the callback only once.</param>
        /// <param name="invokeImmediatelyIfInTargetState">
        /// If true, invokes the callback immediately if the panel is already shown.
        /// </param>
        /// <param name="waitAnimationEnd">
        /// If true, callback is triggered after the show animation finishes; otherwise, at start.
        /// </param>
        public void AddOnShowCallback(
            Action callback,
            bool executeOnce = false,
            bool invokeImmediatelyIfInTargetState = false,
            bool waitAnimationEnd = true)
        {
            bool isConditionAlreadyMet = _isShowingPanel && _hasShowAndHideTransitionEnd;
            bool hasToTriggerCallbackImmediately = invokeImmediatelyIfInTargetState && isConditionAlreadyMet;
            bool hasToSaveCallback = !executeOnce || !hasToTriggerCallbackImmediately;

            if (hasToTriggerCallbackImmediately)
                callback?.Invoke();

            if (hasToSaveCallback)
            {
                callback.DoIfNotNull(() =>
                {
                    if (executeOnce)
                    {
                        if (waitAnimationEnd)
                            _onShowFinishOnceCallbackList.Add(callback);
                        else
                            _onShowStartOnceCallbackList.Add(callback);
                    }
                    else
                    {
                        if (waitAnimationEnd)
                            _onShowFinishCallbackList.Add(callback);
                        else
                            _onShowStartCallbackList.Add(callback);
                    }
                });
            }
        }

        /// <summary>
        /// Registers a one-time callback for when the panel is hidden.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="invokeImmediatelyIfInTargetState">
        /// If true, invokes the callback immediately if the panel is already hidden.
        /// </param>
        /// <param name="waitAnimationEnd">
        /// If true, callback is triggered after the hide animation finishes; otherwise, at start.
        /// </param>
        public void AddOnHideOnceCallback(
            Action callback,
            bool invokeImmediatelyIfInTargetState = false,
            bool waitAnimationEnd = true)
        {
            AddOnHideCallback(callback, true, invokeImmediatelyIfInTargetState, waitAnimationEnd);
        }

        /// <summary>
        /// Registers a persistent or one-time callback for when the panel is hidden.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="executeOnce">Whether to execute the callback only once.</param>
        /// <param name="invokeImmediatelyIfInTargetState">
        /// If true, invokes the callback immediately if the panel is already hidden.
        /// </param>
        /// <param name="waitAnimationEnd">
        /// If true, callback is triggered after the hide animation finishes; otherwise, at start.
        /// </param>
        public void AddOnHideCallback(
            Action callback,
            bool executeOnce = false,
            bool invokeImmediatelyIfInTargetState = false,
            bool waitAnimationEnd = true)
        {
            bool isConditionAreadyMet = !_isShowingPanel && _hasShowAndHideTransitionEnd;
            bool hasToTriggerCallbackImmediately = invokeImmediatelyIfInTargetState && isConditionAreadyMet;
            bool hasToSaveCallback = !executeOnce || !hasToTriggerCallbackImmediately;

            if (hasToTriggerCallbackImmediately)
                callback?.Invoke();

            if (hasToSaveCallback)
            {
                callback.DoIfNotNull(() =>
                {
                    if (executeOnce)
                    {
                        if (waitAnimationEnd)
                            _onHideFinishOnceCallbackList.Add(callback);
                        else
                            _onHideStartOnceCallbackList.Add(callback);
                    }
                    else
                    {
                        if (waitAnimationEnd)
                            _onHideFinishCallbackList.Add(callback);
                        else
                            _onHideStartCallbackList.Add(callback);
                    }
                });
            }
        }
        #endregion Controller - Show/Hide Callbacks"

        #region Controller
        protected virtual void SetInitialState()
        {
            // DebugExtension.DevLog();

            // HandleMovingContainerPositionSetting();

            if (_isShowingPanel)
            {
                ShowPanelInstantaneously();
                HandleMovingContainerPositionSetting();
            }
            else
                HidePanelInstantaneously();
        }

        public virtual void SwitchPanelViewState()
        {
            // DebugExtension.DevLog();

            if (_isShowingPanel)
                HidePanel();
            else
                ShowPanel();
        }

        public virtual void SetPanelViewState(bool newShowValue)
        {
            // DebugExtension.DevLog("newShowValue = ", newShowValue.ToString());

            if (newShowValue)
                ShowPanel();
            else
                HidePanel();
        }

        public virtual void SetPanelViewStateInstantaneously(bool newShowValue)
        {
            // DebugExtension.DevLog("newShowValue = ", newShowValue.ToString());

            if (newShowValue)
                ShowPanelInstantaneously();
            else
                HidePanelInstantaneously();
        }

        public virtual void ShowPanel(TransitionOptions transitionOptions = null)
        {
            // DebugExtension.DevLog();

            if (!_isShowingPanel)
            {
                // bool previousIsShowingPanelState = _isShowingPanel;
                bool previousHasShowAndHideTransitionEndState = _hasShowAndHideTransitionEnd;
                _isShowingPanel = true;
                _hasShowAndHideTransitionEnd = true;

                if (!previousHasShowAndHideTransitionEndState)
                    OnHideFinish();

                TryToKillAllPendingTweens();

                Action onAnimationStart = () => OnShowStart();
                Action onAnimationEnd = () =>
                {
                    _hasShowAndHideTransitionEnd = true;
                    OnShowFinish();
                };

                PlayShowPanelAnimation(onAnimationStart, onAnimationEnd, transitionOptions);
            }
        }

        public virtual void ShowPanelInstantaneously()
        {
            // DebugExtension.DevLog();

            bool previousIsShowingPanelState = _isShowingPanel;
            bool previousHasShowAndHideTransitionEndState = _hasShowAndHideTransitionEnd;
            _isShowingPanel = true;
            _hasShowAndHideTransitionEnd = true;

            if (!previousHasShowAndHideTransitionEndState)
                OnHideFinish();

            if (previousIsShowingPanelState != _isShowingPanel)
                OnShowStart();

            TryToKillAllPendingTweens();
            gameObject.SetActive(true);

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                _fullContentCanvasGroup.blocksRaycasts = true;
                _fullContentCanvasGroup.alpha = 1f;
            },
            () =>
            {
                Debug.LogWarning("_fullContentCanvasGroup is null! gameObject = ", gameObject);
            });

            if (previousIsShowingPanelState != _isShowingPanel)
                OnShowFinish();
        }

        public virtual void HidePanel(TransitionOptions transitionOptions = null)
        {
            // DebugExtension.DevLog();

            if (_isShowingPanel)
            {
                // bool previousIsShowingPanelState = _isShowingPanel;
                bool previousHasShowAndHideTransitionEndState = _hasShowAndHideTransitionEnd;
                _isShowingPanel = false;
                _hasShowAndHideTransitionEnd = true;

                if (!previousHasShowAndHideTransitionEndState)
                    OnShowFinish();

                TryToKillAllPendingTweens();

                Action onAnimationStart = () => OnHideStart();
                Action onAnimationEnd = () =>
                {
                    _hasShowAndHideTransitionEnd = true;
                    OnHideFinish();
                };

                PlayHidePanelAnimation(onAnimationStart, onAnimationEnd, transitionOptions);
            }
        }

        public virtual void HidePanelInstantaneously()
        {
            // DebugExtension.DevLog();

            bool previousIsShowingPanelState = _isShowingPanel;
            bool previousHasShowAndHideTransitionEndState = _hasShowAndHideTransitionEnd;
            _isShowingPanel = false;
            _hasShowAndHideTransitionEnd = true;

            if (!previousHasShowAndHideTransitionEndState)
                OnShowFinish();

            if (previousIsShowingPanelState != _isShowingPanel)
                OnHideStart();

            TryToKillAllPendingTweens();

            _fullContentCanvasGroup.DoIfNotNull(
            () =>
            {
                _fullContentCanvasGroup.blocksRaycasts = false;
                _fullContentCanvasGroup.alpha = 0f;
            },
            () =>
            {
                Debug.LogWarning("_fullContentCanvasGroup is null! gameObject = ", gameObject);
            });

            gameObject.SetActive(false);

            if (previousIsShowingPanelState != _isShowingPanel)
                OnHideFinish();
        }
        #endregion Controller

        #region View
        protected virtual void PlayShowPanelAnimation(
            Action onAnimationStartCallback = null,
            Action onAnimationFinishCallback = null,
            TransitionOptions transitionOptions = null)
        {
            // DebugExtension.DevLog();

            if (transitionOptions is null)
                transitionOptions = new TransitionOptions();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                TryToKillAllPendingTweens();

                gameObject.SetActive(true);
                _fullContentCanvasGroup.blocksRaycasts = true;

                if (onAnimationStartCallback is not null)
                    onAnimationStartCallback();

                TweenCallback onComplete = () => onAnimationFinishCallback?.Invoke();

                _curretBackgroundTween =
                    _fullContentCanvasGroup
                        .DOFade(1f, _coverAnimationDuration)
                        .SetEase(transitionOptions.ShowAnimationEase);

                _curretBackgroundTween.onComplete = onComplete;

                Vector2 movingAnimationDelta = Vector2.zero;

                switch (transitionOptions.OverrideShowAnimation)
                {
                    case AnimationType.Default:
                        break;

                    case AnimationType.SlideToOrFromLeft:
                    case AnimationType.SlideToOrFromRight:
                    case AnimationType.SlideToOrFromDown:
                    case AnimationType.SlideToOrFromUp:
                        {
                            switch (transitionOptions.OverrideSlideFactorType)
                            {
                                case SlideFactorType.Default:
                                case SlideFactorType.ScreenSize:
                                    {
                                        switch (transitionOptions.OverrideShowAnimation)
                                        {
                                            case AnimationType.SlideToOrFromLeft:
                                                movingAnimationDelta = new Vector2(Screen.width * -1f, 0);
                                                break;

                                            case AnimationType.SlideToOrFromRight:
                                                movingAnimationDelta = new Vector2(Screen.width, 0);
                                                break;

                                            case AnimationType.SlideToOrFromDown:
                                                movingAnimationDelta = new Vector2(0f, Screen.height * -1f);
                                                break;

                                            case AnimationType.SlideToOrFromUp:
                                                movingAnimationDelta = new Vector2(0f, Screen.height);
                                                break;
                                        }

                                        break;
                                    }

                                case SlideFactorType.MovingContainerSize:
                                    {
                                        RectTransform baseRectTransform = _fullContentMovingAnimationContainer;
                                        _overrideContentMovingAnimationSizeContainer.DoIfNotNull(() => baseRectTransform = _overrideContentMovingAnimationSizeContainer, false);

                                        baseRectTransform.DoIfNotNull(() =>
                                        {
                                            switch (transitionOptions.OverrideShowAnimation)
                                            {
                                                case AnimationType.SlideToOrFromLeft:
                                                    movingAnimationDelta = new Vector2(baseRectTransform.rect.width * -1f, 0);
                                                    break;

                                                case AnimationType.SlideToOrFromRight:
                                                    movingAnimationDelta = new Vector2(baseRectTransform.rect.width, 0);
                                                    break;

                                                case AnimationType.SlideToOrFromDown:
                                                    movingAnimationDelta = new Vector2(0, baseRectTransform.rect.height * -1f);
                                                    break;

                                                case AnimationType.SlideToOrFromUp:
                                                    movingAnimationDelta = new Vector2(0, baseRectTransform.rect.height);
                                                    break;
                                            }
                                        },
                                        () =>
                                        {
                                            Debug.LogWarning(
                                                "$> ".ToColor(GoodColors.Red) +
                                                "baseRectTransform is null!".ToColor(GoodColors.Pink) + " | " +
                                                "gameObject.name = " + gameObject.name.SerializeObjectToJSON() + "\n" +
                                                "",
                                                gameObject);
                                        });

                                        break;
                                    }

                                default:
                                    {
                                        DebugExtension.DevLogWarning(
                                            "$> ".ToColor(GoodColors.Red),
                                            "Unexpected transitionOptions.OverrideSlideFactorType!".ToColor(GoodColors.Pink), " | ",
                                            "transitionOptions.OverrideSlideFactorType = ",
                                            transitionOptions.OverrideSlideFactorType.ToString(), "\n",
                                            "");

                                        break;
                                    }
                            }

                            break;
                        }

                    default:
                        {
                            DebugExtension.DevLogWarning(
                                "$> ".ToColor(GoodColors.Red),
                                "Unexpected transitionOptions.OverrideShowAnimation!".ToColor(GoodColors.Pink), " | ",
                                "transitionOptions.OverrideShowAnimation = ",
                                transitionOptions.OverrideShowAnimation.ToString(), "\n",
                                "");

                            break;
                        }
                }

                // TODO: REVIEW THIS! (ahjsidyhasd)
                // switch (transitionOptions.OverrideShowAnimation)
                // {
                //     case AnimationType.SlideToOrFromLeft:
                //     case AnimationType.SlideToOrFromRight:
                //     case AnimationType.SlideToOrFromDown:
                //     case AnimationType.SlideToOrFromUp:
                //         {
                //             _fullContentMovingAnimationContainer.DoIfNotNull(() =>
                //             {
                //                 Vector2 startPosition = _initialMovingAnimationContainerPosition + movingAnimationDelta;
                //                 Vector2 finalPosition = _initialMovingAnimationContainerPosition;

                //                 _fullContentMovingAnimationContainer.position = startPosition;
                //                 _movingContentTween = _fullContentMovingAnimationContainer.DOMove(finalPosition, _coverAnimationDuration);
                //             }, false);

                //             break;
                //         }
                // }

                if (!_hasSettedInitialMovingContainerPosition)
                    HandleMovingContainerPositionSetting();

                if (_hasSettedInitialMovingContainerPosition)
                {
                    _fullContentMovingAnimationContainer.DoIfNotNull(() =>
                    {
                        Vector2 startPosition = _initialMovingAnimationContainerPosition + movingAnimationDelta;
                        Vector2 finalPosition = _initialMovingAnimationContainerPosition;

                        _fullContentMovingAnimationContainer.position = startPosition;
                        _movingContentTween =
                            _fullContentMovingAnimationContainer
                                .DOMove(finalPosition, _coverAnimationDuration)
                                .SetEase(transitionOptions.ShowAnimationEase);
                    }, false);
                }
            });
        }

        protected virtual void PlayHidePanelAnimation(
            Action onAnimationStartCallback = null,
            Action onAnimationFinishCallback = null,
            TransitionOptions transitionOptions = null)
        {
            // DebugExtension.DevLog();

            if (transitionOptions is null)
                transitionOptions = new TransitionOptions();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                TryToKillAllPendingTweens();

                _fullContentCanvasGroup.blocksRaycasts = false;

                if (onAnimationStartCallback is not null)
                    onAnimationStartCallback();

                TweenCallback onComplete = () =>
                {
                    gameObject.SetActive(false);
                    onAnimationFinishCallback?.Invoke();
                };

                _curretBackgroundTween =
                    _fullContentCanvasGroup
                    .DOFade(0f, _coverAnimationDuration)
                    .SetEase(transitionOptions.HideAnimationEase);

                _curretBackgroundTween.onComplete = onComplete;

                Vector2 movingAnimationDelta = Vector2.zero;

                switch (transitionOptions.OverrideHideAnimation)
                {
                    case AnimationType.Default:
                        break;

                    case AnimationType.SlideToOrFromLeft:
                    case AnimationType.SlideToOrFromRight:
                    case AnimationType.SlideToOrFromDown:
                    case AnimationType.SlideToOrFromUp:
                        {
                            switch (transitionOptions.OverrideSlideFactorType)
                            {
                                case SlideFactorType.Default:
                                case SlideFactorType.ScreenSize:
                                    {
                                        switch (transitionOptions.OverrideHideAnimation)
                                        {
                                            case AnimationType.SlideToOrFromLeft:
                                                movingAnimationDelta = new Vector2(Screen.width * -1f, 0);
                                                break;

                                            case AnimationType.SlideToOrFromRight:
                                                movingAnimationDelta = new Vector2(Screen.width, 0);
                                                break;

                                            case AnimationType.SlideToOrFromDown:
                                                movingAnimationDelta = new Vector2(0f, Screen.height * -1f);
                                                break;

                                            case AnimationType.SlideToOrFromUp:
                                                movingAnimationDelta = new Vector2(0f, Screen.height);
                                                break;
                                        }

                                        break;
                                    }

                                case SlideFactorType.MovingContainerSize:
                                    {
                                        RectTransform baseRectTransform = _fullContentMovingAnimationContainer;
                                        _overrideContentMovingAnimationSizeContainer.DoIfNotNull(() => baseRectTransform = _overrideContentMovingAnimationSizeContainer, false);

                                        baseRectTransform.DoIfNotNull(() =>
                                        {
                                            switch (transitionOptions.OverrideHideAnimation)
                                            {
                                                case AnimationType.SlideToOrFromLeft:
                                                    movingAnimationDelta = new Vector2(baseRectTransform.rect.width * -1f, 0);
                                                    break;

                                                case AnimationType.SlideToOrFromRight:
                                                    movingAnimationDelta = new Vector2(baseRectTransform.rect.width, 0);
                                                    break;

                                                case AnimationType.SlideToOrFromDown:
                                                    movingAnimationDelta = new Vector2(0, baseRectTransform.rect.height * -1f);
                                                    break;

                                                case AnimationType.SlideToOrFromUp:
                                                    movingAnimationDelta = new Vector2(0, baseRectTransform.rect.height);
                                                    break;
                                            }
                                        },
                                        () =>
                                        {
                                            Debug.LogWarning(
                                                "$> ".ToColor(GoodColors.Red) +
                                                "baseRectTransform is null!".ToColor(GoodColors.Pink) + " | " +
                                                "gameObject.name = " + gameObject.name.SerializeObjectToJSON() + "\n" +
                                                "",
                                                gameObject);
                                        });

                                        break;
                                    }

                                default:
                                    {
                                        DebugExtension.DevLogWarning(
                                            "$> ".ToColor(GoodColors.Red),
                                            "Unexpected transitionOptions.OverrideSlideFactorType!".ToColor(GoodColors.Pink), " | ",
                                            "transitionOptions.OverrideSlideFactorType = ",
                                            transitionOptions.OverrideSlideFactorType.ToString(), "\n",
                                            "");

                                        break;
                                    }
                            }

                            break;
                        }

                    default:
                        {
                            DebugExtension.DevLogWarning(
                                "$> ".ToColor(GoodColors.Red),
                                "Unexpected transitionOptions.OverrideHideAnimation!".ToColor(GoodColors.Pink), " | ",
                                "transitionOptions.OverrideHideAnimation = ",
                                transitionOptions.OverrideHideAnimation.ToString(), "\n",
                                "");

                            break;
                        }
                }

                // TODO: REVIEW THIS! (ahjsidyhasd)
                // switch (transitionOptions.OverrideHideAnimation)
                // {
                //     case AnimationType.SlideToOrFromLeft:
                //     case AnimationType.SlideToOrFromRight:
                //     case AnimationType.SlideToOrFromDown:
                //     case AnimationType.SlideToOrFromUp:
                //         {
                //             _fullContentMovingAnimationContainer.DoIfNotNull(() =>
                //             {
                //                 Vector2 finalPosition = _initialMovingAnimationContainerPosition + movingAnimationDelta;
                //                 _movingContentTween = _fullContentMovingAnimationContainer.DOMove(finalPosition, _coverAnimationDuration);
                //             }, false);

                //             break;
                //         }
                // }

                if (!_hasSettedInitialMovingContainerPosition)
                    HandleMovingContainerPositionSetting();

                if (_hasSettedInitialMovingContainerPosition)
                {
                    _fullContentMovingAnimationContainer.DoIfNotNull(() =>
                    {
                        Vector2 finalPosition = _initialMovingAnimationContainerPosition + movingAnimationDelta;
                        _movingContentTween =
                            _fullContentMovingAnimationContainer
                                .DOMove(finalPosition, _coverAnimationDuration)
                                .SetEase(transitionOptions.HideAnimationEase);
                    }, false);
                }
            });
        }

        protected virtual void TryToKillAllPendingTweens()
        {
            if (_curretBackgroundTween.IsActive())
                _curretBackgroundTween.Kill();

            if (_movingContentTween.IsActive())
                _movingContentTween.Kill();
        }


        // void HandleInitialVideoYPositionFactorSetting()
        void HandleMovingContainerPositionSetting()
        {
            // DebugExtension.DevLog();

            if (!_hasSettedInitialMovingContainerPosition)
            {
                // Debug.Log(">>>".ToColor(GoodColors.Blue) + " gameObject.name = " + gameObject.name, gameObject);

                _hasSettedInitialMovingContainerPosition = true;

                _fullContentMovingAnimationContainer.DoIfNotNull(() =>
                {
                    _initialMovingAnimationContainerPosition = _fullContentMovingAnimationContainer.position;
                }, false);
            }
        }
        #endregion View
    }

    public class TransitionOptions
    {
        public Ease ShowAnimationEase = Ease.InOutBack;
        public Ease HideAnimationEase = Ease.InOutBack;
        public AnimationType OverrideShowAnimation = AnimationType.Default;
        public AnimationType OverrideHideAnimation = AnimationType.Default;
        public SlideFactorType OverrideSlideFactorType = SlideFactorType.Default;
    }

    public enum AnimationType
    {
        UNDEFINED = -1,
        Default,
        /// <summary>
        /// Slide with a [-1, 0] vector scale
        /// </summary>
        SlideToOrFromLeft,
        /// <summary>
        /// Slide with a [1, 0] vector scale
        /// </summary>
        SlideToOrFromRight,
        /// <summary>
        /// Slide with a [0, -1] vector scale
        /// </summary>
        SlideToOrFromDown,
        /// <summary>
        /// Slide with a [0, 1] vector scale
        /// </summary>
        SlideToOrFromUp,
    }

    public enum SlideFactorType
    {
        UNDEFINED = -1,
        Default,
        ScreenSize,
        MovingContainerSize,
    }
}
