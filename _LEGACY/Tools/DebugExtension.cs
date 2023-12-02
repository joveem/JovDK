// system / unity
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
    }

}
