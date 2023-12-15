// system / unity
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// third
// ...

// from company
// ...

// from project
// ...


namespace JovDK.Debug
{

    public static class DebugExtension
    {
        static public void DevLog(string _message)
        {

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.Log("- <color=#f0f>DEVLOG</color> | " + _message.DebugText());
#endif

        }
        static public void DevLogWarning(string _message)
        {

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogWarning("- <color=#f0f>DEVLOG</color> | " + _message.DebugText());
#endif

        }
        static public void DevLogError(string _message)
        {

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogError("- <color=#f0f>DEVLOG</color> | " + _message.DebugText());
#endif

        }

        static string DebugText(this string _text)
        {

            string debugText = "";

#if UNITY_EDITOR || (DEVELOPMENT_BUILD && !UNITY_WEBGL)

            StackFrame _stackFrame = new StackFrame(2, true);
            System.Reflection.MethodBase _methodInfo = _stackFrame.GetMethod();

            debugText += _methodInfo.ReflectedType.FullName.ToColor(GoodColors.Yellow) + " | " + _stackFrame.GetMethod().Name.ToColor(GoodColors.Yellow) + " | ";

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
