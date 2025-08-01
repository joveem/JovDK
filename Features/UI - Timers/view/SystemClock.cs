// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;
using System.Text;

// from project
// ...


namespace JovDK.UI.Timers
{
    public partial class SystemClock : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        float _clockUpdateRemainingGap = 0f;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] TextMeshProUGUI _mainText;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] bool _showSeconds = true;
        [SerializeField] float _clockUpdateGapInSeconds = 0.2f;
        [Header("text filling")]
        [SerializeField] string _separatorText = " ";
        [SerializeField] string _hoursSufix = "h";
        [SerializeField] string _minutesSufix = "m";
        [SerializeField] string _secondsSufix = "s";
        [Header("time format")]
        [SerializeField] bool _useSystemFormat = true;
        [SerializeField] bool _force24HoursFormat = true;
        [Header("12h format")]
        [SerializeField] string _12HoursFormatIndicatorSeparator = " ";
        [SerializeField] string _12HoursFormatAmText = "AM";
        [SerializeField] string _12HoursFormatPmText = "PM";


        #region MonoBehaviour
        void Awake()
        {
            SetInitialState();
        }

        void Update()
        {
            HandleClockUpdate(Time.deltaTime);
        }
        #endregion MonoBehaviour

        #region Controller
        void SetInitialState()
        {
            // DebugExtension.DefaultGenericLog();

            _clockUpdateRemainingGap = _clockUpdateGapInSeconds;
            ApplyCurrentTimeText();
        }

        void HandleClockUpdate(float deltaTime)
        {
            _clockUpdateRemainingGap -= deltaTime;

            bool hasToUpdateTime = _clockUpdateRemainingGap <= 0f;

            if (hasToUpdateTime)
            {
                // reset gap
                _clockUpdateRemainingGap += _clockUpdateGapInSeconds;

                ApplyCurrentTimeText();
            }
        }
        #endregion Controller

        #region View
        void ApplyCurrentTimeText()
        {
            _mainText.DoIfNotNull(() =>
            {
                DateTime localNow = DateTime.Now;
                StringBuilder stringBuilder = new StringBuilder();

                // ! DEBUG ONLY!!!
                // ! DEBUG ONLY!!!
                // ! DEBUG ONLY!!!
                // localNow = localNow.AddHours(12);
                // localNow = localNow.AddMinutes(-10);
                // ! DEBUG ONLY!!!
                // ! DEBUG ONLY!!!
                // ! DEBUG ONLY!!!

                bool finalFormatIs24Hours = _force24HoursFormat;

                if (_useSystemFormat)
                    finalFormatIs24Hours = DateTimeFormatInfo.CurrentInfo.AMDesignator == "";

                // DebugExtension.DevLog("_useSystemFormat = ", _useSystemFormat.ToString());
                // DebugExtension.DevLog("_force24HoursFormat = ", _force24HoursFormat.ToString());
                // DebugExtension.DevLog("DateTimeFormatInfo.CurrentInfo.AMDesignator = ", "\"", DateTimeFormatInfo.CurrentInfo.AMDesignator.SerializeObjectToJSON(), "\"");
                // DebugExtension.DevLog("finalFormatIs24Hours = ", finalFormatIs24Hours.ToString());

                // hours
                if (finalFormatIs24Hours)
                    stringBuilder.Append(localNow.ToString("HH"));
                else
                    stringBuilder.Append(localNow.ToString("hh"));

                stringBuilder.Append(_hoursSufix);

                // separator
                stringBuilder.Append(_separatorText);

                // minutes
                stringBuilder.Append(localNow.Minute.ToString("00"));
                stringBuilder.Append(_minutesSufix);

                if (_showSeconds)
                {
                    // separator
                    stringBuilder.Append(_separatorText);

                    // seconds
                    stringBuilder.Append(localNow.Second.ToString("00"));
                    stringBuilder.Append(_secondsSufix);
                }

                if (!finalFormatIs24Hours)
                {
                    // separator
                    stringBuilder.Append(_12HoursFormatIndicatorSeparator);

                    // 12 hours format sufix
                    stringBuilder.Append(localNow.ToString("tt") == "AM" ? _12HoursFormatAmText : _12HoursFormatPmText);
                }

                _mainText.text = stringBuilder.ToString();
            });
        }
        #endregion View
    }
}
