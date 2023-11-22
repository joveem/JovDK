using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

using JovDK.Debug;

public class Screenshot : MonoBehaviour
{
    [MenuItem("JovDK/Tools/Play Mode/Screenshot! #p")]    
    static void DoSomething()
    {
        DateTime nowDateTime = DateTime.Now;

        string screenshotSufix = "scr";
        string screenshotExtension = ".jpg";
        string screenDimension = Screen.width + "x" + Screen.height;

        string nowDate =
            nowDateTime.Year.ToString("0000") + "-" +
            nowDateTime.Month.ToString("00") + "-" +
            nowDateTime.Day.ToString("00");
        string nowTime =
            nowDateTime.Hour.ToString("00") + "-" +
            nowDateTime.Minute.ToString("00") + "-" +
            nowDateTime.Second.ToString("00");

        string folderName_ = nowDate;
        string fileName_ = screenshotSufix + "_" + screenDimension + "_" + nowTime + screenshotExtension;

        Directory.CreateDirectory("Assets/Screenshots/" + folderName_);

        ScreenCapture.CaptureScreenshot("Assets/Screenshots/" + folderName_ + "/" + fileName_, 1);
        DebugExtension.DevLogWarning("SCREENSHOT!");
    }

}
