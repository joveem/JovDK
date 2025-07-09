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
            DebugExtension.DevLog();

            if (_isShowingPanel)
                ShowPanelInstantaneously();
            else
                HidePanelInstantaneously();
        }

        public virtual void ShowPanel()
        {
            DebugExtension.DevLog();

            if (!_isShowingPanel)
            {
                _isShowingPanel = true;

                PlayShowPanelAnimation();
            }
        }

        public virtual void HidePanel()
        {
            DebugExtension.DevLog();

            if (_isShowingPanel)
            {
                _isShowingPanel = false;

                PlayHidePanelAnimation();
            }
        }
        #endregion Controller

        #region View
        void PlayShowPanelAnimation()
        {
            DebugExtension.DevLog();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                _fullContentCanvasGroup.blocksRaycasts = true;
                _fullContentCanvasGroup.DOFade(1f, _coverAnimationDuration);
            });
        }

        void ShowPanelInstantaneously()
        {
            DebugExtension.DevLog();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                _fullContentCanvasGroup.blocksRaycasts = true;
                _fullContentCanvasGroup.alpha = 1f;
            });
        }

        void PlayHidePanelAnimation()
        {
            DebugExtension.DevLog();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                _fullContentCanvasGroup.blocksRaycasts = false;
                _fullContentCanvasGroup.DOFade(0f, _coverAnimationDuration);
            });
        }

        void HidePanelInstantaneously()
        {
            DebugExtension.DevLog();

            _fullContentCanvasGroup.DoIfNotNull(() =>
            {
                _fullContentCanvasGroup.blocksRaycasts = false;
                _fullContentCanvasGroup.alpha = 0f;
            });
        }
        #endregion View
    }
}
