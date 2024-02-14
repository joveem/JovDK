// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


// third
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class GraphPathNode : MonoBehaviour
{

    [MenuItem("GameObject/>>> Graph Path Node/Switch Gizmos view", false, 9)]
    static void SwitchGizmosView(MenuCommand menuCommand)
    {
        if (Selection.objects.Length > 0)
        {
            if (menuCommand.context != Selection.objects[0])
                return;
        }

        bool hasToDrawGizmos = GetHasToDrawGizmos();
        hasToDrawGizmos = !hasToDrawGizmos;
        SetHasToDrawGizmos(hasToDrawGizmos);
    }

    [MenuItem("GameObject/>>> Graph Path Node/Create ONE Neighbors Nodes", false, 10)]
    static void CreateOneNeighborsNodes(MenuCommand menuCommand)
    {
        if (Selection.objects.Length > 0)
        {
            if (menuCommand.context != Selection.objects[0])
                return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;

        List<GraphPathNode> selectedPathNodesList = new List<GraphPathNode>();

        foreach (GameObject selectedGameObject in selectedObjects)
        {
            if (selectedGameObject != null)
            {
                bool isPathNode =
                    selectedGameObject.TryGetComponent<GraphPathNode>(
                        out GraphPathNode graphPathNode);

                if (isPathNode)
                    selectedPathNodesList.Add(graphPathNode);
            }
        }

        Transform instanceParent = null;
        Vector3 instancePosition = Vector3.zero;

        foreach (GraphPathNode selectedPathNode in selectedPathNodesList)
        {
            if (instanceParent == null)
            {
                instanceParent = selectedPathNode.transform.parent;
                instancePosition = selectedPathNode.transform.position;
            }
        }

        GameObject pathNodeInstanceGameObject = new GameObject();
        GraphPathNode pathNodeInstance = pathNodeInstanceGameObject.AddComponent<GraphPathNode>();

        pathNodeInstanceGameObject.name = "path-node";

        foreach (GraphPathNode selectedPathNode in selectedPathNodesList)
        {
            selectedPathNode._neighborsNodesList.Add(pathNodeInstance);
            pathNodeInstance._neighborsNodesList.Add(selectedPathNode);
        }

        pathNodeInstanceGameObject.transform.SetParent(instanceParent);
        pathNodeInstanceGameObject.transform.position = instancePosition;

        if (instanceParent != null)
        {
            int childIndex = instanceParent.childCount - 1;
            pathNodeInstanceGameObject.transform.SetSiblingIndex(childIndex);
        }

        Selection.objects = new UnityEngine.Object[] { pathNodeInstanceGameObject.gameObject };
    }

    [MenuItem("GameObject/>>> Graph Path Node/Create N Neighbors Nodes", false, 11)]
    static void CreateNNeighborsNodes(MenuCommand menuCommand)
    {
        if (Selection.objects.Length == 0)
        {
            DebugExtension.DevLogWarning(
                "$ > ".ToColor(GoodColors.Pink) +
                "ERROR trying to CreateNNeighborsNodes" + "\n" +
                "NO selected objects" + "\n" +
                "");
            return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;

        List<GraphPathNode> selectedPathNodesList = new List<GraphPathNode>();
        List<GraphPathNode> newPathNodes = new List<GraphPathNode>();

        foreach (GameObject selectedGameObject in selectedObjects)
        {
            if (selectedGameObject != null)
            {
                bool isPathNode =
                    selectedGameObject.TryGetComponent<GraphPathNode>(
                        out GraphPathNode graphPathNode);

                if (isPathNode)
                    selectedPathNodesList.Add(graphPathNode);
            }
        }


        foreach (GraphPathNode selectedPathNode in selectedPathNodesList)
        {
            Transform instanceParent = null;
            Vector3 instancePosition = Vector3.zero;

            if (instanceParent == null)
            {
                instanceParent = selectedPathNode.transform.parent;
                instancePosition = selectedPathNode.transform.position;
            }

            GameObject pathNodeInstanceGameObject = new GameObject();
            GraphPathNode pathNodeInstance = pathNodeInstanceGameObject.AddComponent<GraphPathNode>();

            pathNodeInstanceGameObject.name = "path-node";



            pathNodeInstanceGameObject.transform.SetParent(instanceParent);
            pathNodeInstanceGameObject.transform.position = instancePosition;

            if (instanceParent != null)
            {
                int childIndex = instanceParent.childCount - 1;
                pathNodeInstanceGameObject.transform.SetSiblingIndex(childIndex);
            }

            newPathNodes.Add(pathNodeInstance);
        }

        foreach (GraphPathNode newPathNode in newPathNodes)
        {
            foreach (GraphPathNode selectedPathNode in selectedPathNodesList)
            {
                selectedPathNode._neighborsNodesList.Add(newPathNode);
                newPathNode._neighborsNodesList.Add(selectedPathNode);
            }
        }

        foreach (GraphPathNode newPathNode in newPathNodes)
        {
            foreach (GraphPathNode newPathNodePair in newPathNodes)
            {
                if (newPathNode != newPathNodePair)
                {
                    newPathNode._neighborsNodesList.Add(newPathNodePair);
                    newPathNodePair._neighborsNodesList.Add(newPathNode);
                }
            }
        }

        List<UnityEngine.Object> newSelectionList = new List<UnityEngine.Object>();

        foreach (GraphPathNode newPathNode in newPathNodes)
        {
            newSelectionList.Add(newPathNode.gameObject);
        }

        Selection.objects = newSelectionList.ToArray();
    }

    [MenuItem("GameObject/>>> Graph Path Node/Create N Neighbors Nodes (Near)", false, 12)]
    static void CreateNNeighborsNodesNear(MenuCommand menuCommand)
    {
        if (Selection.objects.Length == 0)
        {
            DebugExtension.DevLogWarning(
                "$ > ".ToColor(GoodColors.Pink) +
                "ERROR trying to CreateNNeighborsNodes" + "\n" +
                "NO selected objects" + "\n" +
                "");
            return;
        }

        if (Selection.objects.Length > 0)
        {
            if (menuCommand.context != Selection.objects[0])
                return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;

        List<GraphPathNode> selectedPathNodesList = new List<GraphPathNode>();
        List<GraphPathNode> newPathNodes = new List<GraphPathNode>();

        foreach (GameObject selectedGameObject in selectedObjects)
        {
            if (selectedGameObject != null)
            {
                bool isPathNode =
                    selectedGameObject.TryGetComponent<GraphPathNode>(
                        out GraphPathNode graphPathNode);

                if (isPathNode)
                    selectedPathNodesList.Add(graphPathNode);
            }
        }


        foreach (GraphPathNode selectedPathNode in selectedPathNodesList)
        {
            Transform instanceParent = null;
            Vector3 instancePosition = Vector3.zero;

            if (instanceParent == null)
            {
                instanceParent = selectedPathNode.transform.parent;
                instancePosition = selectedPathNode.transform.position;
            }

            GameObject pathNodeInstanceGameObject = new GameObject();
            GraphPathNode pathNodeInstance = pathNodeInstanceGameObject.AddComponent<GraphPathNode>();

            pathNodeInstanceGameObject.name = "path-node";



            pathNodeInstanceGameObject.transform.SetParent(instanceParent);
            pathNodeInstanceGameObject.transform.position = instancePosition;

            if (instanceParent != null)
            {
                int childIndex = instanceParent.childCount - 1;
                pathNodeInstanceGameObject.transform.SetSiblingIndex(childIndex);
            }

            newPathNodes.Add(pathNodeInstance);

            selectedPathNode._neighborsNodesList.Add(pathNodeInstance);
            pathNodeInstance._neighborsNodesList.Add(selectedPathNode);
        }

        // Debug.Log("newPathNodes.Count = " + newPathNodes.Count);
        Dictionary<GraphPathNode, List<GraphPathNode>> newL2NeighborsByNodes = new Dictionary<GraphPathNode, List<GraphPathNode>>();


        for (int i = 0; i < newPathNodes.Count; i++)
        {
            GraphPathNode newPathNodeL0 = newPathNodes[i];
            List<GraphPathNode> newL2Neighbors = new List<GraphPathNode>();

            for (int j = 0; j < newPathNodeL0._neighborsNodesList.Count; j++)
            {
                GraphPathNode neighborsNodeL1 = newPathNodeL0._neighborsNodesList[j];

                if (neighborsNodeL1 != null && neighborsNodeL1 != newPathNodeL0)
                {
                    if (selectedPathNodesList.Contains(neighborsNodeL1))
                    {
                        for (int k = 0; k < neighborsNodeL1._neighborsNodesList.Count; k++)
                        {
                            GraphPathNode neighborsNodeL2 = neighborsNodeL1._neighborsNodesList[k];

                            // if (neighborsNodeL2 != null &&
                            //     neighborsNodeL2 != newPathNodeL0)
                            if (neighborsNodeL2 != null &&
                                neighborsNodeL2 != newPathNodeL0 &&
                                neighborsNodeL2 != neighborsNodeL1)
                            {
                                if (selectedPathNodesList.Contains(neighborsNodeL2))
                                    newL2Neighbors.Add(neighborsNodeL2);
                            }
                        }
                    }
                }
            }

            // Debug.Log("newL2Neighbors = " + newL2Neighbors.Count);

            newL2NeighborsByNodes.Add(newPathNodeL0, newL2Neighbors);

            // foreach (GraphPathNode itemL2 in newL2Neighbors)
            // {
            //     newPathNodeL0._neighborsNodesList.Add(itemL2);
            //     itemL2._neighborsNodesList.Add(newPathNodeL0);
            // }
        }


        // Debug.Log("01 - newPathNodes = " + newPathNodes.Count);
        for (int i = 0; i < newPathNodes.Count; i++)
        {
            GraphPathNode newPathNodeL0 = newPathNodes[i];
            List<GraphPathNode> newL3Neighbors = new List<GraphPathNode>();

            for (int j = 0; j < newPathNodeL0._neighborsNodesList.Count; j++)
            {
                GraphPathNode neighborsNodeL1 = newPathNodeL0._neighborsNodesList[j];

                if (neighborsNodeL1 != null && neighborsNodeL1 != newPathNodeL0)
                {
                    if (selectedPathNodesList.Contains(neighborsNodeL1))
                    {
                        // Debug.Log("neighborsNodeL1._neighborsNodesList = " + neighborsNodeL1._neighborsNodesList.Count);
                        for (int k = 0; k < neighborsNodeL1._neighborsNodesList.Count; k++)
                        {
                            GraphPathNode neighborsNodeL2 = neighborsNodeL1._neighborsNodesList[k];

                            if (neighborsNodeL2 != null &&
                                neighborsNodeL2 != newPathNodeL0 &&
                                neighborsNodeL2 != neighborsNodeL1)
                            {
                                if (selectedPathNodesList.Contains(neighborsNodeL2))
                                {
                                    // Debug.Log("01-02 - neighborsNodeL2._neighborsNodesList = " + neighborsNodeL2._neighborsNodesList.Count);
                                    for (int l = 0; l < neighborsNodeL2._neighborsNodesList.Count; l++)
                                    {
                                        GraphPathNode neighborsNodeL3 = neighborsNodeL2._neighborsNodesList[l];

                                        if (neighborsNodeL3 != null &&
                                            neighborsNodeL3 != newPathNodeL0 &&
                                            neighborsNodeL3 != neighborsNodeL1 &&
                                            neighborsNodeL3 != neighborsNodeL2)
                                        {
                                            if (newPathNodes.Contains(neighborsNodeL3))
                                                newL3Neighbors.Add(neighborsNodeL3);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Debug.Log("02-01 - newL3Neighbors = " + newL3Neighbors.Count);
            newL3Neighbors = newL3Neighbors.Distinct().ToList();

            foreach (GraphPathNode itemL3 in newL3Neighbors)
                newPathNodeL0._neighborsNodesList.Add(itemL3);
        }

        foreach (var item in newL2NeighborsByNodes)
        {
            GraphPathNode node = item.Key;
            List<GraphPathNode> newL2Neighbors = item.Value;

            foreach (GraphPathNode itemL2 in newL2Neighbors)
            {
                node._neighborsNodesList.Add(itemL2);
                itemL2._neighborsNodesList.Add(node);
            }
        }

        List<UnityEngine.Object> newSelectionList = new List<UnityEngine.Object>();

        foreach (GraphPathNode newPathNode in newPathNodes)
        {
            newSelectionList.Add(newPathNode.gameObject);
        }

        Selection.objects = newSelectionList.ToArray();
    }

    [MenuItem("GameObject/>>> Graph Path Node/Create N Neighbors Nodes (Isolated)", false, 12)]
    static void CreateNNeighborsNodesIsolated(MenuCommand menuCommand)
    {
        if (Selection.objects.Length == 0)
        {
            DebugExtension.DevLogWarning(
                "$ > ".ToColor(GoodColors.Pink) +
                "ERROR trying to CreateNNeighborsNodes" + "\n" +
                "NO selected objects" + "\n" +
                "");
            return;
        }

        if (Selection.objects.Length > 0)
        {
            if (menuCommand.context != Selection.objects[0])
                return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;

        List<GraphPathNode> selectedPathNodesList = new List<GraphPathNode>();
        List<GraphPathNode> newPathNodes = new List<GraphPathNode>();

        foreach (GameObject selectedGameObject in selectedObjects)
        {
            if (selectedGameObject != null)
            {
                bool isPathNode =
                    selectedGameObject.TryGetComponent<GraphPathNode>(
                        out GraphPathNode graphPathNode);

                if (isPathNode)
                    selectedPathNodesList.Add(graphPathNode);
            }
        }


        foreach (GraphPathNode selectedPathNode in selectedPathNodesList)
        {
            Transform instanceParent = null;
            Vector3 instancePosition = Vector3.zero;

            if (instanceParent == null)
            {
                instanceParent = selectedPathNode.transform.parent;
                instancePosition = selectedPathNode.transform.position;
            }

            GameObject pathNodeInstanceGameObject = new GameObject();
            GraphPathNode pathNodeInstance = pathNodeInstanceGameObject.AddComponent<GraphPathNode>();

            pathNodeInstanceGameObject.name = "path-node";



            pathNodeInstanceGameObject.transform.SetParent(instanceParent);
            pathNodeInstanceGameObject.transform.position = instancePosition;

            if (instanceParent != null)
            {
                int childIndex = instanceParent.childCount - 1;
                pathNodeInstanceGameObject.transform.SetSiblingIndex(childIndex);
            }

            newPathNodes.Add(pathNodeInstance);

            selectedPathNode._neighborsNodesList.Add(pathNodeInstance);
            pathNodeInstance._neighborsNodesList.Add(selectedPathNode);
        }

        // foreach (GraphPathNode newPathNode in newPathNodes)
        // {
        //     foreach (GraphPathNode selectedPathNode in selectedPathNodesList)
        //     {
        //         selectedPathNode._neighborsNodesList.Add(newPathNode);
        //         newPathNode._neighborsNodesList.Add(selectedPathNode);
        //     }
        // }

        // foreach (GraphPathNode newPathNode in newPathNodes)
        // {
        //     foreach (GraphPathNode newPathNodePair in newPathNodes)
        //     {
        //         if (newPathNode != newPathNodePair)
        //         {
        //             newPathNode._neighborsNodesList.Add(newPathNodePair);
        //             newPathNodePair._neighborsNodesList.Add(newPathNode);
        //         }
        //     }
        // }

        List<UnityEngine.Object> newSelectionList = new List<UnityEngine.Object>();

        foreach (GraphPathNode newPathNode in newPathNodes)
        {
            newSelectionList.Add(newPathNode.gameObject);
        }

        Selection.objects = newSelectionList.ToArray();
    }

    [MenuItem("GameObject/>>> Graph Path Node/Recalculate Neighbors", false, 13)]
    static void RecalculateNeighborsNodes(MenuCommand menuCommand)
    {
        GameObject selectedGameObject = (GameObject)menuCommand.context;

        bool isPathNode =
            selectedGameObject.TryGetComponent<GraphPathNode>(
                out GraphPathNode graphPathNode);

        if (isPathNode)
        {
            graphPathNode._neighborsNodesList = graphPathNode._neighborsNodesList.Distinct().ToList();
            graphPathNode._neighborsNodesList = graphPathNode._neighborsNodesList.Where(item => item != null).ToList();
            graphPathNode._neighborsNodesList = graphPathNode._neighborsNodesList.Where(item => item != graphPathNode).ToList();
        }
    }

    [MenuItem("GameObject/>>> Graph Path Node/Validate mutual neighbors", false, 13)]
    static void ValidateMutualNeighborsNodes(MenuCommand menuCommand)
    {
        GameObject selectedGameObject = (GameObject)menuCommand.context;

        bool isPathNode =
            selectedGameObject.TryGetComponent<GraphPathNode>(
                out GraphPathNode graphPathNode);

        if (isPathNode)
        {
            foreach (GraphPathNode neighborsNode in graphPathNode._neighborsNodesList)
            {
                bool isMutualNeighbor = neighborsNode._neighborsNodesList.Any(item => item == graphPathNode);

                if (!isMutualNeighbor)
                    DebugExtension.DevLog("NOT mutual neighbor!");
            }
            // graphPathNode._neighborsNodesList = graphPathNode._neighborsNodesList.Distinct().ToList();
            // graphPathNode._neighborsNodesList = graphPathNode._neighborsNodesList.Where(item => item != null).ToList();
            // graphPathNode._neighborsNodesList = graphPathNode._neighborsNodesList.Where(item => item != graphPathNode).ToList();
        }
    }

    [MenuItem("GameObject/>>> Graph Path Node/Recalculate position (by colision)", false, 14)]
    static void RecalculatePositionByColision(MenuCommand menuCommand)
    {
        GameObject selectedGameObject = (GameObject)menuCommand.context;

        bool isPathNode =
            selectedGameObject.TryGetComponent<GraphPathNode>(
                out GraphPathNode graphPathNode);

        if (isPathNode)
        {
            Vector3 farTopPosition = graphPathNode.transform.position + (Vector3.up * 1000);
            bool hasHit = Physics.Raycast(farTopPosition, Vector3.down, out RaycastHit hit, 2000f);
            if (hasHit)
            {
                // Debug.DrawLine(hit.point, hit.point + (Vector3.up * 5), Color.red, 2f);
                graphPathNode.transform.position = hit.point;
            }
        }
    }

    [MenuItem("GameObject/>>> Graph Path Node/Remove connections (in selecteds)", false, 14)]
    static void RemoveConnectionsIsSelecteds(MenuCommand menuCommand)
    {
        if (Selection.objects.Length > 0)
        {
            if (menuCommand.context != Selection.objects[0])
                return;
        }

        // GameObject selectedGameObject = (GameObject)menuCommand.context;

        // bool isPathNode =
        //     selectedGameObject.TryGetComponent<GraphPathNode>(
        //         out GraphPathNode graphPathNode);

        // if (isPathNode)
        // {
        //     Vector3 farTopPosition = graphPathNode.transform.position + (Vector3.up * 1000);
        //     bool hasHit = Physics.Raycast(farTopPosition, Vector3.down, out RaycastHit hit, 2000f);
        //     if (hasHit)
        //     {
        //         // Debug.DrawLine(hit.point, hit.point + (Vector3.up * 5), Color.red, 2f);
        //         graphPathNode.transform.position = hit.point;
        //     }
        // }

        GameObject[] selectedObjects = Selection.gameObjects;

        List<GraphPathNode> selectedPathNodesList = new List<GraphPathNode>();

        foreach (GameObject selectedGameObject in selectedObjects)
        {
            if (selectedGameObject != null)
            {
                bool isPathNode =
                    selectedGameObject.TryGetComponent<GraphPathNode>(
                        out GraphPathNode graphPathNode);

                if (isPathNode)
                    selectedPathNodesList.Add(graphPathNode);
            }
        }

        for (int i = 0; i < selectedPathNodesList.Count; i++)
        {
            GraphPathNode graphPathNode = selectedPathNodesList[i];
            graphPathNode._neighborsNodesList =
                graphPathNode._neighborsNodesList.Where(
                    (item) =>
                    {
                        bool isSelected = selectedPathNodesList.Contains(item);
                        return !isSelected;
                    }).ToList();
        }
    }
}
