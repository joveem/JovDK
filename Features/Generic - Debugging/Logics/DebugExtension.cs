// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        static public void NDLog()
        {
            NDLog(">".ToColor(GoodColors.Orange), 4);
        }

        static public void NDLog(string message, int stackBackSteps = 3)
        {
            string debugText = NDDevLogText(message, stackBackSteps);
            Debug.Log(debugText);
        }

        static public void NDLogWarning(string message, int stackBackSteps = 3)
        {
            string debugText = NDDevLogText(message, stackBackSteps);
            Debug.LogWarning(debugText);
        }

        static public void NDLogError(string message, int stackBackSteps = 3)
        {
            string debugText = NDDevLogText(message, stackBackSteps);
            Debug.LogError(debugText);
        }

        static string NDDevLogText(string message, int stackBackSteps = 3)
        {
            string value = "";

            value += "- " + "NDLOG".ToColor("#83f") + " | ";
            value += message.NDDebugText(stackBackSteps);

            return value;
        }

        static public void DevLog()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            DevLog(">".ToColor(GoodColors.Orange), 4);
#endif
        }

        static public void DevLog(string message, int stackBackSteps = 3)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string debugText = DevLogText(message, stackBackSteps);
            Debug.Log(debugText);
#endif
        }

        static public void DevLogWarning(string message, int stackBackSteps = 3)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD 
            string debugText = DevLogText(message, stackBackSteps);
            Debug.LogWarning(debugText);
#endif
        }

        static public void DevLogError(string message, int stackBackSteps = 3)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string debugText = DevLogText(message, stackBackSteps);
            Debug.LogError(debugText);
#endif
        }

        static string DevLogText(string message, int stackBackSteps = 3)
        {
            string value = "";

            value += "- " + "DEVLOG".ToColor(GoodColors.Pink) + " | ";
            value += message.DebugText(stackBackSteps);

            return value;
        }

        static string NDDebugText(this string _text, int stackBackSteps = 3)
        {
            string debugText = "";

#if !UNITY_WEBGL
            StackFrame _stackFrame = new StackFrame(stackBackSteps, true);
            System.Reflection.MethodBase _methodInfo = _stackFrame.GetMethod();

            debugText +=
                _methodInfo.ReflectedType.FullName.ToColor(GoodColors.Yellow) + " | " +
                _stackFrame.GetMethod().Name.ToColor(GoodColors.Yellow) + " | ";
#endif

            debugText += _text;

            return debugText;
        }

        static string DebugText(this string _text, int stackBackSteps = 3)
        {
            string debugText = "";

#if UNITY_EDITOR || (DEVELOPMENT_BUILD && !UNITY_WEBGL)
            StackFrame _stackFrame = new StackFrame(stackBackSteps, true);
            System.Reflection.MethodBase _methodInfo = _stackFrame.GetMethod();

            debugText +=
                _methodInfo.ReflectedType.FullName.ToColor(GoodColors.Yellow) + " | " +
                _stackFrame.GetMethod().Name.ToColor(GoodColors.Yellow) + " | ";
#endif

#if UNITY_EDITOR || DEVELOPMENT_BUILD 
            debugText += _text;
#endif

            return debugText;
        }

        public static void DebugPosition(Vector3 globalPosition, Color lineColor)
        {
            float lineSize = 5f;
            Vector3 positionDelta = Vector3.up * lineSize;
            positionDelta = globalPosition + positionDelta;

            UnityEngine.Debug.DrawLine(globalPosition, positionDelta, lineColor);
        }

        public static void DebugPosition(Vector3 globalPosition, Color lineColor, float duration)
        {
            float lineSize = 5f;
            Vector3 positionDelta = Vector3.up * lineSize;
            positionDelta = globalPosition + positionDelta;

            UnityEngine.Debug.DrawLine(globalPosition, positionDelta, lineColor, duration);
        }

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
