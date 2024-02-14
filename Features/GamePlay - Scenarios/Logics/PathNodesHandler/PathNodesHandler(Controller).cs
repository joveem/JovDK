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
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


public partial class PathNodesHandler : MonoBehaviour
{

    public List<GraphPathNode> GetShortestPath(
        Vector3 startPosition,
        Vector3 endPosition)
    {
        List<GraphPathNode> value = new List<GraphPathNode>();

        GraphPathNode startNode = GetNearestNode(startPosition);
        GraphPathNode endNode = GetNearestNode(endPosition);

        value = GetShortestPathBFS(startNode, endNode);

        return value;
    }

    List<GraphPathNode> GetShortestPathBFS(
        GraphPathNode startVertex,
        GraphPathNode endVertex)
    {
        Dictionary<GraphPathNode, GraphPathNode> predecessor = new Dictionary<GraphPathNode, GraphPathNode>();
        Queue<GraphPathNode> queue = new Queue<GraphPathNode>();
        HashSet<GraphPathNode> visited = new HashSet<GraphPathNode>();

        visited.Add(startVertex);
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            GraphPathNode currentVertex = queue.Dequeue();

            List<GraphPathNode> adjacencyList = currentVertex.GetNeighborsNodesList();

            foreach (GraphPathNode adjacentVertex in adjacencyList)
            {
                if (!visited.Contains(adjacentVertex))
                {
                    visited.Add(adjacentVertex);
                    queue.Enqueue(adjacentVertex);
                    predecessor[adjacentVertex] = currentVertex;
                }
            }
        }

        // Reconstruct the shortest path
        List<GraphPathNode> shortestPath = new List<GraphPathNode>();
        GraphPathNode current = endVertex;
        while (current != startVertex)
        {
            shortestPath.Add(current);
            current = predecessor[current];
        }
        shortestPath.Add(startVertex);
        shortestPath.Reverse();

        return shortestPath;
    }

    GraphPathNode GetNearestNode(
        Vector3 basePosition)
    {
        GraphPathNode value = null;

        float minDistance = Mathf.Infinity;
        foreach (var item in _pathNodesList)
        {
            if (item == null)
                continue;

            float currentDistance = Vector3.Distance(item.transform.position, basePosition);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                value = item;
            }
        }

        return value;
    }
}
