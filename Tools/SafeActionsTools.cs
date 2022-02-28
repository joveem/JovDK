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
        // Start is called before the first frame update

        public static void SetActiveIfNotNull(this GameObject _object, bool _value)
        {

            if (_object != null)
            {

                _object.SetActive(_value);

            }
            else
            {

                // TODO: get original object property name
                DebugExtension.DevLogError(nameof(_object) + " IS NULL!");

            }

        }

        public static void SetActiveIfNotNull(this Transform _object, bool _value)
        {

            if (_object != null)
            {

                _object.gameObject.SetActive(_value);

            }
            else
            {

                // TODO: get original object property name
                DebugExtension.DevLogError(nameof(_object) + " IS NULL!");

            }

        }

        public static void SetOnClickIfNotNull(this Button _button, Action _action)
        {

            if (_button != null)
            {

                if (_action != null)
                {

                    _button.onClick.AddListener(() =>
                    {

                        _action();

                    });

                }
                else
                {

                    DebugExtension.DevLogError("_action IS NULL!");

                }


            }
            else
            {

                // TODO: get original object property name
                DebugExtension.DevLogError(nameof(_button) + " IS NULL!");

            }

        }

        public static void DoIfNotNull(this object _object, Action _action)
        {

            if (_object != null)
            {

                _action();

            }
            else
            {

                DebugExtension.DevLogWarning((nameof(_object) + " IS NULL!").ToColor(GoodCollors.orange));

            }


        }


    }

}
