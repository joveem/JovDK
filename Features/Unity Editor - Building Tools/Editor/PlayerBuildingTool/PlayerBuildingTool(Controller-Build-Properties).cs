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
        // saved properties - file
        const string _fileAppNameKey = "build-config-file-app-name";
        const string _fileVersionKey = "build-config-file-version";
        // saved properties - app
        const string _hasToUpdateAppVersionKey = "build-config-app-has-to-update-app-version";
        const string _appVersionKey = "build-config-app-version";
        const string _hasToUpdateBundleCodeKey = "build-config-app-has-to-update-bundle-code";
        const string _currentBuildBundleCodeKey = "build-config-app-bundle-code";
        // saved properties - android
        const string _hasToBuildAndroidKey = "build-config-android-has-to-build-android";
        const string _buildApkInsteadOfAabKey = "build-config-android-has-to-build-apk-instead-of-aab";
        const string _keystorePasswordKey = "build-config-android-keystore-password";
        const string _keystoreAliasPasswordKey = "build-config-android-keystore-alias-password";
        // saved properties - build
        const string _isDevelopmentBuildKey = "build-config-build-is-development-build";

        void SaveBuildProperties()
        {
            EditorPrefs.SetString(GetProjectPrefKey(_fileAppNameKey), _fileAppName);
            // File
            EditorPrefs.SetString(GetProjectPrefKey(_fileAppNameKey), _fileAppName);
            EditorPrefs.SetString(GetProjectPrefKey(_fileVersionKey), _fileVersion.ToString());

            // App
            EditorPrefs.SetBool(GetProjectPrefKey(_hasToUpdateAppVersionKey), _hasToUpdateAppVersion);
            EditorPrefs.SetString(GetProjectPrefKey(_appVersionKey), _appVersion.ToString());
            EditorPrefs.SetBool(GetProjectPrefKey(_hasToUpdateBundleCodeKey), _hasToUpdateBundleCode);
            EditorPrefs.SetInt(GetProjectPrefKey(_currentBuildBundleCodeKey), _currentBuildBundleCode);

            // Android
            EditorPrefs.SetBool(GetProjectPrefKey(_hasToBuildAndroidKey), _hasToBuildAndroid);
            EditorPrefs.SetBool(GetProjectPrefKey(_buildApkInsteadOfAabKey), _buildApkInsteadOfAab);
            EditorPrefs.SetString(GetProjectPrefKey(_keystorePasswordKey), _keystorePassword);
            EditorPrefs.SetString(GetProjectPrefKey(_keystoreAliasPasswordKey), _keystoreAliasPassword);

            // Build
            EditorPrefs.SetBool(GetProjectPrefKey(_isDevelopmentBuildKey), _isDevelopmentBuild);
        }

        void LoadBuildProperties()
        {
            // File
            _fileAppName = EditorPrefs.GetString(GetProjectPrefKey(_fileAppNameKey), "UNDEFINED");
            string buildFileVersionAsText = EditorPrefs.GetString(GetProjectPrefKey(_fileVersionKey), "1.0");
            _fileVersion = new Version(buildFileVersionAsText);

            // App
            _hasToUpdateAppVersion = EditorPrefs.GetBool(GetProjectPrefKey(_hasToUpdateAppVersionKey), true);
            string appVersionAsText = EditorPrefs.GetString(GetProjectPrefKey(_appVersionKey), "1.0.0");
            _appVersion = new Version(appVersionAsText);
            _hasToUpdateBundleCode = EditorPrefs.GetBool(GetProjectPrefKey(_hasToUpdateBundleCodeKey), true);
            _currentBuildBundleCode = EditorPrefs.GetInt(GetProjectPrefKey(_currentBuildBundleCodeKey), 1);

            // Android
            _hasToBuildAndroid = EditorPrefs.GetBool(GetProjectPrefKey(_hasToBuildAndroidKey), false);
            _buildApkInsteadOfAab = EditorPrefs.GetBool(GetProjectPrefKey(_buildApkInsteadOfAabKey), false);
            _keystorePassword = EditorPrefs.GetString(GetProjectPrefKey(_keystorePasswordKey), "");
            _keystoreAliasPassword = EditorPrefs.GetString(GetProjectPrefKey(_keystoreAliasPasswordKey), "");

            // Build
            _isDevelopmentBuild = EditorPrefs.GetBool(GetProjectPrefKey(_isDevelopmentBuildKey), false);
        }

        string GetProjectPrefKey(string baseKey)
        {
            // DebugExtension.DevLog("Application.identifier = " + Application.identifier);
            return Application.identifier + "-" + baseKey;
        }
    }
}
