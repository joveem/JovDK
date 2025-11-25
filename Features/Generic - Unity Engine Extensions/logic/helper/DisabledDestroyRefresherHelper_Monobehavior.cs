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
    [ExecuteInEditMode]
    public partial class DisabledDestroyRefresherHelper_Monobehavior : MonoBehaviour
    {
        void OnDestroy()
        {
            // DebugExtension.DevLog();

#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                UnityEditor.EditorApplication.delayCall += () => DisabledMonoBehaviourTriggersExtensionService.RefreshAllLists();
#endif
        }
    }
}
