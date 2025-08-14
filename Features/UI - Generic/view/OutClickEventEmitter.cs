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


namespace JovDK.UI.Generic
{
    public partial class OutClickEventEmitter : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        public Action OnInsideClickCallback = null;
        public Action OnOutsideClickCallback = null;
        List<RectTransform> _finalInnerAreas = new List<RectTransform>();


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] List<RectTransform> _customInnerAreas = new List<RectTransform>();


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;



        #region MonoBehaviour
        void Awake()
        {
            SetupComponent();
        }

        void Update()
        {
            HandleInputs();
        }
        #endregion MonoBehaviour

        #region Callbacks
        void OnInsideClick()
        {
            if (OnInsideClickCallback != null)
                OnInsideClickCallback();
        }

        void OnOutsideClick()
        {
            if (OnOutsideClickCallback != null)
                OnOutsideClickCallback();
        }
        #endregion Callbacks

        #region Controller
        void HandleInputs()
        {
            bool hasClicked = false;
            Vector2 clickPosition = new Vector2();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hasClicked = true;
                clickPosition = Input.mousePosition;
            }

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    hasClicked = true;
                    clickPosition = touch.position;
                }
            }

            if (hasClicked && _finalInnerAreas.Count > 0)
                HandleClick(clickPosition);
        }

        void HandleClick(Vector2 mousePosition)
        {
            bool isAnInsideClick = false;

            foreach (RectTransform innerArea in _finalInnerAreas)
            {
                if (innerArea != null && innerArea.gameObject.activeInHierarchy)
                {
                    bool isClickInsideCurrentArea = RectTransformUtility.RectangleContainsScreenPoint(innerArea, mousePosition);

                    if (isClickInsideCurrentArea)
                    {
                        isAnInsideClick = true;
                        break;
                    }
                }
            }

            if (isAnInsideClick)
                OnInsideClick();
            else
                OnOutsideClick();
        }
        #endregion Controller

        #region Controller - Setup
        void SetupComponent()
        {
            if (_customInnerAreas != null && _customInnerAreas.Count > 0)
                _finalInnerAreas.AddRange(_customInnerAreas);
            else
            {
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.DoIfNotNull(() => _finalInnerAreas.Add(rectTransform));
            }

            if (_finalInnerAreas.Count == 0)
                DebugExtension.NDLogWarning("$$> ".ToColor(GoodColors.Red), "_finalInnerAreas.Count is zero!");
        }
        #endregion Controller - Setup
    }
}
