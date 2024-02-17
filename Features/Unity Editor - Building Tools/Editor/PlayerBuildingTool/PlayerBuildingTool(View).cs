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
        private void OnGUI()
        {
            GUIStyle titleLabelStyle = new GUIStyle(GUI.skin.label);
            titleLabelStyle.fontStyle = FontStyle.Bold;
            titleLabelStyle.fontSize = 18;
            titleLabelStyle.padding.bottom = 6;

            GUILayoutOption[] fieldTitleStylesList =
                new GUILayoutOption[]{
                        GUILayout.Width(40),
                        GUILayout.ExpandWidth(true),
                };

            GUILayoutOption[] inputFieldStylesList =
                new GUILayoutOption[]{
                        GUILayout.Width(20),
                        GUILayout.ExpandWidth(true),
                };

            GUILayout.Space(15);

            FileConfigViewSection(titleLabelStyle, fieldTitleStylesList, inputFieldStylesList);
            DrawUILine();
            AppConfigViewSection(titleLabelStyle, fieldTitleStylesList, inputFieldStylesList);
            DrawUILine();
            AndroidConfigViewSection(titleLabelStyle, fieldTitleStylesList, inputFieldStylesList);
            DrawUILine();
            BuildViewSection(titleLabelStyle, fieldTitleStylesList, inputFieldStylesList);
        }

        public static void DrawUILine(Color color = default, int thickness = 1, int padding = 10)
        {
            if (color.Equals(default))
                color = Color.grey;

            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }
    }
}
