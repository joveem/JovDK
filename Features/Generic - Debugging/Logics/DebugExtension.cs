// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using UnityObject = UnityEngine.Object;

// third
// ...

// from company
// ...

// from project
// ...


namespace JovDK.Debugging
{
    public static partial class DebugExtension
    {
        /// <summary>
        /// Logs a default System.Exception message.
        /// </summary>
        /// <param name="message">
        /// The message of System.Exception.
        /// </param>
        static public void LogException(string message)
        {
            Debug.LogException(new Exception(message));
        }


        static public void NotImplementedLogWarning()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            DevLogWarning(stackBackSteps, "[NOT IMPLEMENTED!]".ToColor(GoodColors.Red));
#endif
        }

        static public void DefaultGenericLog(params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            string logPrefix = ">".ToColor(GoodColors.Orange);

            if (messages is not null)
            {
                List<string> messages2 = new List<string>();

                messages2.Add(logPrefix);
                messages2.Add(" ");
                messages2.AddRange(messages);

                DevLog(stackBackSteps, context: null, messages2.ToArray());
            }
            else
                DevLog(stackBackSteps, context: null, logPrefix);
#endif
        }

        static public void DefaultButtonLog(params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            string logPrefix = "#>".ToColor(GoodColors.Blue);

            if (messages is not null)
            {
                List<string> messages2 = new List<string>();

                messages2.Add(logPrefix);
                messages2.Add(" ");
                messages2.AddRange(messages);

                DevLog(stackBackSteps, context: null, messages2.ToArray());
            }
            else
                DevLog(stackBackSteps, context: null, logPrefix);
#endif
        }

        static public void DefaultCallbackLog(params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            string logPrefix = ">".ToColor(GoodColors.Pink);

            if (messages is not null)
            {
                List<string> messages2 = new List<string>();

                messages2.Add(logPrefix);
                messages2.Add(" ");
                messages2.AddRange(messages);

                DevLog(stackBackSteps, context: null, messages2.ToArray());
            }
            else
                DevLog(stackBackSteps, context: null, logPrefix);
#endif
        }

        static public void DefaultSubscriptionLog(params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            string logPrefix = "#>".ToColor(GoodColors.Pink);

            if (messages is not null)
            {
                List<string> messages2 = new List<string>();

                messages2.Add(logPrefix);
                messages2.Add(" ");
                messages2.AddRange(messages);

                DevLog(stackBackSteps, context: null, messages2.ToArray());
            }
            else
                DevLog(stackBackSteps, context: null, logPrefix);
#endif
        }

        /// <summary>
        /// Logs a dev message in Unity Editor or Development builds.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLog(params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            DevLog(stackBackSteps, context: null, messages);
#endif
        }

        /// <summary>
        /// Logs a dev message in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLog(
            int stackBackSteps,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            stackBackSteps++;
            DevLog(stackBackSteps, context: null, messages);
#endif
        }

        /// <summary>
        /// Logs a dev message in Unity Editor or Development builds.
        /// </summary>
        /// <param name="context">
        /// Optional UnityEngine.Object used to associate the warning in the Console.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLog(
            UnityObject context = null,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            DevLog(stackBackSteps, context, messages);
#endif
        }

        /// <summary>
        /// Logs a dev message in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// <param name="context">
        /// Optional UnityEngine.Object used to associate the warning in the Console.
        /// </param>
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLog(
            int stackBackSteps,
            UnityObject context = null,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            stackBackSteps++;
            string debugText = DevLogText(stackBackSteps, messages);

            if (context is null || context == null)
                Debug.Log(debugText);
            else
                Debug.Log(debugText, context);
#endif
        }

        /// <summary>
        /// Logs a warning in Unity Editor or Development builds.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogWarning(params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            DevLogWarning(stackBackSteps, context: null, messages);
#endif
        }

        /// <summary>
        /// Logs a warning in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogWarning(
            int stackBackSteps,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            stackBackSteps++;
            DevLogWarning(stackBackSteps, context: null, messages);
#endif
        }

        /// <summary>
        /// Logs a warning in Unity Editor or Development builds.
        /// </summary>
        /// </param>
        /// <param name="context">
        /// Optional UnityEngine.Object used to associate the warning in the Console.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogWarning(
            UnityObject context = null,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            DevLogWarning(stackBackSteps, context, messages);
