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
        void FileConfigViewSection(
            GUIStyle titleLabelStyle,
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            GUILayout.Label("File", titleLabelStyle);
            GUILayout.Space(10);

            BuildFileAppNameField(fieldTitleStylesList, inputFieldStylesList);
            BuildFileVersionField(fieldTitleStylesList, inputFieldStylesList);
        }

        void BuildFileAppNameField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("App name = ", fieldTitleStylesList);

            if (!_isEditingAppName)
            {
                GUILayout.Label(_fileAppName, inputFieldStylesList);

                if (GUILayout.Button("Edit", GUILayout.Width(38)))
                    _isEditingAppName = !_isEditingAppName;
            }
            else
            {
                _fileAppName = GUILayout.TextField(_fileAppName, inputFieldStylesList);

                if (GUILayout.Button("Set", GUILayout.Width(38)))
                {
                    _isEditingAppName = !_isEditingAppName;
                    SaveBuildProperties();
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        void BuildFileVersionField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Version = ", fieldTitleStylesList);

            if (!_isEditingBuildFileVersion)
            {
                string versionText = "v" + _fileVersion.ToString();
                GUILayout.Label(versionText, inputFieldStylesList);

                if (GUILayout.Button("Edit", GUILayout.Width(38)))
                    _isEditingBuildFileVersion = !_isEditingBuildFileVersion;
            }
            else
            {
                GUILayout.Label("v", GUILayout.Width(10));

                int fileMajorVersion = EditorGUILayout.IntField(_fileVersion.Major, inputFieldStylesList);
                GUILayout.Label(".", GUILayout.Width(6));
                int fileMinorVersion = EditorGUILayout.IntField(_fileVersion.Minor, inputFieldStylesList);

                _fileVersion = new Version(fileMajorVersion, fileMinorVersion);

                if (GUILayout.Button("Set", GUILayout.Width(38)))
                {
                    _isEditingBuildFileVersion = !_isEditingBuildFileVersion;
                    SaveBuildProperties();
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
