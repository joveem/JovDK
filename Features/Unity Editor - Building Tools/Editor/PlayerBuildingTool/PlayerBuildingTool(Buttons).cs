// system / unity
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Reporting;

// third
// ...

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Unity.Editor.Build
{
    public partial class PlayerBuildingTool : EditorWindow
    {
        void BuildAndroidButton()
        {
            HandleBuildVersions();
            BuildAndroid();
        }

        void BuildAllSelectedPlatforms()
        {
            // ! NOT IMPLEMENTED!!!
            DebugExtension.DevLogWarning("NOT IMPLEMENTED!!!".ToColor(GoodColors.Red));
            // ...
            // HandleBuildVersions();
        }

        void OpenBuildFolderButton()
        {
            string buildFoulderPath = GetBuildFolderPatch();

            Application.OpenURL("file://" + buildFoulderPath);

            // #if UNITY_EDITOR_WIN
            //             // Application.OpenURL("file://" + buildPath);
            // #else
            //             // Application.OpenURL("file://" + buildPath);
            // #endif
        }

        string GetBuildFolderPatch()
        {
            // base configs
            string buildFolderName = "_BUILDS";
            string assetsFolderPath = Application.dataPath;
            string buildFoulderPath = assetsFolderPath + "/../" + buildFolderName;

            Directory.CreateDirectory(buildFoulderPath);

            return buildFoulderPath;
        }
    }
}
