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
        const string MenuItemPath = "JovDK/Tools/Localization/Populate MultiLanguage Texts";
        const string MenuItemPathAllowInstances = "JovDK/Tools/Localization/Populate MultiLanguage Texts (Allow Prefab Instances)";
        const string MenuItemPathValidateDuplicates = "JovDK/Tools/Localization/Validate Duplicated Keys";
        const string MenuItemPathValidateMissingKeys = "JovDK/Tools/Localization/Validate Missing Keys Across Languages";
        const string TermsAssetPath = "Assets/_Game/Features/Localization - Terms/Resources/localization-terms-content-by-language-id/pt-br/localization-terms.txt";
        const string TermsDirectoryPath = "Assets/_Game/Features/Localization - Terms/Resources/localization-terms-content-by-language-id";

        [MenuItem(MenuItemPath)]
        static void PopulateLocalizationData()
        {
            RunPopulationRoutine(allowPrefabInstancesInScene: false, appendNewTermsOnly: false);
        }

        [MenuItem(MenuItemPathAllowInstances)]
        static void PopulateLocalizationDataAllowingInstances()
        {
            RunPopulationRoutine(allowPrefabInstancesInScene: true, appendNewTermsOnly: true);
        }

        [MenuItem(MenuItemPathValidateDuplicates)]
        static void ValidateDuplicatedKeys()
        {
            Dictionary<string, List<string>> duplicatesByFile = CollectDuplicatedKeysByFile();

            if (duplicatesByFile.Count == 0)
            {
                Debug.Log("SceneLocalizationScrapperHelper: no duplicated localization keys were found.");
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SceneLocalizationScrapperHelper: duplicated localization keys found:");

            foreach (KeyValuePair<string, List<string>> entry in duplicatesByFile)
            {
                string keysText = string.Join(", ", entry.Value.Distinct(StringComparer.Ordinal));
                builder.AppendLine($"{entry.Key} ({keysText})");
            }

            Debug.LogWarning(builder.ToString());
        }

        [MenuItem(MenuItemPathValidateMissingKeys)]
        static void ValidateMissingKeysAcrossFiles()
        {
            CompareLocalizationKeysAcrossFiles();
        }

        static void RunPopulationRoutine(bool allowPrefabInstancesInScene, bool appendNewTermsOnly)
        {
            Scene activeScene = SceneManager.GetActiveScene();

            if (!activeScene.IsValid())
            {
                Debug.LogWarning("SceneLocalizationScrapperHelper: No valid scene is loaded.");
                return;
            }

            Dictionary<string, string> newTerms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            int sceneUpdates = ProcessScene(activeScene, newTerms, allowPrefabInstancesInScene);
            int prefabUpdates = ProcessPrefabs(newTerms);

            if (newTerms.Count > 0)
                MergeTermsIntoFile(newTerms, appendNewTermsOnly);

            StringBuilder summary = new StringBuilder();
            summary.Append("SceneLocalizationScrapperHelper finished. ");
            summary.Append($"Scene updates: {sceneUpdates}. ");
            summary.Append($"Prefab updates: {prefabUpdates}. ");
            summary.Append($"Terms written: {newTerms.Count}. ");
            summary.Append($"Terms file: {TermsAssetPath}");

            Debug.Log(summary.ToString());
        }

        static int ProcessScene(Scene scene, Dictionary<string, string> gatheredTerms, bool allowPrefabInstances)
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

                    if (!allowPrefabInstances && IsPartOfPrefabInstance(go))
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

        static void MergeTermsIntoFile(Dictionary<string, string> newTerms, bool appendOnly)
        {
            Dictionary<string, string> existingTerms = LoadExistingTerms();

            if (appendOnly)
                AppendTerms(existingTerms, newTerms);
            else
            {
                foreach (KeyValuePair<string, string> entry in newTerms)
                    existingTerms[entry.Key] = entry.Value;

                WriteTerms(existingTerms);
            }
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

        static void AppendTerms(Dictionary<string, string> existingTerms, Dictionary<string, string> newTerms)
        {
            string absolutePath = GetAbsoluteTermsPath();
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));

            HashSet<string> existingKeys = new HashSet<string>(existingTerms.Keys, StringComparer.OrdinalIgnoreCase);

            StringBuilder builder = new StringBuilder();
            bool fileExists = File.Exists(absolutePath);

            foreach (KeyValuePair<string, string> entry in newTerms)
            {
                if (!existingKeys.Contains(entry.Key))
                    existingKeys.Add(entry.Key);
                else
                    continue;

                if (builder.Length > 0 || fileExists)
                    builder.Append('\n');

                builder.Append(entry.Key);
                builder.Append('=');
                builder.Append(entry.Value ?? string.Empty);

                fileExists = true;
            }

            if (builder.Length > 0)
            {
                File.AppendAllText(absolutePath, builder.ToString(), Encoding.UTF8);
                AssetDatabase.ImportAsset(TermsAssetPath);
            }
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
            string projectRoot = GetProjectRootPath();

            return Path.GetFullPath(Path.Combine(projectRoot ?? string.Empty, TermsAssetPath));
        }

        static string GetProjectRootPath()
        {
            return Path.GetDirectoryName(Application.dataPath);
        }

        static string GetAbsoluteTermsDirectoryPath()
        {
            string projectRoot = GetProjectRootPath();

            return Path.GetFullPath(Path.Combine(projectRoot ?? string.Empty, TermsDirectoryPath));
        }

        static Dictionary<string, List<string>> CollectDuplicatedKeysByFile()
        {
            Dictionary<string, List<string>> duplicatesByFile = new Dictionary<string, List<string>>();
            string directoryAbsolutePath = GetAbsoluteTermsDirectoryPath();

            if (!Directory.Exists(directoryAbsolutePath))
            {
                Debug.LogWarning($"SceneLocalizationScrapperHelper: directory not found \"{directoryAbsolutePath}\".");
                return duplicatesByFile;
            }

            string[] termFiles = Directory.GetFiles(directoryAbsolutePath, "localization-terms.txt", SearchOption.AllDirectories);
            string projectRoot = GetProjectRootPath() ?? string.Empty;

            foreach (string filePath in termFiles)
            {
                Dictionary<string, int> occurrencesByKey = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                List<string> duplicatedKeys = new List<string>();
                HashSet<string> duplicatesLookup = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                string[] lines = File.ReadAllLines(filePath);

                foreach (string rawLine in lines)
                {
                    string line = rawLine.Trim();

                    if (string.IsNullOrEmpty(line) || line.StartsWith("#", StringComparison.Ordinal))
                        continue;

                    int separatorIndex = line.IndexOf('=');

                    if (separatorIndex < 0)
                        continue;

                    string key = line.Substring(0, separatorIndex).Trim();

                    if (string.IsNullOrEmpty(key))
                        continue;

                    if (!occurrencesByKey.TryAdd(key, 1))
                    {
                        occurrencesByKey[key]++;

                        if (duplicatesLookup.Add(key))
                            duplicatedKeys.Add(key);
                    }
                }

                if (duplicatedKeys.Count > 0)
                {
                    string relativePath = filePath;

                    if (!string.IsNullOrEmpty(projectRoot) &&
                        filePath.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase))
                    {
                        relativePath = filePath.Substring(projectRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    }

                    duplicatesByFile[relativePath] = duplicatedKeys;
                }
            }

            return duplicatesByFile;
        }

        static Dictionary<string, HashSet<string>> ReadLocalizationKeysByFile()
        {
            Dictionary<string, HashSet<string>> keysByFile = new Dictionary<string, HashSet<string>>();
            string directoryAbsolutePath = GetAbsoluteTermsDirectoryPath();

            if (!Directory.Exists(directoryAbsolutePath))
            {
                Debug.LogWarning($"SceneLocalizationScrapperHelper: directory not found \"{directoryAbsolutePath}\".");
                return keysByFile;
            }

            string[] termFiles = Directory.GetFiles(directoryAbsolutePath, "localization-terms.txt", SearchOption.AllDirectories);
            string projectRoot = GetProjectRootPath() ?? string.Empty;

            foreach (string filePath in termFiles)
            {
                HashSet<string> keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                string[] lines = File.ReadAllLines(filePath);

                foreach (string rawLine in lines)
                {
                    string line = rawLine.Trim();

                    if (string.IsNullOrEmpty(line) || line.StartsWith("#", StringComparison.Ordinal))
                        continue;

                    int separatorIndex = line.IndexOf('=');

                    if (separatorIndex < 0)
                        continue;

                    string key = line.Substring(0, separatorIndex).Trim();

                    if (string.IsNullOrEmpty(key))
                        continue;

                    keys.Add(key);
                }

                string relativePath = filePath;

                if (!string.IsNullOrEmpty(projectRoot) &&
                    filePath.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase))
                {
                    relativePath = filePath.Substring(projectRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }

                keysByFile[relativePath] = keys;
            }

            return keysByFile;
        }

        static void CompareLocalizationKeysAcrossFiles()
        {
            Dictionary<string, HashSet<string>> keysByFile = ReadLocalizationKeysByFile();
            int filesCount = keysByFile.Count;

            if (filesCount == 0)
            {
                Debug.LogWarning("SceneLocalizationScrapperHelper: no localization files were found.");
                return;
            }

            Dictionary<string, int> keyOccurrences = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (HashSet<string> keys in keysByFile.Values)
            {
                foreach (string key in keys)
                {
                    if (!keyOccurrences.TryAdd(key, 1))
                        keyOccurrences[key]++;
                }
            }

            List<string> incompleteKeys = keyOccurrences
                .Where(pair => pair.Value < filesCount)
                .Select(pair => pair.Key)
                .OrderBy(key => key, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (incompleteKeys.Count == 0)
            {
                Debug.Log("SceneLocalizationScrapperHelper: all localization files share the same set of keys.");
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SceneLocalizationScrapperHelper: missing localization keys detected.");
            builder.AppendLine("Keys not present in every file:");
            builder.AppendLine(string.Join("\n", incompleteKeys));
            builder.AppendLine();
            builder.AppendLine("Missing keys per file:");

            foreach (KeyValuePair<string, HashSet<string>> entry in keysByFile)
            {
                List<string> missingKeysForFile = incompleteKeys
                    .Where(key => !entry.Value.Contains(key))
                    .ToList();

                if (missingKeysForFile.Count == 0)
                    continue;

                builder.Append(entry.Key);
                builder.Append("\n");
                builder.AppendLine(string.Join("\n", missingKeysForFile));
            }

            Debug.LogWarning(builder.ToString());
        }
    }
}
