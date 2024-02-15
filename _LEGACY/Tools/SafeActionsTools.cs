using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JovDK.Debug;


namespace JovDK.SafeActions
{

    public static class SafeActionsTools
    {

        public static void DoIfNull<T>(this T objectValue, Action action, bool debugIfNotNull = true)
        {

            if (objectValue == null || objectValue.Equals(null))
                action();
            else if (debugIfNotNull)
            {

                string debugText =
                    "<" + typeof(T) + ">" +
                    (nameof(objectValue) + " IS NOT NULL!").ToColor(GoodColors.Orange);

                DebugExtension.DevLogWarning(debugText);

            }

        }

        public static void DoIfNull<T>(this T objectValue, Action action, Action ifNotNullaction)
        {

            if (objectValue == null || objectValue.Equals(null))
                action();
            else
                ifNotNullaction();

        }

        public static void DoIfNotNull<T>(this T objectValue, Action action, bool debugIfNull = true)
        {

            if (objectValue != null && !objectValue.Equals(null))
                action();
            else if (debugIfNull)
                DebugExtension.DevLogWarning("<" + typeof(T) + ">" + (nameof(objectValue) + " IS NULL!").ToColor(GoodColors.Orange));

        }

        public static void DoIfNotNull<T>(this T @object, Action action, Action ifNullAction)
        {

            if (@object != null && !@object.Equals(null))
                action();
            else
                ifNullAction();

        }

        public static void SetActiveIfNotNull<T>(this T objectValue, bool setActive) where T : Component
        {

            objectValue.DoIfNotNull(() => objectValue.gameObject.SetActive(setActive));

        }

        public static void SetActiveIfNotNull(this GameObject objectValue, bool setActive)
        {

            objectValue.DoIfNotNull(() => objectValue.SetActive(setActive));

        }

        public static void SetActiveIfNotNull(this Transform objectValue, bool setActive)
        {

            objectValue.DoIfNotNull(() => objectValue.gameObject.SetActive(setActive));

        }


        public static bool TryGetComponent<T>(Component component, out T outValue) where T : Component
        {

            outValue = null;

            try
            {

                outValue = component.GetComponent<T>();

            }
            catch (System.Exception)
            {

                string debugText =
                    "<" + typeof(T) + ">" +
                    "object NOT FOUND!".ToColor(GoodColors.Orange);

                DebugExtension.DevLogWarning(debugText);

            }

            if (outValue != null)
                return true;
            else
                return false;

        }

        public static bool TryFindGameObject<T>(out T outValue) where T : Component
        {

            outValue = null;

            try
            {

                outValue = GameObject.FindObjectOfType<T>();

            }
            catch (System.Exception)
            {

                DebugExtension.DevLogWarning(("<" + typeof(T) + "> object NOT FOUND!").ToColor(GoodColors.Orange));

            }

            if (outValue != null)
                return true;
            else
                return false;

        }

        #region Butons 
        public static void SetOnClickIfNotNull(this Button button, UnityEngine.Events.UnityAction action)
        {

            button.DoIfNotNull(() =>
                action.DoIfNotNull(() =>
                    button.onClick.AddListener(action)));

        }

        public static void SetInteractableIfNotNull(this Button button, bool setInteractable)
        {

            button.DoIfNotNull(() => button.interactable = setInteractable);

        }
        #endregion 

    }

}
