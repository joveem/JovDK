using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace JovDK.SerializingTools.Json
{

    public static class JsonSerializingTools
    {
        public static string SerializeObjectToJSON(this object _object)
        {

            string _value = "";

            try
            {

                _value = JsonConvert.SerializeObject(_object,
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            }
            catch (System.Exception _error)
            {

                UnityEngine.Debug.LogError("001 | error: " + _error);
                _value = "ERROR";
                //throw;

            }

            return _value;

        }

        public static T DeserializeJsonToObject<T>(this string _text)
        {

            return JsonConvert.DeserializeObject<T>(_text);

        }

        public static T ConvertDynamicJsonToObject<T>(dynamic dynamicObject)
        {

            return ((Newtonsoft.Json.Linq.JObject)dynamicObject).ToObject<T>();

        }

        public static T ConvertJsonObjectToObject<T>(object dynamicObject)
        {

            return ((Newtonsoft.Json.Linq.JObject)dynamicObject).ToObject<T>();

        }

    }

}