#endif
        }

        /// <summary>
        /// Logs a warning in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="context">
        /// Optional UnityEngine.Object used to associate the warning in the Console.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogWarning(
            int stackBackSteps,
            UnityObject context = null,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            stackBackSteps++;
            string debugText = DevLogText(stackBackSteps, messages);

            if (context is null || context == null)
                Debug.LogWarning(debugText);
            else
                Debug.LogWarning(debugText, context);
#endif
        }

        /// <summary>
        /// Logs an error in Unity Editor or Development builds.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogError(params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            DevLogError(stackBackSteps, context: null, messages);
#endif
        }

        /// <summary>
        /// Logs an error in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogError(
            int stackBackSteps,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            stackBackSteps++;
            DevLogError(stackBackSteps, context: null, messages);
#endif
        }

        /// <summary>
        /// Logs an error in Unity Editor or Development builds.
        /// </summary>
        /// <param name="context">
        /// Optional UnityEngine.Object used to associate the warning in the Console.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogError(
            UnityObject context = null,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 1;
            DevLogError(stackBackSteps, context: null, messages);
#endif
        }
        /// <summary>
        /// Logs an error in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="context">
        /// Optional UnityEngine.Object used to associate the warning in the Console.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogError(
            int stackBackSteps,
            UnityObject context = null,
            params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            stackBackSteps++;
            string debugText = DevLogText(stackBackSteps, messages);

            if (context is null || context == null)
                Debug.LogError(debugText);
            else
                Debug.LogError(debugText, context);
#endif
        }

        /// <summary>
        /// Formats dev log messages.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        /// <returns>
        /// Formatted debug message.
        /// </returns>
        static string DevLogText(params string[] messages)
        {
            int stackBackSteps = 1;
            return DevLogText(stackBackSteps, messages);
        }

        /// <summary>
        /// Formats dev log messages.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        /// <returns>
        /// Formatted debug message.
        /// </returns>
        static string DevLogText(int stackBackSteps = 0, params string[] messages)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stackBackSteps++;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            stringBuilder.AppendWithColor("DEVLOG", GoodColors.Pink);
            stringBuilder.Append(" | ");
#endif

#if UNITY_EDITOR || (DEVELOPMENT_BUILD && !UNITY_WEBGL)
            System.Reflection.MethodBase _methodInfo = null;
            StackFrame _stackFrame = null;

            bool hasAlreadyDecreasedStackBackSteps = false;

            while (_methodInfo == null && stackBackSteps >= 0)
            {
                if (hasAlreadyDecreasedStackBackSteps)
                    Debug.Log("hasAlreadyDecreasedStackBackSteps = " + hasAlreadyDecreasedStackBackSteps + " | stackBackSteps = " + stackBackSteps);

                _stackFrame = new StackFrame(stackBackSteps, true);
                _methodInfo = _stackFrame.GetMethod();

                stackBackSteps--;
                hasAlreadyDecreasedStackBackSteps = true;
            }

            if (_methodInfo != null)
            {
                stringBuilder.AppendWithColor(_methodInfo.ReflectedType.Name, GoodColors.Yellow);
                stringBuilder.Append(" | ");
            }

            if (_stackFrame != null)
            {
                stringBuilder.AppendWithColor(_stackFrame.GetMethod().Name, GoodColors.Yellow);
                stringBuilder.Append(" | ");
            }

#endif

#if UNITY_EDITOR || DEVELOPMENT_BUILD 
            foreach (string message in messages)
            {
                if (message != null)
                    stringBuilder.Append(message);
                else
                    stringBuilder.Append("<null>");
            }
#endif

            return stringBuilder.ToString();
        }

        static public void NDLog()
        {
            int stackBackSteps = 1;
            NDLog(stackBackSteps, ">".ToColor(GoodColors.Orange));
        }

        /// <summary>
        /// Logs a message even in non-development builds.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLog(params string[] messages)
        {
            int stackBackSteps = 1;
            NDLog(stackBackSteps, messages);
        }

        /// <summary>
        /// Logs a message even in non-development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLog(int stackBackSteps, params string[] messages)
        {
            stackBackSteps++;
            string debugText = NotDevDebugText(stackBackSteps, messages);
            Debug.Log(debugText);
        }

        /// <summary>
        /// Logs a warning even in non-development builds.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLogWarning(params string[] messages)
        {
            int stackBackSteps = 1;
            NDLogWarning(stackBackSteps, messages);
        }

        /// <summary>
        /// Logs a warning even in non-development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLogWarning(int stackBackSteps, params string[] messages)
        {
            stackBackSteps++;
            string debugText = NotDevDebugText(stackBackSteps, messages);
            Debug.LogWarning(debugText);
        }

        /// <summary>
        /// Logs an error even in non-development builds.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLogError(params string[] messages)
        {
            int stackBackSteps = 1;
            NDLogError(stackBackSteps, messages);
        }

        /// <summary>
        /// Logs an error even in non-development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Cumullative stack trace depth.
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLogError(int stackBackSteps, params string[] messages)
        {
            stackBackSteps++;
            string debugText = NotDevDebugText(stackBackSteps, messages);
            Debug.LogError(debugText);
        }

        /// <summary>
        /// Formats production log messages.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Stack trace depth (default is 1).
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        /// <returns>
        /// Formatted debug message.
        /// </returns>
        static string NotDevDebugText(int stackBackSteps = 0, params string[] messages)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stackBackSteps++;

            stringBuilder.AppendWithColor("NDLOG", "#83f");
            stringBuilder.Append(" | ");

