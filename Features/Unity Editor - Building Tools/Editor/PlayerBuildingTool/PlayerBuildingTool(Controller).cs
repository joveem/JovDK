// system / unity
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
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
        void HandleBuildVersions()
        {
            _fileVersion = new Version(_fileVersion.Major, _fileVersion.Minor + 1);

            if (_hasToUpdateBundleCode)
                _currentBuildBundleCode++;

            if (_hasToUpdateAppVersion)
                _appVersion = new Version(_appVersion.Major, _appVersion.Minor, _appVersion.Build + 1);

            SaveBuildProperties();
        }

        public void BuildAndroid(Action OnFinish = null)
        {
            DateTime buildStart = DateTime.UtcNow;

            if (_isDevelopmentBuild)
                DebugExtension.DevLog("[ Android ] ".ToColor(GoodColors.Orange) + "Starting Build " + "( DEV ) ".ToColor(GoodColors.Pink));
            else
                DebugExtension.DevLog("[ Android ] ".ToColor(GoodColors.Orange) + "Starting Build...");

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

            // handle folder/file naming
            string buildFolderVersion = _fileVersion.Major.ToString("0000") + "_" + _fileVersion.Minor.ToString("0000");
            string buildFileName = _fileAppName + "_" + buildFolderVersion + "_android" + (_buildApkInsteadOfAab ? ".apk" : ".aab");
            string buildFilePath = GetBuildFolderPatch() + "/" + buildFileName;

            // handle scenes listing
            List<string> scenesPaths = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                    scenesPaths.Add(scene.path);
            }

            buildPlayerOptions.scenes = scenesPaths.ToArray();
            buildPlayerOptions.locationPathName = buildFilePath;
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.None;

            EditorUserBuildSettings.buildAppBundle = !_buildApkInsteadOfAab;
            EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Debugging; // TODO: REVIEW THIS!

            if (_isDevelopmentBuild)
                buildPlayerOptions.options = BuildOptions.Development;

            PlayerSettings.Android.keystorePass = _keystorePassword;
            PlayerSettings.Android.keyaliasPass = _keystoreAliasPassword;

            PlayerSettings.Android.bundleVersionCode = _currentBuildBundleCode;
            PlayerSettings.bundleVersion = _appVersion.ToString();

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            DateTime buildEnd = DateTime.UtcNow;
            TimeSpan buildDuration = buildEnd.Subtract(buildStart);

            if (summary.result == BuildResult.Succeeded)
                DebugExtension.DevLog("[ Android ] ".ToColor(GoodColors.Green) + "Build succeeded! (duration = " + buildDuration.ToString() + ")  ~" + summary.totalSize / 7943573 + " MB (" + summary.totalSize + " bytes)");

            if (summary.result == BuildResult.Failed)
                DebugExtension.DevLogError("[ Android ] ".ToColor(GoodColors.Red) + "Build failed (duration = " + buildDuration.ToString() + ")");

            OnFinish?.Invoke();
        }
    }
}
