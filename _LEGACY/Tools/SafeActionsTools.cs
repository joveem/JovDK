// system / unity
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        public static void DoIfNull<T>(this T baseObjectValue, Action action, bool debugIfNotNull = true)
        {
            if (baseObjectValue is null || baseObjectValue == null)
                action();
            else if (debugIfNotNull)
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("<");
                stringBuilder.Append(typeof(T).Name);
                stringBuilder.Append("> ");
                stringBuilder.Append(nameof(baseObjectValue));
                stringBuilder.Append(" is ");
                stringBuilder.AppendWithColor("NOT", GoodColors.Pink);
                stringBuilder.Append(" null!");

                int stackBackSteps = 4;
                DebugExtension.DevLogWarning(
                    stackBackSteps,
                    stringBuilder.ToString().ToColor(GoodColors.Orange));
            }
        }

        public static void DoIfNull<T>(this T baseObjectValue, Action action, Action ifNotNullAction)
        {
            if (baseObjectValue is null || baseObjectValue == null)
                action();
            else
                ifNotNullAction();
        }

        public static void DoIfNotNull<T>(this T baseObjectValue, Action action, bool debugIfNull = true)
        {
            if (baseObjectValue is not null && baseObjectValue != null)
                action();
            else if (debugIfNull)
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("<");
                stringBuilder.Append(typeof(T).Name);
                stringBuilder.Append("> is null!");

                int stackBackSteps = 4;
                DebugExtension.DevLogWarning(
                    stackBackSteps,
                    stringBuilder.ToString().ToColor(GoodColors.Orange));
            }
        }

        public static void DoIfNotNull<T>(this T baseObjectValue, Action action, Action ifNullAction)
        {
            if (baseObjectValue is not null && baseObjectValue != null)
                action();
            else
                ifNullAction();
        }

        public static void DoIfAllNotNull<T>(
            this T[] baseObjectsValuesList, Action action, bool debugIfNull = true, bool detailedDebug = false)
        {
            bool hasNull = false;
            StringBuilder stringBuilder = null;

            foreach (T baseObjectValue in baseObjectsValuesList)
            {
                if (baseObjectValue is null || baseObjectValue == null)
                {
                    hasNull = true;

                    if (stringBuilder is null)
                    {
                        stringBuilder = new StringBuilder();
                        stringBuilder.Append("hasNull = ");
                        stringBuilder.Append(hasNull.ToString());
                    }

                    if (!detailedDebug)
                        break;
                    else
                    {
                        stringBuilder.AppendLine();
                        stringBuilder.Append("<");
                        stringBuilder.AppendType(baseObjectValue);
                        stringBuilder.Append("> is null!");
                    }
                }
            }

            if (!hasNull)
                action();
            else if (debugIfNull)
            {
                int stackBackSteps = 4;
                DebugExtension.DevLogWarning(stackBackSteps, stringBuilder.ToString());
            }
        }

        public static void DoIfAllNotNull<T>(
            this T[] baseObjectsValuesList,
            Action action,
            Action ifHasAnyNullAction)
        {
            bool hasNull = false;

            foreach (T baseObjectValue in baseObjectsValuesList)
            {
                if (baseObjectValue is null || baseObjectValue == null)
                {
                    hasNull = true;
                    break;
                }
            }

            if (!hasNull)
                action();
            else
                ifHasAnyNullAction();
        }

        public static void SetActiveIfNotNull<T>(this T baseObjectValue, bool setActive, bool debugIfNull = true) where T : Component
        {
            baseObjectValue.DoIfNotNull(() => baseObjectValue.gameObject.SetActive(setActive), debugIfNull);
        }

        public static void SetActiveIfNotNull(this GameObject baseObjectValue, bool setActive, bool debugIfNull = true)
        {
            baseObjectValue.DoIfNotNull(() => baseObjectValue.SetActive(setActive), debugIfNull);
        }

        public static void SetActiveIfNotNull(this Transform baseObjectValue, bool setActive, bool debugIfNull = true)
        {
            baseObjectValue.DoIfNotNull(() => baseObjectValue.gameObject.SetActive(setActive), debugIfNull);
        }

        public static void SetInverseActiveIfNotNull<T>(this T baseObjectValue, bool debugIfNull = true) where T : Component
        {
            baseObjectValue.DoIfNotNull(() => baseObjectValue.gameObject.SetActive(!baseObjectValue.gameObject.activeSelf), debugIfNull);
        }

        public static void SetInverseActiveIfNotNull(this GameObject baseObjectValue, bool debugIfNull = true)
        {
            baseObjectValue.DoIfNotNull(() => baseObjectValue.SetActive(!baseObjectValue.activeSelf), debugIfNull);
        }

        public static bool TryGetComponent<T>(Component component, out T outValue) where T : Component
        {
            outValue = null;

            try
            {
                outValue = component.GetComponent<T>();
            }
            catch (Exception)
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("<");
                stringBuilder.Append(typeof(T).Name);
                stringBuilder.Append("> object NOT FOUND!");

                int stackBackSteps = 4;
                DebugExtension.DevLogWarning(
                    stackBackSteps,
                    stringBuilder.ToString().ToColor(GoodColors.Orange));
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
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("<");
                stringBuilder.Append(typeof(T).Name);
                stringBuilder.Append("> object NOT FOUND!");

                int stackBackSteps = 4;
                DebugExtension.DevLogWarning(
                    stackBackSteps,
                    stringBuilder.ToString().ToColor(GoodColors.Orange));
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
