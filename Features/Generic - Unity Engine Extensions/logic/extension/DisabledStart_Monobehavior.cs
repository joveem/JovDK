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

// from project
// ...


namespace JovDK.Generic.UnityEngineExtensions
{
    public abstract partial class DisabledStart_Monobehavior : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]
        DisabledDestroyRefresherHelper_Monobehavior _disabledDestroyRefresherHelper_Monobehavior = null;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // [SerializeField] bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] bool _configs;



        #region MonoBehaviour
        void Reset()
        {
            HandleRefresherHelperValidation();
            HandleListValidation();
        }

        void OnValidate()
        {
            HandleListValidation();
        }

        /// <summary>
        /// An additional "Awake" function that triggers even if the Monobehavior is disabled!
        /// </summary>
        public virtual void DisabledStart() { }
        #endregion MonoBehaviour

        #region Controller
        void HandleRefresherHelperValidation()
        {
            if (_disabledDestroyRefresherHelper_Monobehavior == null)
            {
                DisabledDestroyRefresherHelper_Monobehavior[] refresherHelpersList = gameObject.GetComponents<DisabledDestroyRefresherHelper_Monobehavior>();

                if (refresherHelpersList.Length > 0)
                    _disabledDestroyRefresherHelper_Monobehavior = refresherHelpersList[0];

                for (int i = 1; i < refresherHelpersList.Length; i++)
                {
                    DisabledDestroyRefresherHelper_Monobehavior refresherHelper = refresherHelpersList[i];
                    Destroy(refresherHelper);
                }
            }

            if (_disabledDestroyRefresherHelper_Monobehavior == null)
                _disabledDestroyRefresherHelper_Monobehavior = gameObject.AddComponent<DisabledDestroyRefresherHelper_Monobehavior>();
        }

        void HandleListValidation()
        {
            DisabledMonoBehaviourTriggersExtensionService.RefreshAllLists();
        }
        #endregion Controller
    }
}
