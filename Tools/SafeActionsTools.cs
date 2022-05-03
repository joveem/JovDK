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

        public static void DoIfNull<T>(this T _object, Action _action, bool debugIfNotNull = true)
        {

            if (_object == null || _object.Equals(null))
                _action();
            else if (debugIfNotNull)
                DebugExtension.DevLogWarning("<" + typeof(T) + ">" + (nameof(_object) + " IS NOT NULL!").ToColor(GoodCollors.orange));

        }

        public static void DoIfNotNull<T>(this T _object, Action _action)
        {

            if (_object != null && !_object.Equals(null))
                _action();
            else
                DebugExtension.DevLogWarning("<" + typeof(T) + ">" + (nameof(_object) + " IS NULL!").ToColor(GoodCollors.orange));

        }

        public static void SetActiveIfNotNull<T>(this T _object, bool _value) where T : MonoBehaviour
        {

            _object.DoIfNotNull(() => _object.gameObject.SetActive(_value));

        }

        public static void SetActiveIfNotNull(this GameObject _object, bool _value)
        {

            _object.DoIfNotNull(() => _object.SetActive(_value));

        }

        public static void SetActiveIfNotNull(this Transform _object, bool _value)
        {

            _object.DoIfNotNull(() => _object.gameObject.SetActive(_value));

        }


        public static bool TryGetComponent<T>(Component component, out T value) where T : Component
        {

            value = null;

            try
            {

                value = component.GetComponent<T>();

            }
            catch (System.Exception)
            {

                DebugExtension.DevLogWarning(("<" + typeof(T) + ">object NOT FOUND!").ToColor(GoodCollors.orange));

            }

            if (value != null)
                return true;
            else
                return false;

        }

        public static bool TryFindGameObject<T>(out T value) where T : Component
        {

            value = null;

            try
            {

                value = GameObject.FindObjectOfType<T>();

            }
            catch (System.Exception)
            {

                DebugExtension.DevLogWarning(("<" + typeof(T) + "> object NOT FOUND!").ToColor(GoodCollors.orange));

            }

            if (value != null)
                return true;
            else
                return false;

        }

        #region Butons 
        public static void SetOnClickIfNotNull(this Button _button, UnityEngine.Events.UnityAction _action)
        {

            _button.DoIfNotNull(() =>
                _action.DoIfNotNull(() =>
                    _button.onClick.AddListener(_action)));

        }

        public static void SetInteractableIfNotNull(this Button _button, bool _value)
        {

            _button.DoIfNotNull(() => _button.interactable = _value);

        }
        #endregion 

    }

}
