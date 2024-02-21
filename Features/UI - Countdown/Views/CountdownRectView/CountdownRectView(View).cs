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
    public partial class CountdownRectView : MonoBehaviour
    {

        public void ApplyTime(
            DateTime currentTime,
            DateTime deadlineTime,
            TimeSpan maxTime,
            bool roundMilliseconds = true)
        {
            _rectTransform.DoIfNotNull(() =>
            {
                if (roundMilliseconds)
                {
                    currentTime = RoundDateTimeMilliseconds(currentTime);
                    deadlineTime = RoundDateTimeMilliseconds(deadlineTime);
                }

                TimeSpan duration = deadlineTime.Subtract(currentTime);

                float timeFactor = (float)duration.Divide(maxTime);
                timeFactor = Mathf.Clamp(timeFactor, 0f, 1f);

                Vector2 sizeDelta = _rectTransform.sizeDelta;
                sizeDelta.x = _maxRectSize * timeFactor;

                _rectTransform.sizeDelta = sizeDelta;
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
    }
}
