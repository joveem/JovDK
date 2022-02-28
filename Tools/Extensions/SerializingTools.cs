using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

public static class SerializingTools
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
            throw;

        }

        return _value;

    }

    public static T DeserializeJsonToObject<T>(this string _text)
    {

        return JsonConvert.DeserializeObject<T>(_text);

    }

    public static T DeepClone<T>(this T _object)
    {

        return (T)JsonConvert.DeserializeObject<T>(_object.SerializeObjectToJSON());

    }

}
