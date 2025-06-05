// system / unity
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debugging;

// from project
// ...


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
                    nameof(objectValue) + (" IS " + "NOT".ToColor(GoodColors.Pink) + " NULL!").ToColor(GoodColors.Orange);

                // DebugExtension.DevLogWarning(debugText, 4);
                DebugExtension.DevLogWarning(4, debugText);

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
                // DebugExtension.DevLogWarning("<" + typeof(T) + ">" + (nameof(objectValue) + " IS NULL!").ToColor(GoodColors.Orange), 4);
                DebugExtension.DevLogWarning(4, "<" + typeof(T) + ">" + (nameof(objectValue) + " IS NULL!").ToColor(GoodColors.Orange));

        }

        public static void DoIfNotNull<T>(this T @object, Action action, Action ifNullAction)
        {

            if (@object != null && !@object.Equals(null))
                action();
            else
                ifNullAction();

        }

        public static void SetActiveIfNotNull<T>(this T objectValue, bool setActive, bool debugIfNull = true) where T : Component
        {

            objectValue.DoIfNotNull(() => objectValue.gameObject.SetActive(setActive), debugIfNull);

        }

        public static void SetActiveIfNotNull(this GameObject objectValue, bool setActive, bool debugIfNull = true)
        {

            objectValue.DoIfNotNull(() => objectValue.SetActive(setActive), debugIfNull);

        }

        public static void SetActiveIfNotNull(this Transform objectValue, bool setActive, bool debugIfNull = true)
        {

            objectValue.DoIfNotNull(() => objectValue.gameObject.SetActive(setActive), debugIfNull);

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

                // DebugExtension.DevLogWarning(debugText, 4);
                DebugExtension.DevLogWarning(4, debugText);

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
                outValue = GameObject.FindFirstObjectByType<T>();
            }
            catch (Exception)
            {
                DebugExtension.DevLogWarning(4, ("<" + typeof(T) + "> object NOT FOUND!").ToColor(GoodColors.Orange));
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

        #region Image
        public static void SetSpriteIfNotNull(this Image image, Sprite sprite, bool debugIfNull = true)
        {
            image.DoIfNotNull(() => image.sprite = sprite, debugIfNull);
        }

        public static void SetColorIfNotNull(this Image image, Color color, bool debugIfNull = true)
        {
            image.DoIfNotNull(() => image.color = color, debugIfNull);
        }

        public static void SetColorAlphaIfNotNull(this Image image, float alphaValue, bool debugIfNull = true)
        {
            image.DoIfNotNull(() =>
            {
                Color color = image.color;

                color.a = alphaValue;
                image.color = color;
            }, debugIfNull);
        }
        #endregion Image

        #region Text
        public static void SetTextIfNotNull(this TextMeshProUGUI text, string content, bool debugIfNull = true)
        {
            text.DoIfNotNull(() => text.text = content, debugIfNull);
        }
        #endregion Text
    }

}
