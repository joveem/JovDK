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
        void PcConfigViewSection(
            GUIStyle titleLabelStyle,
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            GUILayout.Label("PC", titleLabelStyle);

            GUIContent guiContent = new GUIContent("Build PC");
            _hasToBuildPc = EditorGUILayout.BeginToggleGroup(guiContent, _hasToBuildPc);

            // AndroidApkField(fieldTitleStylesList, inputFieldStylesList);
            // AndroidKeystorePasswordField(fieldTitleStylesList, inputFieldStylesList);
            // AndroidKeystoreAliasPasswordField(fieldTitleStylesList, inputFieldStylesList);
            PcButtonField(fieldTitleStylesList, inputFieldStylesList);

            EditorGUILayout.EndToggleGroup();
        }

        // void AndroidApkField(
        //     GUILayoutOption[] fieldTitleStylesList,
        //     GUILayoutOption[] inputFieldStylesList)
        // {
        //     _buildApkInsteadOfAab = GUILayout.Toggle(_buildApkInsteadOfAab, "Build APK", fieldTitleStylesList);
        // }

        void PcButtonField(
            GUILayoutOption[] fieldTitleStylesList,
            GUILayoutOption[] inputFieldStylesList)
        {
            if (GUILayout.Button("Build PC" + (_isDevelopmentBuild ? _developmentBuildWarning : "")))
            {
                BuildPcButton();
                // SaveBuildProperties();
            }
        }

        // void AndroidKeystorePasswordField(
        //     GUILayoutOption[] fieldTitleStylesList,
        //     GUILayoutOption[] inputFieldStylesList)
        // {
        //     EditorGUILayout.BeginHorizontal();

        //     GUILayout.Label("Keystore password = ", fieldTitleStylesList);

        //     if (!_isEditingAndroidKeysorePassword)
        //     {
        //         GUILayout.Label("****", inputFieldStylesList);

        //         if (GUILayout.Button("Edit", GUILayout.Width(38)))
        //             _isEditingAndroidKeysorePassword = !_isEditingAndroidKeysorePassword;
        //     }
        //     else
        //     {
        //         _keystorePassword = GUILayout.PasswordField(_keystorePassword, '*', inputFieldStylesList);

        //         if (GUILayout.Button("Set", GUILayout.Width(38)))
        //         {
        //             _isEditingAndroidKeysorePassword = !_isEditingAndroidKeysorePassword;
        //             SaveBuildProperties();
        //         }
        //     }

        //     EditorGUILayout.EndHorizontal();
        // }

        // void AndroidKeystoreAliasPasswordField(
        //     GUILayoutOption[] fieldTitleStylesList,
        //     GUILayoutOption[] inputFieldStylesList)
        // {
        //     EditorGUILayout.BeginHorizontal();

        //     GUILayout.Label("Keystore Alias password = ", fieldTitleStylesList);

        //     if (!_isEditingAndroidKeysoreAliasPassword)
        //     {
        //         GUILayout.Label("****", inputFieldStylesList);

        //         if (GUILayout.Button("Edit", GUILayout.Width(38)))
        //             _isEditingAndroidKeysoreAliasPassword = !_isEditingAndroidKeysoreAliasPassword;
        //     }
        //     else
        //     {
        //         _keystoreAliasPassword = GUILayout.PasswordField(_keystoreAliasPassword, '*', inputFieldStylesList);

        //         if (GUILayout.Button("Set", GUILayout.Width(38)))
        //         {
        //             _isEditingAndroidKeysoreAliasPassword = !_isEditingAndroidKeysoreAliasPassword;
        //             SaveBuildProperties();
        //         }
        //     }

        //     EditorGUILayout.EndHorizontal();
        // }
    }
}
