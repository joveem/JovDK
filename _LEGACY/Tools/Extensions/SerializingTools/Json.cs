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
        public static string SerializeObjectToJSON(
            this object genericObject,
            bool indent = false)
        {

            string value = "";

            try
            {
                Formatting formatting = indent ? Formatting.Indented : Formatting.None;

                JsonSerializerSettings jsonSerializerSettings =
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    };

                value = JsonConvert.SerializeObject(
                    genericObject,
                    formatting,
                    jsonSerializerSettings);
            }
            catch (System.Exception _error)
            {

                UnityEngine.Debug.LogError("001 | error: " + _error);
                value = "ERROR";
                //throw;

            }

            return value;

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
