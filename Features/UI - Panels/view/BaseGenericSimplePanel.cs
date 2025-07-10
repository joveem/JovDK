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
        Tween _curretBackgroundTween = null;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] protected CanvasGroup _fullContentCanvasGroup;


        [Space(5), Header("[ Configs ]"), Space(10)]

        protected float _coverAnimationDuration = 0.35f;



        #region MonoBehaviour
        public override void DisabledAwake()
        {
            SetInitialState();
        }
        #endregion MonoBehaviour

        #region Controller
        protected virtual void SetInitialState()
        {
            // DebugExtension.DevLog();

            if (_isShowingPanel)
                ShowPanelInstantaneously();
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

        public virtual void ShowPanel()
        {
            // DebugExtension.DevLog();

            if (!_isShowingPanel)
            {
                _isShowingPanel = true;
                TryToKillBackgroundTween();

                PlayShowPanelAnimation();
            }
        }

        public void ShowPanelInstantaneously()
        {
            // DebugExtension.DevLog();

            _isShowingPanel = true;
            TryToKillBackgroundTween();
            gameObject.SetActive(true);

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                _fullContentCanvasGroup.blocksRaycasts = true;
                _fullContentCanvasGroup.alpha = 1f;
            });
        }

        public virtual void HidePanel()
        {
            // DebugExtension.DevLog();

            if (_isShowingPanel)
            {
                _isShowingPanel = false;
                TryToKillBackgroundTween();

                PlayHidePanelAnimation();
            }
        }

        public void HidePanelInstantaneously()
        {
            // DebugExtension.DevLog();

            _isShowingPanel = false;
            TryToKillBackgroundTween();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                _fullContentCanvasGroup.blocksRaycasts = false;
                _fullContentCanvasGroup.alpha = 0f;
            });

            gameObject.SetActive(false);
        }
        #endregion Controller

        #region View
        protected void PlayShowPanelAnimation()
        {
            // DebugExtension.DevLog();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                TryToKillBackgroundTween();

                gameObject.SetActive(true);

                _fullContentCanvasGroup.blocksRaycasts = true;
                _curretBackgroundTween = _fullContentCanvasGroup.DOFade(1f, _coverAnimationDuration);
            });
        }

        protected void PlayHidePanelAnimation()
        {
            // DebugExtension.DevLog();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                TryToKillBackgroundTween();

                _fullContentCanvasGroup.blocksRaycasts = false;
                _curretBackgroundTween = _fullContentCanvasGroup.DOFade(0f, _coverAnimationDuration);

                _curretBackgroundTween.onComplete = () => gameObject.SetActive(false);
            });
        }

        protected void TryToKillBackgroundTween()
        {
            if (_curretBackgroundTween.IsActive())
                _curretBackgroundTween.Kill();
        }
        #endregion View
    }
}
