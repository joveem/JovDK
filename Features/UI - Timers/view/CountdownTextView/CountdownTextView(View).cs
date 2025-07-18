// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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


namespace JovDK.UI.Timers
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
            StringBuilder stringBuilder = new StringBuilder();

            int numbersAmount = _timeFractionsAmount;

            if (numbersAmount < 1)
            {
                numbersAmount = 1;
                DebugExtension.DevLogWarning(
                    "$> ".ToColor(GoodColors.Red),
                    "_timeFractionsAmount = ", _timeFractionsAmount.ToString(), "\n",
                    "_maxTimeFraction = ", _maxTimeFraction.ToString(), "\n",
                    "");
            }

            TimeFraction minTimeFraction = _maxTimeFraction - (numbersAmount + 1);
            List<int> numbers = new List<int>();

            switch (_maxTimeFraction)
            {
                case TimeFraction.Day:
                    {
                        int daysSum = duration.Days;

                        int finalValue = daysSum;
                        numbers.Add(finalValue);

                        if (numbersAmount >= 2)
                            numbers.Add(duration.Hours);

                        if (numbersAmount >= 3)
                            numbers.Add(duration.Minutes);

                        if (numbersAmount >= 4)
                            numbers.Add(duration.Seconds);

                        if (numbersAmount >= 5)
                            numbers.Add(duration.Milliseconds);
                        break;
                    }

                case TimeFraction.Hour:
                    {
                        int daysSum = duration.Days;
                        int hoursSum = duration.Hours + (daysSum * 24);

                        int finalValue = hoursSum;
                        numbers.Add(finalValue);

                        if (numbersAmount >= 2)
                            numbers.Add(duration.Minutes);

                        if (numbersAmount >= 3)
                            numbers.Add(duration.Seconds);

                        if (numbersAmount >= 4)
                            numbers.Add(duration.Milliseconds);
                        break;
                    }

                case TimeFraction.Minute:
                    {
                        int daysSum = duration.Days;
                        int hoursSum = duration.Hours + (daysSum * 24);
                        int minutesSum = duration.Minutes + (hoursSum * 60);

                        int finalValue = minutesSum;
                        numbers.Add(finalValue);

                        if (numbersAmount >= 2)
                            numbers.Add(duration.Seconds);

                        if (numbersAmount >= 3)
                            numbers.Add(duration.Milliseconds);
                        break;
                    }

                case TimeFraction.Second:
                    {
                        int daysSum = duration.Days;
                        int hoursSum = duration.Hours + (daysSum * 24);
                        int minutesSum = duration.Minutes + (hoursSum * 60);
                        int secondsSum = duration.Seconds + (minutesSum * 60);

                        int finalValue = secondsSum;
                        numbers.Add(finalValue);

                        if (numbersAmount >= 2)
                            numbers.Add(duration.Milliseconds);
                        break;
                    }

                case TimeFraction.Millisecond:
                    {
                        int daysSum = duration.Days;
                        int hoursSum = duration.Hours + (daysSum * 24);
                        int minutesSum = duration.Minutes + (hoursSum * 60);
                        int secondsSum = duration.Seconds + (minutesSum * 60);
                        int millisecondSum = duration.Milliseconds + (secondsSum * 1000);

                        int finalValue = millisecondSum;
                        numbers.Add(finalValue);
                        break;
                    }

                default:
                    {
                        DebugExtension.DevLog(
                            "$ > ".ToColor(GoodColors.Red),
                            "UNEXPECTED _maxTimeFraction!", "\n",
                            "_maxTimeFraction = ", _maxTimeFraction.ToString(), "\n",
                            "");

                        int daysSum = duration.Days;
                        int hoursSum = duration.Hours + (daysSum * 24);
                        int minutesSum = duration.Minutes + (hoursSum * 60);

                        int finalValue = minutesSum;
                        numbers.Add(finalValue);
                        numbers.Add(duration.Seconds);

                        break;
                    }
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                bool isLastNumber = i == numbers.Count - 1;

                int numberValue = numbers[i];

                if (!isLastNumber || minTimeFraction != TimeFraction.Millisecond)
                    stringBuilder.Append(numberValue.ToString("00"));
                else
                    stringBuilder.Append(numberValue.ToString("0000"));

                if (!isLastNumber)
                    stringBuilder.Append(_separatorCharacter);
            }

            return stringBuilder.ToString();
        }

        void HandleTextColoring(
            bool isNegative,
            string positiveHexColor = null,
            string negativeHexColor = null)
        {
            if (_initialColor.Equals(default))
                _text.DoIfNotNull(() => _initialColor = _text.color);

            if (!isNegative)
            {
                if (positiveHexColor != null)
                    _text.color = Color.white;
                else
                    _text.color = _initialColor;
                    // _text.color = Color.cyan;
            }
            else
            {
                if (negativeHexColor != null)
                    _text.color = Color.white;
                else
                    _text.color = _initialColor;
                    // _text.color = Color.cyan;
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
