// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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
    void DrawNodePosition(Vector3 centerPosition)
    {
        Gizmos.color = new Color(1f, 0f, 1f, 0.5f);
        // if (_customCylinderMesh == null)
        //     _customCylinderMesh = GetCustomMesh(_areaSize, 5f, 8);

        // Mesh customMesh = _customCylinderMesh;
        Mesh customMesh = GetCustomMesh(_areaSize, 5f, 8);
        Gizmos.DrawMesh(customMesh, centerPosition);
    }

    void DrawNeighborsNodes(Vector3 centerPosition)
    {
        for (int i = 0; i < _neighborsNodesList.Count; i++)
        {
            GraphPathNode connectedNodes = _neighborsNodesList[i];

            Vector3 startPosition;
            Vector3 endPosition;
            Color lineColor;

            if (connectedNodes)
            {
                lineColor = new Color(1f, 0f, 1f, 0.5f); ;

                startPosition = transform.position;
                endPosition = connectedNodes.transform.position;
            }
            else
            {
                lineColor = new Color(1f, 0f, 0f);

                Vector3 center = transform.position;
                startPosition = center + (Vector3.down * 5);
                endPosition = center + (Vector3.up * 5);
            }

            Gizmos.color = lineColor;
            // Gizmos.DrawLine(startPosition, endPosition);
            DrawConnectionLine(startPosition, endPosition, lineColor, 5);
        }
    }

    void DrawConnectionLine(Vector3 startPosition, Vector3 endPosition, Color lineColor, int thickness = 100)
    {
        Vector3 middlePosition = endPosition - startPosition;
        middlePosition = middlePosition / 2f;
        middlePosition = startPosition + middlePosition;

        middlePosition = middlePosition + Vector3.up * 20;

        Handles.DrawBezier(
            startPosition,
            endPosition,
            middlePosition,
            middlePosition,
            lineColor,
            null,
            thickness);
    }

    // static Mesh _customCylinderMesh = null;

    public Mesh GetCustomMesh(float radius, float height, int numSegments)
    {
        Mesh mesh = new Mesh();

        int numVertices = (numSegments + 1) * 2 + 2;
        int numTriangles = numSegments * 6 * 2;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numTriangles];
        Vector2[] uv = new Vector2[numVertices];

        int vertexIndex = 0;
        int triangleIndex = 0;

        // Generate vertices and UVs for side
        for (int i = 0; i <= numSegments; i++)
        {
            float angle = (Mathf.PI * 2.0f * i) / numSegments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[vertexIndex] = new Vector3(x, 0f, z);
            vertices[vertexIndex + 1] = new Vector3(x, height, z);

            uv[vertexIndex] = new Vector2((float)i / numSegments, 0f);
            uv[vertexIndex + 1] = new Vector2((float)i / numSegments, 1f);

            vertexIndex += 2;
        }

        // Generate vertices for top and bottom centers
        vertices[vertexIndex] = new Vector3(0f, 0f, 0f); // Center of bottom
        vertices[vertexIndex + 1] = new Vector3(0f, height, 0f); // Center of top
        uv[vertexIndex] = new Vector2(0.5f, 0.5f); // UV for bottom center
        uv[vertexIndex + 1] = new Vector2(0.5f, 0.5f); // UV for top center

        int bottomCenterIndex = numVertices - 2;
        int topCenterIndex = numVertices - 1;

        // Generate triangles for side
        for (int i = 0; i < numSegments; i++)
        {
            triangles[triangleIndex + 2] = i * 2;
            triangles[triangleIndex + 1] = (i + 1) * 2;
            triangles[triangleIndex + 0] = i * 2 + 1;

            triangles[triangleIndex + 5] = (i + 1) * 2;
            triangles[triangleIndex + 4] = (i + 1) * 2 + 1;
            triangles[triangleIndex + 3] = i * 2 + 1;

            triangleIndex += 6;
        }

        // Generate triangles for top and bottom
        for (int i = 0; i < numSegments; i++)
        {
            triangles[triangleIndex] = i * 2;
            triangles[triangleIndex + 1] = (i + 1) * 2;
            triangles[triangleIndex + 2] = bottomCenterIndex;

            triangles[triangleIndex + 3] = (i + 1) * 2 + 1;
            triangles[triangleIndex + 4] = i * 2 + 1;
            triangles[triangleIndex + 5] = topCenterIndex;

            triangleIndex += 6;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
}
