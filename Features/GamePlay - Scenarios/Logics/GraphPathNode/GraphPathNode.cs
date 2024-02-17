// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

// from project
// ...


public partial class GraphPathNode : MonoBehaviour
{

    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    // bool _dependencies;


    // [Space(5), Header("[ State ]"), Space(10)]

    // bool _state;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] List<GraphPathNode> _neighborsNodesList = new List<GraphPathNode>();


    [Space(5), Header("[ Configs ]"), Space(10)]

    [SerializeField] float _areaSize = 10f;
    public float AreaSize { get { return _areaSize; } }


    // void Awake()
    // {

    // }

    // void Start()
    // {

    // }

    // void Update()
    // {

    // }

    // void FixedUpdate()
    // {

    // }


    void OnDrawGizmos()
    {
        bool hasToDrawGizmos = GetHasToDrawGizmos();
        if (!hasToDrawGizmos) // TODO: REVIEW THIS!
            return;

        Vector3 centerPosition = transform.position;
        DrawNodePosition(centerPosition);
        DrawNeighborsNodes(centerPosition);
    }

    static bool GetHasToDrawGizmos()
    {
        bool value = false;

#if UNITY_EDITOR
        string prefKey = GetProjectPrefKey("jovdk-graph-path-node-has-to-draw-gizmos");
        value = EditorPrefs.GetBool(prefKey, true);
#endif

        return value;
    }

    static void SetHasToDrawGizmos(bool value)
    {
#if UNITY_EDITOR
        string prefKey = GetProjectPrefKey("jovdk-graph-path-node-has-to-draw-gizmos");
        EditorPrefs.SetBool(prefKey, value);
#endif
    }

    static string GetProjectPrefKey(string baseKey)
    {
        // DebugExtension.DevLog("Application.identifier = " + Application.identifier);
        return Application.identifier + "-" + baseKey;
    }
}
