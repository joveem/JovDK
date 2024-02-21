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
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.UI.CountDown
{
    public partial class CountdownTextView : MonoBehaviour
    {
        public void ApplyTime(
            DateTime currentTime,
            DateTime deadlineTime,
            string positiveHexColor = null,
            string negativeHexColor = null,
            bool roundMilliseconds = true)
        {
            _text.DoIfNotNull(() =>
            {
                if (roundMilliseconds)
                {
                    currentTime = RoundDateTimeMilliseconds(currentTime);
                    deadlineTime = RoundDateTimeMilliseconds(deadlineTime);
                }

                TimeSpan duration = deadlineTime.Subtract(currentTime);
                bool isNegative = duration < TimeSpan.Zero;
                string durationText = GetDurationText(duration, isNegative, positiveHexColor, negativeHexColor);

                HandleTextColoring(isNegative, positiveHexColor, negativeHexColor);
                _text.text = durationText;
            });
        }

        public void ApplyTimeNotNegative(
            DateTime currentTime,
            DateTime deadlineTime,
            string positiveHexColor = null,
            string negativeHexColor = null,
            bool roundMilliseconds = true)
        {
            _text.DoIfNotNull(() =>
            {
                if (roundMilliseconds)
                {
                    currentTime = RoundDateTimeMilliseconds(currentTime);
                    deadlineTime = RoundDateTimeMilliseconds(deadlineTime);
                }

                TimeSpan duration = deadlineTime.Subtract(currentTime);

                if (duration < TimeSpan.Zero)
                    duration = TimeSpan.Zero;

                bool isNegative = duration < TimeSpan.Zero;
                string durationText = GetDurationText(duration, isNegative, positiveHexColor, negativeHexColor);

                HandleTextColoring(isNegative, positiveHexColor, negativeHexColor);
                _text.text = durationText;
            });
        }

        DateTime RoundDateTimeMilliseconds(DateTime value)
        {
            value = new DateTime(
                        value.Year,
                        value.Month,
                        value.Day,
                        value.Hour,
                        value.Minute,
                        value.Second);

            return value;
        }

        string GetDurationText(
            TimeSpan duration,
            bool isNegative,
            string positiveHexColor = null,
            string negativeHexColor = null)
        {
            string value = "";

            int firstNumber;
            int secondNumber;

            switch (_maxTimeFraction)
            {
                case TimeFraction.Day:
                    {
                        firstNumber = duration.Days;
                        secondNumber = duration.Hours;
                        break;
                    }

                case TimeFraction.Hour:
                    {
                        firstNumber = duration.Hours + (duration.Days * 24);
                        secondNumber = duration.Minutes;
                        break;
                    }

                case TimeFraction.Minute:
                    {
                        firstNumber = duration.Minutes + (duration.Hours * 60) + (duration.Days * 24 * 60);
                        secondNumber = duration.Seconds;
                        break;
                    }

                default:
                    {
                        string debugText =
                            "$ > ".ToColor(GoodColors.Red) +
                            "ERROR trying to GetDurationText!" + "\n" +
                            "UNEXPECTED _maxTimeFraction!" + "\n" +
                            "";
                        DebugExtension.DevLog(debugText);

                        firstNumber = duration.Minutes + (duration.Hours * 60) + (duration.Days * 24 * 60);
                        secondNumber = duration.Seconds;

                        break;
                    }
            }

            string firstNumberText = Mathf.Abs(firstNumber).ToString("00");
            string secondNumberText = Mathf.Abs(secondNumber).ToString("00");

            value = firstNumberText + _separatorCharacter + secondNumberText;
            value = HandleTextContentColoring(value, isNegative, positiveHexColor, negativeHexColor);

            return value;
        }

        void HandleTextColoring(
            bool isNegative,
            string positiveHexColor = null,
            string negativeHexColor = null)
        {
            if (!isNegative)
            {
                if (positiveHexColor != null)
                    _text.color = Color.white;
                else
                    _text.color = _initialColor;
            }
            else
            {
                if (negativeHexColor != null)
                    _text.color = Color.white;
                else
                    _text.color = _initialColor;
            }
        }

        string HandleTextContentColoring(
            string value,
            bool isNegative,
            string positiveHexColor = null,
            string negativeHexColor = null)
        {
            if (!isNegative)
            {
                if (positiveHexColor != null)
                    value = value.ToColor(positiveHexColor);
            }
            else
            {
                if (negativeHexColor != null)
                    value = value.ToColor(negativeHexColor);
            }

            return value;
        }
    }
}
