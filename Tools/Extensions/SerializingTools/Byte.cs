using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace JovDK.SerializingTools.Byte
{

    public static class ByteSerializingTools
    {

        public static byte[] SerializeObjectToByte(this object _object)
        {

            //return (byte[])Convert.ChangeType(_object, typeof(byte[]));

            ///*
            if (_object == null)
                return null;

            BinaryFormatter _binaryFormatter = new BinaryFormatter();
            MemoryStream _memoryStream = new MemoryStream();
            _binaryFormatter.Serialize(_memoryStream, _object);

            return _memoryStream.ToArray();
            //*/

        }

        public static T DeserializeByteToObject<T>(this byte[] _data)
        {

            /*
            using (MemoryStream _memoryStream = new MemoryStream(_data))
            {

                IFormatter _iFormatter = new BinaryFormatter();
                return (T)_iFormatter.Deserialize(_memoryStream);

            }
            */


            /*
            T _Value = (T)new object;
            byte[] NewValue = new byte[];

            object Temp = _Value; // temp to avoid casting issues

            if (_Value is Int32) Temp =
            BitConverter.ToInt32(NewValue, 0);
            else if (_Value is Boolean) Temp =
            BitConverter.ToBoolean(NewValue, 0);
            else if (_Value is Byte) Temp = NewValue[0];
            else if (_Value is Double) Temp =
            BitConverter.ToDouble(NewValue, 0);
            else if (_Value is String) Temp =
            BitConverter.ToString(NewValue);
            else if (_Value is UInt32) Temp =
            BitConverter.ToUInt32(NewValue, 0);
            else if (_Value is UInt16) Temp =
            BitConverter.ToUInt16(NewValue, 0);
            else throw new InvalidTypeException("Unsupported type" + _Value.GetType().ToString());

            _Value = (T)Temp;
            */



            //return (T)Convert.ChangeType(_data, typeof(T));


            MemoryStream _memoryStream = new MemoryStream();
            BinaryFormatter _binaryFormatter = new BinaryFormatter();
            _memoryStream.Write(_data, 0, _data.Length);
            _memoryStream.Seek(0, SeekOrigin.Begin);
            object _object = _binaryFormatter.Deserialize(_memoryStream);

            return (T)_object;


        }

    }

}
