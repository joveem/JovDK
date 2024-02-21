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
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Generic.Shaders
{
    public partial class MaskContent : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] List<GameObject> _containersToMaskList = new List<GameObject>();


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        // void Awake()
        // {

        // }

        void Start()
        {
            for (int i = 0; i < _containersToMaskList.Count; i++)
            {
                GameObject containerToMask = _containersToMaskList[i];
                // containerToMask.DoIfNotNull(() => containerToMask.GetComponent<MeshRenderer>().material.renderQueue = 3002);
                HandleObjectMask(containerToMask);
            }
        }

        void HandleObjectMask(GameObject objectsToMask)
        {
            objectsToMask.DoIfNotNull(() =>
            {
                MeshRenderer[] renderersList = objectsToMask.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer renderer in renderersList)
                {
                    foreach (Material material in renderer.materials)
                        material.renderQueue = 3002;
                }
            });

        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
