// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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

        static public void DevLog()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            int stackBackSteps = 3;
            DevLog(stackBackSteps, ">".ToColor(GoodColors.Orange));
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
            int stackBackSteps = 3;
            DevLog(stackBackSteps, messages);
#endif
        }

        /// <summary>
        /// Logs a dev message in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Stack trace depth (default is 3).
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLog(int stackBackSteps, params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string debugText = DevLogText(stackBackSteps, messages);
            Debug.Log(debugText);
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
            int stackBackSteps = 3;
            DevLogWarning(stackBackSteps, messages);
#endif
        }

        /// <summary>
        /// Logs a warning in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Stack trace depth (default is 3).
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogWarning(int stackBackSteps, params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string debugText = DevLogText(stackBackSteps, messages);
            Debug.LogWarning(debugText);
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
            int stackBackSteps = 3;
            DevLogError(stackBackSteps, messages);
#endif
        }

        /// <summary>
        /// Logs an error in Unity Editor or Development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Stack trace depth (default is 3).
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void DevLogError(int stackBackSteps, params string[] messages)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string debugText = DevLogText(stackBackSteps, messages);
            Debug.LogError(debugText);
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

        static string DevLogText(int stackBackSteps = 1, params string[] messages)
        {
            StringBuilder stringBuilder = new StringBuilder();

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
                stringBuilder.AppendWithColor(_methodInfo.ReflectedType.FullName, GoodColors.Yellow);
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
            NDLog(4, ">".ToColor(GoodColors.Orange));
        }

        /// <summary>
        /// Logs a message even in non-development builds.
        /// </summary>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLog(params string[] messages)
        {
            int stackBackSteps = 3;
            NDLog(stackBackSteps, messages);
        }

        /// <summary>
        /// Logs a message even in non-development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Stack trace depth (default is 3).
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLog(int stackBackSteps = 3, params string[] messages)
        {
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
            int stackBackSteps = 3;
            NDLogWarning(stackBackSteps, messages);
        }

        /// <summary>
        /// Logs a warning even in non-development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Stack trace depth (default is 3).
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLogWarning(int stackBackSteps = 3, params string[] messages)
        {
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
            int stackBackSteps = 3;
            NDLogError(stackBackSteps, messages);
        }

        /// <summary>
        /// Logs an error even in non-development builds.
        /// </summary>
        /// <param name="stackBackSteps">
        /// Stack trace depth (default is 3).
        /// </param>
        /// <param name="messages">
        /// Messages to log (concatenated without spacing).
        /// </param>
        static public void NDLogError(int stackBackSteps = 3, params string[] messages)
        {
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
        static string NotDevDebugText(int stackBackSteps = 1, params string[] messages)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendWithColor("NDLOG", "#83f");
            stringBuilder.Append(" | ");

#if !UNITY_WEBGL
            StackFrame _stackFrame = new StackFrame(stackBackSteps, true);
            System.Reflection.MethodBase _methodInfo = _stackFrame.GetMethod();

            stringBuilder.AppendWithColor(_methodInfo.ReflectedType.FullName, GoodColors.Yellow);
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
