// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;
using System.Linq;
using UnityEngine.SceneManagement;

// from project
// ...


namespace JovDK.Generic.UnityEngineExtensions
{
    public partial class DisabledMonoBehaviourTriggersExtensionService : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        static DisabledMonoBehaviourTriggersExtensionService _instance = null;
        [SerializeField] List<DisabledAwake_Monobehavior> _disabledAwakeComponentesList = new List<DisabledAwake_Monobehavior>();
        [SerializeField] List<DisabledStart_Monobehavior> _disabledStartComponentesList = new List<DisabledStart_Monobehavior>();


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // [SerializeField] bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;

        #region MonoBehaviour
        void Reset()
        {
            ValidateSingleton();
        }

        void OnValidate()
        {
            ValidateSingleton();
        }

        void Awake()
        {
            if (!TryClaimSingleton())
                return;

            RefreshAllLists();

            foreach (var component in _disabledAwakeComponentesList)
            {
                try
                {
                    component.DoIfNotNull(() => component.DisabledAwake());
                }
                catch (Exception exception)
                {
                    DebugExtension.DevLogError(
                        "$$> ".ToColor(GoodColors.Red),
                        "exception = ", "\n",
                        exception.ToString(), "\n",
                        "");

                    // throw;
                }
            }
        }

        void Start()
        {
            foreach (var component in _disabledStartComponentesList)
            {
                try
                {
                    component.DoIfNotNull(() => component.DisabledStart());
                }
                catch (Exception exception)
                {
                    DebugExtension.DevLogError(
                        "$$> ".ToColor(GoodColors.Red),
                        "exception = ", "\n",
                        exception.ToString(), "\n",
                        "");

                    // throw;
                }
            }
        }

        void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
        #endregion MonoBehaviour

        #region Controller
        // TODO: REVIEW THIS! (9ghn0208g2e)
        // public static void ValidateInstance()
        // {
        //     // if (_instance == null)
        //     //     _instance = FindFirstObjectByType<DisabledMonoBehaviourTriggersExtensionService>(FindObjectsInactive.Include);
        // }

        void ValidateSingleton()
        {
            // DebugExtension.DevLog();

#if UNITY_EDITOR
            if (_instance == null)
                _instance = this;

            if (_instance == this)
                RefreshAllLists();
            else
            {
                DebugExtension.DevLogWarning(
                    "$> ".ToColor(GoodColors.Red),
                    "Duplicated instance!", "\n",
                    "");

                Debug.LogWarning(
                    "gameObject.name = " + gameObject.name.SerializeObjectToJSON() + "\n" +
                    "", gameObject);
            }
#endif
        }

        bool TryClaimSingleton()
        {
            if (_instance == null)
            {
                _instance = this;
                return true;
            }

            if (_instance == this)
                return true;

            Debug.LogWarning("Duplicated DisabledMonoBehaviourTriggersExtensionService instance was disabled.", this);
            enabled = false;
            return false;
        }

        public static void RefreshAllLists()
        {
            // TODO: REVIEW THIS! (9ghn0208g2e)
            // ValidateInstance();

            DisabledMonoBehaviourTriggersExtensionService instance = _instance;
            if (instance == null)
                return;

            Scene targetScene = instance.gameObject.scene;
            List<DisabledAwake_Monobehavior> awakeComponents = FindStableSceneComponents<DisabledAwake_Monobehavior>(targetScene);
            List<DisabledStart_Monobehavior> startComponents = FindStableSceneComponents<DisabledStart_Monobehavior>(targetScene);

            if (instance == null)
                return;

            if (RequiresSerializedListUpdate(instance._disabledAwakeComponentesList, awakeComponents))
                instance._disabledAwakeComponentesList = awakeComponents;

            if (RequiresSerializedListUpdate(instance._disabledStartComponentesList, startComponents))
                instance._disabledStartComponentesList = startComponents;
        }

        static List<T> FindStableSceneComponents<T>(Scene targetScene) where T : Component
        {
            T[] discoveredComponents = GameObject.FindObjectsByType<T>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);
            var uniqueComponents = new HashSet<T>();

            for (int index = 0; index < discoveredComponents.Length; index++)
            {
                T component = discoveredComponents[index];
                GameObject componentGameObject = component != null ? component.gameObject : null;
                if (componentGameObject != null && componentGameObject.scene == targetScene)
                    uniqueComponents.Add(component);
            }

            return uniqueComponents
                .OrderBy(GetStableComponentKey, StringComparer.Ordinal)
                .ToList();
        }

        static string GetStableComponentKey(Component component)
        {
            string hierarchyKey = component.gameObject.scene.path;
            var siblingIndexes = new Stack<int>();
            Transform currentTransform = component.transform;

            while (currentTransform != null)
            {
                siblingIndexes.Push(currentTransform.GetSiblingIndex());
                currentTransform = currentTransform.parent;
            }

            while (siblingIndexes.Count > 0)
                hierarchyKey += "/" + siblingIndexes.Pop().ToString("D8");

            Component[] sameTypeComponents = component.GetComponents(component.GetType());
            int componentIndex = Array.IndexOf(sameTypeComponents, component);
            return hierarchyKey + "|" + component.GetType().FullName + "|" + componentIndex.ToString("D4");
        }

        public static bool RequiresSerializedListUpdate<T>(
            IReadOnlyList<T> currentComponents,
            IReadOnlyList<T> discoveredComponents)
            where T : UnityEngine.Object
        {
            if (currentComponents == null || discoveredComponents == null ||
                currentComponents.Count != discoveredComponents.Count)
            {
                return true;
            }

            var currentSet = new HashSet<T>();
            for (int index = 0; index < currentComponents.Count; index++)
            {
                T component = currentComponents[index];
                if (component == null || !currentSet.Add(component))
                    return true;
            }

            for (int index = 0; index < discoveredComponents.Count; index++)
            {
                T component = discoveredComponents[index];
                if (component == null || !currentSet.Contains(component))
                    return true;
            }

            return false;
        }

        public void TryToRegisterDisabledAwakeComponent(DisabledAwake_Monobehavior component)
        {
            RefreshAllLists();
        }

        public void TryToRegisterDisabledStartComponent(DisabledStart_Monobehavior component)
        {
            RefreshAllLists();
        }
        #endregion Controller
    }
}