#if !UNITY_WEBGL
            StackFrame _stackFrame = new StackFrame(stackBackSteps, true);
            System.Reflection.MethodBase _methodInfo = _stackFrame.GetMethod();

            stringBuilder.AppendWithColor(_methodInfo.ReflectedType.Name, GoodColors.Yellow);
            stringBuilder.Append(" | ");
            stringBuilder.AppendWithColor(_stackFrame.GetMethod().Name, GoodColors.Yellow);
            stringBuilder.Append(" | ");
#endif

            foreach (string message in messages)
                stringBuilder.Append(message);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Draws a vertical debug line from a world position in the Scene view.
        /// </summary>
        /// <param name="globalPosition">
        /// World position to draw from.
        /// </param>
        /// <param name="lineColor">
        /// Line color.
        /// </param>
        public static void DebugPosition(Vector3 globalPosition, Color lineColor)
        {
            float lineSize = 5f;
            Vector3 positionDelta = Vector3.up * lineSize;
            positionDelta = globalPosition + positionDelta;

            UnityEngine.Debug.DrawLine(globalPosition, positionDelta, lineColor);
        }

        /// <summary>
        /// Draws a vertical debug line from a world position in the Scene view with duration.
        /// </summary>
        /// <param name="globalPosition">
        /// World position to draw from.
        /// </param>
        /// <param name="lineColor">
        /// Line color.
        /// </param>
        /// <param name="duration">
        /// Time in seconds to show the line.
        /// </param>
        public static void DebugPosition(Vector3 globalPosition, Color lineColor, float duration)
        {
            float lineSize = 5f;
            Vector3 positionDelta = Vector3.up * lineSize;
            positionDelta = globalPosition + positionDelta;

            UnityEngine.Debug.DrawLine(globalPosition, positionDelta, lineColor, duration);
        }

        /// <summary>
        /// Draws debug lines along a path of positions in the Scene view.
        /// </summary>
        /// <param name="pathPositionsList">
        /// List of world positions.
        /// </param>
        /// <param name="lineColor">
        /// Line color (default red).
        /// </param>
        public static void DebugPath(
            List<Vector3> pathPositionsList,
            Color lineColor = default)
        {
            if (lineColor.Equals(default))
                lineColor = new Color(1f, 0f, 0f);

            for (int i = 0; i < pathPositionsList.Count - 1; i++)
            {
                Vector3 segmendStartPosition = pathPositionsList[i];
                Vector3 segmendEndPosition = pathPositionsList[i + 1];

                UnityEngine.Debug.DrawLine(segmendStartPosition, segmendEndPosition, lineColor);
            }
        }

        /// <summary>
        /// Draws debug lines along a path of positions in the Scene view with duration.
        /// </summary>
        /// <param name="pathPositionsList">
        /// List of world positions.
        /// </param>
        /// <param name="duration">
        /// Time in seconds to show each line.
        /// </param>
        /// <param name="lineColor">
        /// Line color (default red).
        /// </param>
        public static void DebugPath(
            List<Vector3> pathPositionsList,
            float duration,
            Color lineColor = default)
        {
            if (lineColor.Equals(default))
                lineColor = new Color(1f, 0f, 0f);

            for (int i = 0; i < pathPositionsList.Count - 1; i++)
            {
                Vector3 segmendStartPosition = pathPositionsList[i];
                Vector3 segmendEndPosition = pathPositionsList[i + 1];

                UnityEngine.Debug.DrawLine(segmendStartPosition, segmendEndPosition, lineColor, duration);
            }
        }
    }
}
