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
        void AppConfigViewSection(
            GUIStyle titleLabelStyle,
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            GUIContent guiContent = new GUIContent("App");
            GUILayout.Label(guiContent, titleLabelStyle);

            UpdateAppVersionField(fieldTitleStylesList, inputFieldStylesList);
            AppVersionField(fieldTitleStylesList, inputFieldStylesList);
            UpdateBundleCodeField(fieldTitleStylesList, inputFieldStylesList);
            AppBundleVersionCodeField(fieldTitleStylesList, inputFieldStylesList);
        }

        void UpdateAppVersionField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            _hasToUpdateAppVersion = GUILayout.Toggle(_hasToUpdateAppVersion, "Update version", fieldTitleStylesList);
        }

        void AppVersionField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            EditorGUILayout.BeginHorizontal();

            if (!_isEditingAppVersion)
            {
                if (_hasToUpdateAppVersion)
                    GUILayout.Label("Next version = ", fieldTitleStylesList);
                else
                    GUILayout.Label("Current version = ", fieldTitleStylesList);

                Version viewAppVersion;

                if (_hasToUpdateAppVersion)
                    viewAppVersion = new Version(_appVersion.Major, _appVersion.Minor, _appVersion.Build + 1);
                else
                    viewAppVersion = new Version(_appVersion.Major, _appVersion.Minor, _appVersion.Build);

                string versionText = "v" + viewAppVersion.ToString();
                GUILayout.Label(versionText, inputFieldStylesList);

                if (GUILayout.Button("Edit", GUILayout.Width(38)))
                    _isEditingAppVersion = !_isEditingAppVersion;
            }
            else
            {
                GUILayout.Label("Current version = ", fieldTitleStylesList);

                GUILayout.Label("v", GUILayout.Width(10));

                int appMajorVersion = EditorGUILayout.IntField(_appVersion.Major, inputFieldStylesList);
                GUILayout.Label(".", GUILayout.Width(6));
                int appMinorVersion = EditorGUILayout.IntField(_appVersion.Minor, inputFieldStylesList);
                GUILayout.Label(".", GUILayout.Width(6));
                int appBuildVersion = EditorGUILayout.IntField(_appVersion.Build, inputFieldStylesList);
                _appVersion = new Version(appMajorVersion, appMinorVersion, appBuildVersion);

                if (GUILayout.Button("Set", GUILayout.Width(38)))
                {
                    _isEditingAppVersion = !_isEditingAppVersion;
                    SaveBuildProperties();
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        void UpdateBundleCodeField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            _hasToUpdateBundleCode = GUILayout.Toggle(_hasToUpdateBundleCode, "Update bundle code", fieldTitleStylesList);
        }

        void AppBundleVersionCodeField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            EditorGUILayout.BeginHorizontal();

            if (!_isEditingAppBundleVersion)
            {
                if (_hasToUpdateBundleCode)
                    GUILayout.Label("Next bundle code = ", fieldTitleStylesList);
                else
                    GUILayout.Label("Current bundle code = ", fieldTitleStylesList);

                string bundleVersionCodeText;

                if (_hasToUpdateBundleCode)
                    bundleVersionCodeText = (_currentBuildBundleCode + 1).ToString();
                else
                    bundleVersionCodeText = _currentBuildBundleCode.ToString();

                GUILayout.Label(bundleVersionCodeText, inputFieldStylesList);

                if (GUILayout.Button("Edit", GUILayout.Width(38)))
                    _isEditingAppBundleVersion = !_isEditingAppBundleVersion;
            }
            else
            {
                GUILayout.Label("Current bundle code = ", fieldTitleStylesList);

                _currentBuildBundleCode = EditorGUILayout.IntField(_currentBuildBundleCode, inputFieldStylesList);

                if (GUILayout.Button("Set", GUILayout.Width(38)))
                {
                    _isEditingAppBundleVersion = !_isEditingAppBundleVersion;
                    SaveBuildProperties();
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
