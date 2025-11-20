// system / unity
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JovDK.LEGACY.Localization.Editor
{
    /// <summary>
    /// Editor helper that scans the active scene and every prefab looking for TextMeshProUGUI
    /// components, adding MultiLanguageText + generating localization keys and entries.
    /// </summary>
    internal static class SceneLocalizationScrapperHelper
    {
        const string MenuItemPath = "Tools/JovDK/Localization/Populate MultiLanguage Texts";
        const string TermsAssetPath = "Assets/_Game/Features/Localization - Terms/Resources/localization-terms-content-by-language-id/pt-br/localization-terms.txt";

        [MenuItem(MenuItemPath)]
        static void PopulateLocalizationData()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            if (!activeScene.IsValid())
            {
                Debug.LogWarning("SceneLocalizationScrapperHelper: No valid scene is loaded.");
                return;
            }

            Dictionary<string, string> newTerms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            int sceneUpdates = ProcessScene(activeScene, newTerms);
            int prefabUpdates = ProcessPrefabs(newTerms);

            if (newTerms.Count > 0)
                MergeTermsIntoFile(newTerms);

            StringBuilder summary = new StringBuilder();
            summary.Append("SceneLocalizationScrapperHelper finished. ");
            summary.Append($"Scene updates: {sceneUpdates}. ");
            summary.Append($"Prefab updates: {prefabUpdates}. ");
            summary.Append($"Terms written: {newTerms.Count}. ");
            summary.Append($"Terms file: {TermsAssetPath}");

            Debug.Log(summary.ToString());
        }

        static int ProcessScene(Scene scene, Dictionary<string, string> gatheredTerms)
        {
            int updates = 0;
            string scenePrefix = Sanitize(scene.name);

            if (string.IsNullOrEmpty(scenePrefix))
                scenePrefix = "untitled-scene";

            foreach (GameObject rootObject in scene.GetRootGameObjects())
            {
                TextMeshProUGUI[] texts = rootObject.GetComponentsInChildren<TextMeshProUGUI>(true);

                foreach (TextMeshProUGUI textComponent in texts)
                {
                    GameObject go = textComponent.gameObject;

                    if (IsPartOfPrefabInstance(go))
                        continue;

                    if (textComponent.GetComponent<MultiLanguageText>() != null)
                        continue;

                    string termKey = BuildTermKey(scenePrefix, textComponent.transform);

                    if (AssignLocalizationComponent(textComponent, termKey, registerUndo: true))
                    {
                        string normalizedContent = NormalizeTextContent(textComponent.text);
                        gatheredTerms[termKey] = normalizedContent;
                        updates++;
                    }
                }
            }

            if (updates > 0)
                EditorSceneManager.MarkSceneDirty(scene);

            return updates;
        }

        static int ProcessPrefabs(Dictionary<string, string> gatheredTerms)
        {
            int totalUpdates = 0;
            string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });

            foreach (string guid in prefabGuids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                if (string.IsNullOrEmpty(assetPath))
                    continue;

                GameObject prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
                bool prefabChanged = false;
                string prefabPrefix = Sanitize(Path.GetFileNameWithoutExtension(assetPath));

                if (string.IsNullOrEmpty(prefabPrefix))
                    prefabPrefix = "prefab";

                TextMeshProUGUI[] textComponents = prefabRoot.GetComponentsInChildren<TextMeshProUGUI>(true);

                foreach (TextMeshProUGUI textComponent in textComponents)
                {
                    GameObject go = textComponent.gameObject;

                    if (PrefabUtility.IsPartOfPrefabInstance(go))
                        continue;

                    if (textComponent.GetComponent<MultiLanguageText>() != null)
                        continue;

                    string termKey = BuildTermKey(prefabPrefix, textComponent.transform);

                    if (AssignLocalizationComponent(textComponent, termKey, registerUndo: false))
                    {
                        string normalizedContent = NormalizeTextContent(textComponent.text);
                        gatheredTerms[termKey] = normalizedContent;
                        prefabChanged = true;
                        totalUpdates++;
                    }
                }

                if (prefabChanged)
                    PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);

                PrefabUtility.UnloadPrefabContents(prefabRoot);
            }

            return totalUpdates;
        }

        static bool AssignLocalizationComponent(TextMeshProUGUI textComponent, string termKey, bool registerUndo)
        {
            if (string.IsNullOrEmpty(termKey))
                return false;

            MultiLanguageText multiLanguageText = registerUndo
                ? Undo.AddComponent<MultiLanguageText>(textComponent.gameObject)
                : textComponent.gameObject.AddComponent<MultiLanguageText>();

            SerializedObject serializedObject = new SerializedObject(multiLanguageText);
            serializedObject.FindProperty("_baseTextMeshProUGUI").objectReferenceValue = textComponent;
            serializedObject.FindProperty("_termKey").stringValue = termKey;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            EditorUtility.SetDirty(multiLanguageText);
            EditorUtility.SetDirty(textComponent);

            return true;
        }

        static string BuildTermKey(string prefix, Transform targetTransform)
        {
            List<string> segments = new List<string>();
            Transform current = targetTransform;

            while (current != null)
            {
                segments.Add(Sanitize(current.name));
                current = current.parent;
            }

            segments.Reverse();

            if (segments.Count == 0)
                segments.Add("unnamed");

            StringBuilder keyBuilder = new StringBuilder(prefix);

            foreach (string segment in segments)
            {
                keyBuilder.Append('.');
                keyBuilder.Append(string.IsNullOrEmpty(segment) ? "unnamed" : segment);
            }

            return keyBuilder.ToString();
        }

        static bool IsPartOfPrefabInstance(GameObject go)
        {
            PrefabInstanceStatus status = PrefabUtility.GetPrefabInstanceStatus(go);

            return status == PrefabInstanceStatus.Connected ||
                   status == PrefabInstanceStatus.Disconnected ||
                   status == PrefabInstanceStatus.MissingAsset;
        }

        static string Sanitize(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return "unnamed";

            StringBuilder builder = new StringBuilder(raw.Length);

            foreach (char c in raw.Trim())
            {
                if (char.IsLetterOrDigit(c))
                {
                    builder.Append(char.ToLowerInvariant(c));
                }
                else if (c == '-' || c == '_')
                {
                    builder.Append(char.ToLowerInvariant(c));
                }
                else if (char.IsWhiteSpace(c) || c == '.' || c == '/')
                {
                    builder.Append('-');
                }
                else
                {
                    builder.Append('-');
                }
            }

            string sanitized = builder.ToString();

            while (sanitized.Contains("--"))
                sanitized = sanitized.Replace("--", "-");

            sanitized = sanitized.Trim('-');

            return string.IsNullOrEmpty(sanitized) ? "unnamed" : sanitized;
        }

        static string NormalizeTextContent(string rawText)
        {
            if (string.IsNullOrEmpty(rawText))
                return string.Empty;

            string normalized = rawText.Replace("\r\n", "\n")
                                       .Replace("\r", "\n")
                                       .Replace("\n", "<br>");

            return normalized.Trim();
        }

        static void MergeTermsIntoFile(Dictionary<string, string> newTerms)
        {
            Dictionary<string, string> existingTerms = LoadExistingTerms();

            foreach (KeyValuePair<string, string> entry in newTerms)
                existingTerms[entry.Key] = entry.Value;

            WriteTerms(existingTerms);
        }

        static Dictionary<string, string> LoadExistingTerms()
        {
            Dictionary<string, string> terms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string absolutePath = GetAbsoluteTermsPath();

            if (!File.Exists(absolutePath))
                return terms;

            string[] lines = File.ReadAllLines(absolutePath);

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                if (string.IsNullOrEmpty(line) || line.StartsWith("#", StringComparison.Ordinal))
                    continue;

                int separatorIndex = line.IndexOf('=');

                if (separatorIndex < 0)
                    continue;

                string key = line.Substring(0, separatorIndex).Trim();
                string value = line.Substring(separatorIndex + 1);

                if (string.IsNullOrEmpty(key))
                    continue;

                terms[key] = value;
            }

            return terms;
        }

        static void WriteTerms(Dictionary<string, string> terms)
        {
            string absolutePath = GetAbsoluteTermsPath();
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));

            List<string> orderedKeys = terms.Keys.OrderBy(key => key, StringComparer.Ordinal).ToList();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < orderedKeys.Count; i++)
            {
                string key = orderedKeys[i];
                string value = terms[key] ?? string.Empty;

                builder.Append(key);
                builder.Append('=');
                builder.Append(value);

                if (i < orderedKeys.Count - 1)
                    builder.Append('\n');
            }

            File.WriteAllText(absolutePath, builder.ToString(), Encoding.UTF8);
            AssetDatabase.ImportAsset(TermsAssetPath);
        }

        static string GetAbsoluteTermsPath()
        {
            string projectRoot = Path.GetDirectoryName(Application.dataPath);

            return Path.GetFullPath(Path.Combine(projectRoot ?? string.Empty, TermsAssetPath));
        }
    }
}
