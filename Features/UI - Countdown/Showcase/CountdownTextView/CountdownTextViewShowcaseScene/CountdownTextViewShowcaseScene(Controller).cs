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


namespace JovDK.UI.CountDown.Testing.Showcase
{
    public partial class CountdownTextViewShowcaseScene : MonoBehaviour
    {
        void SetInitialState()
        {
            TimeSpan additionalTime = new TimeSpan(0, 0, _testDeadlineDurationInSeconds);
            _deadLineTime = DateTime.UtcNow.Add(additionalTime);
        }

        void HandleCountDownUpdate(float deltaTime)
        {
            _elapsedTimeFromLastCountdownUpdate += deltaTime;

            if (_elapsedTimeFromLastCountdownUpdate >= _countdownUpdateGap)
            {
                _elapsedTimeFromLastCountdownUpdate -= _countdownUpdateGap;

                _notNegativeCountdownTextView.ApplyTimeNotNegative(DateTime.UtcNow, _deadLineTime, null, GoodColors.Red);
                _countdownTextView.ApplyTime(DateTime.UtcNow, _deadLineTime, GoodColors.Green, GoodColors.Red);
                _countdownRectView.ApplyTime(DateTime.UtcNow, _deadLineTime, new TimeSpan(0, 0, 0, _testDeadlineDurationInSeconds));
            }
        }
    }
}
