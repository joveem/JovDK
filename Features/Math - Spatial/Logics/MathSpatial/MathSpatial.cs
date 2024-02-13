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
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Math
{
    public static class MathSpatial
    {
        public static List<Vector3> UniformDistribuitionOverArea(
            Vector3 startPosition,
            Vector3 endPosition,
            int positionsAmount)
        {
            List<Vector3> positionsList = null;

            Vector3 areaSize = endPosition - startPosition;
            positionsList = UniformDistribuitionOverArea(areaSize, positionsAmount);

            for (int i = 0; i < positionsList.Count; i++)
            {
                Vector3 position = positionsList[i];
                position += startPosition;
                positionsList[i] = position;
            }

            return positionsList;
        }

        public static List<Vector3> UniformDistribuitionOverArea(
            Vector3 areaSize,
            int positionsAmount)
        {
            List<Vector3> positionsList = new List<Vector3>();

            float xValue = 1;
            float zValue = 1;
            float horizontalFactor = areaSize.x / areaSize.z;
            float verticalFactor = areaSize.z / areaSize.x;
            int currentPositionsAmount = (int)(xValue * zValue);

            while (positionsAmount > currentPositionsAmount)
            {
                float nextHorizontalFactor = xValue + 1 / zValue;
                float nextVerticalFactor = zValue + 1 / xValue;

                if (nextHorizontalFactor / horizontalFactor < nextVerticalFactor / verticalFactor)
                    xValue++;
                else
                    zValue++;

                currentPositionsAmount = (int)(xValue * zValue);
            }

            float horizontalGap = areaSize.x / (xValue - 1);
            float verticalGap = areaSize.z / (zValue - 1);

            for (float z = 0; z < zValue; z++)
            {
                for (float x = 0; x < xValue; x++)
                {
                    Vector3 position = new Vector3(x * horizontalGap, 0, z * verticalGap);
                    positionsList.Add(position);
                }
            }

            return positionsList;
        }
    }
}
