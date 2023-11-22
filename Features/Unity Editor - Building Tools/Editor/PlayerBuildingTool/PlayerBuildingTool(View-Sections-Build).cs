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
        void BuildViewSection(
            GUIStyle titleLabelStyle,
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            GUILayout.Label("Build", titleLabelStyle);

            DevelopmentBuildField(fieldTitleStylesList, inputFieldStylesList);
            BuildButtonViewSection(fieldTitleStylesList, inputFieldStylesList);
            OpenFolderViewSection(fieldTitleStylesList, inputFieldStylesList);
        }

        void DevelopmentBuildField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            _isDevelopmentBuild = GUILayout.Toggle(_isDevelopmentBuild, "Development Build", fieldTitleStylesList);
        }

        void BuildButtonViewSection(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            GUIStyle _openFolderButtonStyle = new GUIStyle(GUI.skin.button);
            _openFolderButtonStyle.fixedHeight = 32;

            GUIStyle _buildForAllButtonStyle = new GUIStyle(GUI.skin.button);
            _buildForAllButtonStyle.fontSize = 15;
            _buildForAllButtonStyle.fixedHeight = 46;
            _buildForAllButtonStyle.fontStyle = FontStyle.Bold;

            _buildForAllButtonStyle = HandleBuildButtonColor(_buildForAllButtonStyle);
            string platformsListText = GetSelectedPlatformsListText();

            if (GUILayout.Button("[ BUILD " + platformsListText + " ]" + (_isDevelopmentBuild ? _developmentBuildWarning : ""), _buildForAllButtonStyle))
            {
                BuildAllSelectedPlatforms();
                // SaveBuildProperties();
            }
        }

        string GetSelectedPlatformsListText()
        {
            string value = "NONE";

            if (_hasToBuildAndroid)
                value = "ANDROID";

            return value;
        }

        GUIStyle HandleBuildButtonColor(GUIStyle baseStyle)
        {
            if (!_hasToBuildAndroid)
                baseStyle.normal.textColor = Color.red;

            return baseStyle;
        }

        void OpenFolderViewSection(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            GUIStyle _openFolderButtonStyle = new GUIStyle(GUI.skin.button);
            _openFolderButtonStyle.fixedHeight = 32;

            if (GUILayout.Button("Open folder", _openFolderButtonStyle))
                OpenBuildFolderButton();
        }
    }
}
