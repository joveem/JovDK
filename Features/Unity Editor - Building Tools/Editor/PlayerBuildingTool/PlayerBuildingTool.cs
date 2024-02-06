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

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        // File
        string _fileAppName = "UNDEFINED";
        bool _isEditingAppName = false;
        Version _fileVersion = new Version(1, 0);
        bool _isEditingBuildFileVersion = false;

        // App
        bool _hasToUpdateAppVersion = true;
        Version _appVersion = new Version(1, 0, 0);
        bool _isEditingAppVersion = false;
        bool _hasToUpdateBundleCode = true;
        int _currentBuildBundleCode = 1;
        bool _isEditingAppBundleVersion = false;

        // Android
        bool _hasToBuildAndroid = false;
        bool _buildApkInsteadOfAab = false;
        bool _isEditingAndroidKeysorePassword = false;
        string _keystorePassword = "";
        bool _isEditingAndroidKeysoreAliasPassword = false;
        string _keystoreAliasPassword = "";

        // Build
        bool _isDevelopmentBuild = false;
        string _developmentBuildWarning = " (DEV)";


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        [MenuItem("JovDK/Tools/Build")]
        public static void OpenWindow()
        {
            GetWindow<PlayerBuildingTool>("[JD] Build");
        }

        void Awake()
        {
            LoadBuildProperties();
        }

        void OnEnable()
        {
            LoadBuildProperties();
        }
    }
}
