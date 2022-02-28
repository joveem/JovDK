using System.Diagnostics;


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

#if UNITY_EDITOR || DEVELOPMENT_BUILD

        StackFrame _stackFrame = new StackFrame(2, true);
        System.Reflection.MethodBase _methodInfo = _stackFrame.GetMethod();

        return _methodInfo.ReflectedType.FullName.ToColor(GoodCollors.yellow) + " | " + _stackFrame.GetMethod().Name.ToColor(GoodCollors.yellow) + " | " + _text;

#endif

        }
    }

}