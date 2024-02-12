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
using System.Linq;

// from project
// ...


namespace JovDK.Generic.SpatialUI
{
    public partial class SpatialUIHandler : MonoBehaviour
    {
        void SetInitialState()
        {
            if (_baseCamera == null)
                _baseCamera = Camera.main;
        }

        public void SetBaseCamera(Camera baseCamera)
        {
            _baseCamera = baseCamera;
        }

        public void RegisterSpatialUIItems(
            Transform basePositionTransform,
            RectTransform relativeUITransform,
            Vector3 basePositionDelta = default)
        {
            SpatialUIItem spatialUIItem = new SpatialUIItem(
                                            basePositionTransform,
                                            relativeUITransform,
                                            basePositionDelta);

            RegisterSpatialUIItems(spatialUIItem);
        }

        public void RegisterSpatialUIItems(SpatialUIItem spatialUIItem)
        {
            if (!_currentSpatialUIItemList.Contains(spatialUIItem))
                _currentSpatialUIItemList.Add(spatialUIItem);
            else
            {
                string debugText =
                    "ERROR trying to RegisterSpatialUIItems" + "\n" +
                    "spatialUIItem IS DUPLICATED!" + "\n" +
                    "(item was not be added)" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
            }
        }

        void HandleUIPositioning()
        {
            if (_baseCamera == null)
                _baseCamera = Camera.main;

            foreach (SpatialUIItem spatialUIItem in _currentSpatialUIItemList)
            {
                spatialUIItem.DoIfNotNull(() =>
                {
                    Vector3 basePosition = spatialUIItem.BasePositionTransform.position;
                    float cameraDistance = Vector3.Distance(basePosition, _baseCamera.transform.position);
                    basePosition += spatialUIItem.BasePositionDelta;

                    Vector3 screenPosition = _baseCamera.WorldToScreenPoint(basePosition);
                    spatialUIItem.RelativeUITransform.position = screenPosition;

                    spatialUIItem.CameraDistance = cameraDistance;
                });
            }
        }

        void HandleUISorting()
        {
            List<int> availableIndexesList = new List<int>();

            foreach (SpatialUIItem spatialUIItem in _currentSpatialUIItemList)
            {
                spatialUIItem.DoIfNotNull(() =>
                {
                    int containerSortIndex = spatialUIItem.RelativeUITransform.transform.GetSiblingIndex();
                    availableIndexesList.Add(containerSortIndex);
                });
            }

            availableIndexesList = availableIndexesList.OrderBy((item) => item).ToList();
            _currentSpatialUIItemList = _currentSpatialUIItemList.OrderByDescending((item) => item.CameraDistance).ToList();

            for (int i = 0; i < _currentSpatialUIItemList.Count; i++)
            {
                SpatialUIItem spatialUIItem = _currentSpatialUIItemList[i];
                int containerSortIndex = availableIndexesList[i];

                spatialUIItem.DoIfNotNull(() =>
                {
                    spatialUIItem.ContainerSortIndex = containerSortIndex;
                    spatialUIItem.RelativeUITransform.transform.SetSiblingIndex(containerSortIndex);
                });
            }
        }
    }
}
