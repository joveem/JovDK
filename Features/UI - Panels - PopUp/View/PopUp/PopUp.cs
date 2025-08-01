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
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...

namespace JovDK.UI.PopUp
{
    public partial class PopUp : BasePanel
    {

        [Space(5), Header("[ State ]"), Space(10)]

        [SerializeField] bool _isShowingPositiveButton = true;
        [SerializeField] bool _isShowingNegativeButton = true;
        [SerializeField] bool _isShowingCloseButton = true;

        Action _positiveCallback;
        Action _negativeCallback;
        Action _closeCallback;
        Action _postPositiveCallback;
        Action _postNegativeCallback;
        Action _postCloseCallback;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] TextMeshProUGUI _titleText;
        [SerializeField] TextMeshProUGUI _descriptionText;
        [SerializeField] Button _positiveButton;
        [SerializeField] Button _negativeButton;
        [SerializeField] Button _closeButton;


        #region MonoBehaviour
        void Awake()
        {
            SetupComponent();
        }

        void OnEnable()
        {
            RefreshViewState();
        }

        void Start()
        {
            SetupButtons();
        }
        #endregion MonoBehaviour

        #region View
        void RefreshViewState()
        {
            // DebugExtension.DevLog();

            RefreshPositiveButtonViewState();
            RefreshNegativeButtonViewState();
            RefreshCloseButtonViewState();
        }
        #endregion View

        #region View - Buttons
        public void SwitchPositiveButtonViewState()
        {
            _isShowingPositiveButton = !_isShowingPositiveButton;

            RefreshPositiveButtonViewState();
        }

        public void SetPositiveButtonViewState(bool show)
        {
            _isShowingPositiveButton = show;

            if (show)
                ShowPositiveButton();
            else
                HidePositiveButton();
        }

        public void ShowPositiveButton()
        {
            _isShowingPositiveButton = true;
            _positiveButton.SetActiveIfNotNull(true);
        }

        public void HidePositiveButton()
        {
            _isShowingPositiveButton = false;
            _positiveButton.SetActiveIfNotNull(false);
        }

        void RefreshPositiveButtonViewState()
        {
            // DebugExtension.DevLog();

            SetPositiveButtonViewState(_isShowingPositiveButton);
        }

        public void SwitchNegativeButtonViewState()
        {
            _isShowingNegativeButton = !_isShowingNegativeButton;

            RefreshNegativeButtonViewState();
        }

        public void SetNegativeButtonViewState(bool show)
        {
            _isShowingNegativeButton = show;

            if (show)
                ShowNegativeButton();
            else
                HideNegativeButton();
        }

        public void ShowNegativeButton()
        {
            _isShowingNegativeButton = true;
            _negativeButton.SetActiveIfNotNull(true);
        }

        public void HideNegativeButton()
        {
            _isShowingNegativeButton = false;
            _negativeButton.SetActiveIfNotNull(false);
        }

        void RefreshNegativeButtonViewState()
        {
            // DebugExtension.DevLog();

            SetNegativeButtonViewState(_isShowingNegativeButton);
        }

        public void SwitchCloseButtonViewState()
        {
            _isShowingCloseButton = !_isShowingCloseButton;

            RefreshCloseButtonViewState();
        }

        public void SetCloseButtonViewState(bool show)
        {
            _isShowingCloseButton = show;

            if (show)
                ShowCloseButton();
            else
                HideCloseButton();
        }

        public void ShowCloseButton()
        {
            _isShowingCloseButton = true;
            _closeButton.SetActiveIfNotNull(true);
        }

        public void HideCloseButton()
        {
            _isShowingCloseButton = false;
            _closeButton.SetActiveIfNotNull(false);
        }

        void RefreshCloseButtonViewState()
        {
            // DebugExtension.DevLog();

            SetCloseButtonViewState(_isShowingCloseButton);
        }
        #endregion View
    }
}
