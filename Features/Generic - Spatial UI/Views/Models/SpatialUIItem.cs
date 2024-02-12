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


namespace JovDK.Generic.SpatialUI
{
    public class SpatialUIItem
    {
        public Transform BasePositionTransform;
        public RectTransform RelativeUITransform;
        public Vector3 BasePositionDelta;
        public float CameraDistance = 0f;
        public int ContainerSortIndex = -1;

        public SpatialUIItem(
            Transform basePositionTransform,
            RectTransform relativeUITransform,
            Vector3 basePositionDelta = default)
        {
            BasePositionTransform = basePositionTransform;
            RelativeUITransform = relativeUITransform;
            BasePositionDelta = basePositionDelta;
        }
    }
}
