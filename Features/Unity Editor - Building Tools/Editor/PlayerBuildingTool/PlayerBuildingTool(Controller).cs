// system / unity
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        const string SevenZipExecutable = "7z.exe";
        static readonly string[] SevenZipExcludeTokens = new string[]
        {
            "_DoNotShip",
            "_ButDontShipItWithYourGame"
        };

        void HandleBuildVersions()
        {
            _fileVersion = new Version(_fileVersion.Major, _fileVersion.Minor + 1);

            if (_hasToUpdateBundleCode)
                _currentBuildBundleCode++;

            if (_hasToUpdateAppVersion)
                _appVersion = new Version(_appVersion.Major, _appVersion.Minor, _appVersion.Build + 1);

            SaveBuildProperties();
        }

        public void BuildPc(Action OnFinish = null)
        {
            DateTime buildStart = DateTime.UtcNow;

            if (_isDevelopmentBuild)
                DebugExtension.DevLog("[ PC ] ".ToColor(GoodColors.Orange) + "Starting Build " + "( DEV ) ".ToColor(GoodColors.Pink));
            else
                DebugExtension.DevLog("[ PC ] ".ToColor(GoodColors.Orange) + "Starting Build...");

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

            // handle folder/file naming
            string buildFolderVersion = _fileVersion.Major.ToString("0000") + "_" + _fileVersion.Minor.ToString("0000");
            string buildOutputFolderName = _fileAppName + "_" + buildFolderVersion + "_pc" + (_isDevelopmentBuild ? "_DEV" : "");
            string buildOutputFolder = Path.Combine(GetBuildFolderPatch(), buildOutputFolderName);
            string buildFileName = Path.Combine(buildOutputFolderName, PlayerSettings.productName + ".exe");
            string buildFilePath = Path.Combine(GetBuildFolderPatch(), buildFileName);

            // handle scenes listing
            List<string> scenesPaths = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                    scenesPaths.Add(scene.path);
            }

            buildPlayerOptions.scenes = scenesPaths.ToArray();
            buildPlayerOptions.locationPathName = buildFilePath;
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.None;

            // EditorUserBuildSettings.buildAppBundle = !_buildApkInsteadOfAab;
            // EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Debugging; // TODO: REVIEW THIS!

            if (_isDevelopmentBuild)
                buildPlayerOptions.options = BuildOptions.Development;

            // PlayerSettings.Android.keystorePass = _keystorePassword;
            // PlayerSettings.Android.keyaliasPass = _keystoreAliasPassword;

            // PlayerSettings.Android.bundleVersionCode = _currentBuildBundleCode;
            string previousBundleVersion = PlayerSettings.bundleVersion;
            PlayerSettings.bundleVersion = _appVersion.ToString();

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            DateTime buildEnd = DateTime.UtcNow;
            TimeSpan buildDuration = buildEnd.Subtract(buildStart);

            switch (summary.result)
            {
                case BuildResult.Succeeded:
                    {
                        DebugExtension.DevLog(
                            "[ PC ] ".ToColor(GoodColors.Green),
                            "Build succeeded! ",
                            "(v", _appVersion.ToString(), " | duration = ", buildDuration.ToString(), ")  ",
                            "~", (summary.totalSize / 7943573).ToString(), "MB ",
                            "(", summary.totalSize.ToString(), " bytes)", "\n",
                            "");

                        break;
                    }

                case BuildResult.Failed:
                    {
                        DebugExtension.DevLogError(
                            "[ PC ] ".ToColor(GoodColors.Red),
                            "Build failed ",
                            "(duration = ", buildDuration.ToString(), ")", "\n",
                            "");

                        break;
                    }

                case BuildResult.Cancelled:
                    {
                        DebugExtension.DevLogError(
                            "[ PC ] ".ToColor(GoodColors.Red),
                            "Build cancelled ",
                            "(duration = ", buildDuration.ToString(), ")", "\n",
                            "");

                        break;
                    }

                default:
                    {
                        DebugExtension.DevLogWarning(
                            "$$> ".ToColor(GoodColors.Red),
                            "[ PC ] ".ToColor(GoodColors.Orange),
                            "Unexpected build result!", " ",
                            "(summary.result = ", summary.result.ToString(), " | ",
                            "duration = ", buildDuration.ToString(), ")", "\n",
                            "");

                        break;
                    }
            }

            bool buildSucceeded = summary.result == BuildResult.Succeeded;
            LogBuildResult("[ PC ] ", buildSucceeded);

            if (buildSucceeded)
            {
                bool compressionSucceeded = TryCompressBuildFolder(buildOutputFolder, out string archivePath, out string compressionMessage);
                LogCompressionResult("[ PC ] ", compressionSucceeded, archivePath, compressionMessage);
            }
            else
            {
                LogCompressionResult("[ PC ] ", false, null, "Compression skipped because build did not succeed.");
            }

            PlayerSettings.bundleVersion = previousBundleVersion;
            OnFinish?.Invoke();
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
            string buildOutputFolderName = _fileAppName + "_" + buildFolderVersion + "_android" + (_isDevelopmentBuild ? "_DEV" : "");
            string buildOutputFolder = Path.Combine(GetBuildFolderPatch(), buildOutputFolderName);
            string buildFileName = buildOutputFolderName + (_buildApkInsteadOfAab ? ".apk" : ".aab");
            string buildFilePath = Path.Combine(buildOutputFolder, buildFileName);

            Directory.CreateDirectory(buildOutputFolder);

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

            bool buildSucceeded = summary.result == BuildResult.Succeeded;
            LogBuildResult("[ Android ] ", buildSucceeded);

            if (buildSucceeded)
            {
                bool compressionSucceeded = TryCompressBuildFolder(buildOutputFolder, out string archivePath, out string compressionMessage);
                LogCompressionResult("[ Android ] ", compressionSucceeded, archivePath, compressionMessage);
            }
            else
            {
                LogCompressionResult("[ Android ] ", false, null, "Compression skipped because build did not succeed.");
            }

            OnFinish?.Invoke();
        }

        static void LogBuildResult(string tag, bool succeeded)
        {
            if (succeeded)
                DebugExtension.DevLog(tag.ToColor(GoodColors.Green) + "Build result: Succeeded.");
            else
                DebugExtension.DevLogWarning(tag.ToColor(GoodColors.Red) + "Build result: Failed or Cancelled.");
        }

        static void LogCompressionResult(string tag, bool succeeded, string archivePath, string message)
        {
            if (succeeded)
            {
                DebugExtension.DevLog(
                    tag.ToColor(GoodColors.Green),
                    "Compression succeeded: ",
                    archivePath ?? "(unknown archive path)",
                    message != null ? " | " + message : "");
            }
            else
            {
                DebugExtension.DevLogError(
                    tag.ToColor(GoodColors.Red),
                    "Compression failed: ",
                    message ?? "(no details)");
            }
        }

        static bool TryCompressBuildFolder(string buildOutputFolder, out string archivePath, out string message)
        {
            archivePath = null;
            message = null;

            if (string.IsNullOrWhiteSpace(buildOutputFolder))
            {
                message = "Build output folder path was empty.";
                return false;
            }

            if (!Directory.Exists(buildOutputFolder))
            {
                message = "Build output folder does not exist: " + buildOutputFolder;
                return false;
            }

            DirectoryInfo buildDir = new DirectoryInfo(buildOutputFolder);
            DirectoryInfo parentDir = buildDir.Parent;
            if (parentDir == null)
            {
                message = "Cannot resolve parent directory for build output folder: " + buildOutputFolder;
                return false;
            }

            archivePath = Path.Combine(parentDir.FullName, buildDir.Name + ".7z");

            List<string> relativeFiles = GetCompressibleRelativeFiles(buildOutputFolder);
            if (relativeFiles.Count == 0)
            {
                message = "No files found to compress after exclusions.";
                return false;
            }

            string listFileName = "_7z_file_list_" + Guid.NewGuid().ToString("N") + ".txt";
            string listFilePath = Path.Combine(buildOutputFolder, listFileName);

            try
            {
                File.WriteAllLines(listFilePath, relativeFiles);

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = SevenZipExecutable,
                    Arguments = "a -t7z -mx=9 -y \"" + archivePath + "\" @" + listFileName,
                    WorkingDirectory = buildOutputFolder,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    if (process == null)
                    {
                        message = "Failed to start 7-Zip process.";
                        return false;
                    }

                    string stdout = process.StandardOutput.ReadToEnd();
                    string stderr = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        message = "7-Zip exited with code " + process.ExitCode + ". " + (string.IsNullOrWhiteSpace(stderr) ? stdout : stderr);
                        return false;
                    }

                    message = string.IsNullOrWhiteSpace(stdout) ? "7-Zip completed successfully." : stdout.Trim();
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Compression failed: " + ex.Message;
                return false;
            }
            finally
            {
                try
                {
                    if (File.Exists(listFilePath))
                        File.Delete(listFilePath);
                }
                catch
                {
                    // best effort cleanup
                }
            }
        }

        static List<string> GetCompressibleRelativeFiles(string buildOutputFolder)
        {
            List<string> result = new List<string>();
            foreach (string filePath in Directory.EnumerateFiles(buildOutputFolder, "*", SearchOption.AllDirectories))
            {
                if (IsExcludedPath(filePath))
                    continue;

                string relativePath = Path.GetRelativePath(buildOutputFolder, filePath);
                if (IsExcludedPath(relativePath))
                    continue;

                result.Add(relativePath);
            }

            return result;
        }

        static bool IsExcludedPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            for (int i = 0; i < SevenZipExcludeTokens.Length; i++)
            {
                if (path.IndexOf(SevenZipExcludeTokens[i], StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }

            return false;
        }
    }
}
