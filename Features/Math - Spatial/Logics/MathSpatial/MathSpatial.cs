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

        public static Vector3 GetRandomNearPositionInArea(
            Vector3 initialPosition,
            Vector3 areaStartPosition,
            Vector3 areaEndPosition,
            Vector3 maxDeltaArea)
        {
            Vector3 value = default;

            float randomDeltaMovementX = UnityRandom.Range(-maxDeltaArea.x / 2, maxDeltaArea.x / 2f);
            float randomDeltaMovementY = UnityRandom.Range(-maxDeltaArea.y / 2, maxDeltaArea.y / 2f);
            float randomDeltaMovementZ = UnityRandom.Range(-maxDeltaArea.z / 2, maxDeltaArea.z / 2f);
            Vector3 randomDeltaPosition = new Vector3(randomDeltaMovementX, randomDeltaMovementY, randomDeltaMovementZ);

            value = initialPosition + randomDeltaPosition;
            value = ClampPositionInArea(value, areaStartPosition, areaEndPosition);

            return value;
        }

        public static Vector3 ClampPositionInArea(
            Vector3 basePosition,
            Vector3 areaStartPosition,
            Vector3 areaEndPosition)
        {
            Vector3 value = default;

            float clampedX = Mathf.Clamp(basePosition.x, areaStartPosition.x, areaEndPosition.x);
            float clampedY = Mathf.Clamp(basePosition.y, areaStartPosition.y, areaEndPosition.y);
            float clampedZ = Mathf.Clamp(basePosition.z, areaStartPosition.z, areaEndPosition.z);

            value = new Vector3(clampedX, clampedY, clampedZ);

            return value;
        }
    }
}
