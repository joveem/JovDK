using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace JovDK.SerializingTools.Bson
{

    public static class BsonSerializingTools
    {

        public static byte[] SerializeObjectToBSON(this object _object)
        {

            byte[] _value = new byte[] { };

            try
            {

                MemoryStream ms = new MemoryStream();
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                using (BsonWriter writer = new BsonWriter(ms))
                {
                    JsonSerializer serializer = JsonSerializer.CreateDefault(serializerSettings);
                    serializer.Serialize(writer, _object);
                }

                _value = ms.ToArray();

            }
            catch (System.Exception _error)
            {

                UnityEngine.Debug.LogError("001 | error: " + _error);
                //throw;

            }

            return _value;

        }

        public static T DeserializeBsonToObject<T>(this byte[] _bson)
        {

            MemoryStream ms = new MemoryStream(_bson);
            using (BsonReader reader = new BsonReader(ms))
            {
                JsonSerializer serializer = new JsonSerializer();

                return serializer.Deserialize<T>(reader);
            }

        }

    }

}
