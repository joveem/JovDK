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
using JovDK.Debugging;
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

        void BuildPcButton()
        {
            HandleBuildVersions();
            BuildPc();
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
            string assetsFolderPath = Application.dataPath;
            string buildFoulderPath = Path.GetFullPath(Path.Combine(assetsFolderPath, "..", "_bin", "_app-player-builds"));

            Directory.CreateDirectory(buildFoulderPath);

            return buildFoulderPath;
        }
    }
}
