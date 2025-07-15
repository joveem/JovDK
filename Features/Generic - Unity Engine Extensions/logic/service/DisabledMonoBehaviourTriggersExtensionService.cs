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

        public DisabledMonoBehaviourTriggersExtensionService()
        {
            if (_instance == null)
                _instance = this;
        }



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
            foreach (var component in _disabledAwakeComponentesList)
                component.DoIfNotNull(() => component.DisabledAwake());
        }

        void Start()
        {
            foreach (var component in _disabledStartComponentesList)
                component.DoIfNotNull(() => component.DisabledStart());
        }
        #endregion MonoBehaviour

        #region Controller
        public static void ValidateInstance()
        {
            // DebugExtension.DevLog();

            if (_instance == null)
            {
                GameObject gameObjectInstance = new GameObject();

                _instance = gameObjectInstance.AddComponent<DisabledMonoBehaviourTriggersExtensionService>();
                gameObjectInstance.name = "disabled-monobehaviour-triggers-extension-service";

                Instantiate(gameObjectInstance);
            }
        }

        void ValidateSingleton()
        {
            // DebugExtension.DevLog();

#if UNITY_EDITOR
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

                UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(gameObject);
            }
#endif
        }

        public static void RefreshAllLists()
        {
            // DebugExtension.DevLog();

            ValidateInstance();

            _instance.DoIfNotNull(() =>
            {
                _instance._disabledAwakeComponentesList = GameObject.FindObjectsByType<DisabledAwake_Monobehavior>(FindObjectsInactive.Include, UnityEngine.FindObjectsSortMode.None).ToList();
                _instance._disabledStartComponentesList = GameObject.FindObjectsByType<DisabledStart_Monobehavior>(FindObjectsInactive.Include, UnityEngine.FindObjectsSortMode.None).ToList();
            });
        }

        public void TryToRegisterDisabledAwakeComponent(DisabledAwake_Monobehavior component)
        {
            bool isAlreadyRegistered = _disabledAwakeComponentesList.Contains(component);

            if (!isAlreadyRegistered)
                _disabledAwakeComponentesList.Add(component);
        }

        public void TryToRegisterDisabledStartComponent(DisabledStart_Monobehavior component)
        {
            bool isAlreadyRegistered = _disabledStartComponentesList.Contains(component);

            if (!isAlreadyRegistered)
                _disabledStartComponentesList.Add(component);
        }
        #endregion Controller
    }
}
