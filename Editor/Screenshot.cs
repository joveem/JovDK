using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

using JovDK.Debug;

public class Screenshot : MonoBehaviour
{
    // Add a menu item named "Do Something" to MyMenu in the menu bar.
    [MenuItem("MyMenu/SHOT!")]
    static void DoSomething()
    {

        DateTime dateNow_ = DateTime.Now;

        string folderName_ = dateNow_.Day.ToString("00") + "-" + dateNow_.Month.ToString("00") + "-" + dateNow_.Year.ToString("0000");
        string fileName_ = "scr_" + Screen.width + "x" + Screen.height + "_" + dateNow_.Hour.ToString("00") + "-" + dateNow_.Minute.ToString("00") + "-" + dateNow_.Second.ToString("00") + ".jpg";

        Directory.CreateDirectory("Assets/Screenshots/" + folderName_);

        ScreenCapture.CaptureScreenshot("Assets/Screenshots/" + folderName_ + "/" + fileName_, 1);
        DebugExtension.DevLogWarning("SHOT!");
    }

}
