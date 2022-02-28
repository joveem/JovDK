using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JovDK.Debug;

public class MeshTools : MonoBehaviour
{
    static public void CopyMeshRenderer(GameObject _original, GameObject _destination)
    {

        if(_original.GetComponent<MeshRenderer>() != null && _destination.GetComponent<MeshRenderer>() != null){

            _destination.GetComponent<MeshRenderer>().sharedMaterials = _original.GetComponent<MeshRenderer>().sharedMaterials;
            _destination.GetComponent<MeshFilter>().sharedMesh = _original.GetComponent<MeshFilter>().sharedMesh;

        }else{

            DebugExtension.DevLogWarning(_original.GetComponent<MeshRenderer>().TextIfIsNull("_original IS NULL! | ") + _destination.GetComponent<MeshRenderer>().TextIfIsNull("_destination IS NULL! | ")); 

        }


    }

    static public void CopySkinnedMesh(GameObject _original, GameObject _destination)
    {

        if(_original.GetComponent<SkinnedMeshRenderer>() != null && _destination.GetComponent<SkinnedMeshRenderer>() != null){

            _destination.GetComponent<SkinnedMeshRenderer>().sharedMaterials = _original.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
            _destination.GetComponent<MeshFilter>().sharedMesh = _original.GetComponent<MeshFilter>().sharedMesh;

        }else{

            DebugExtension.DevLogWarning(_original.GetComponent<MeshRenderer>().TextIfIsNull("_original IS NULL! | ") + _destination.GetComponent<MeshRenderer>().TextIfIsNull("_destination IS NULL! | ")); 

        }


    }
}
