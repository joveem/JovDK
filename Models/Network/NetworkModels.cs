using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JovDK.Models.Network
{

    [System.Serializable]
    public class SerializableVector3
    {

        public SerializableVector3()
        {

            X = 0f;
            Y = 0f;
            Z = 0f;

        }

        public SerializableVector3(float x, float y, float z)
        {

            X = x;
            Y = y;
            Z = z;

        }

        public SerializableVector3(Vector3 _vector3)
        {

            X = _vector3.x;
            Y = _vector3.y;
            Z = _vector3.z;

        }

        public float X;
        public float Y;
        public float Z;

        public Vector3 Vector3
        {
            get
            {
                return new Vector3(X, Y, Z);
            }
            set
            {

                X = value.x;
                Y = value.y;
                Z = value.z;

            }
        }

        public Vector3 ToVector3()
        {

            return new Vector3(X, Y, Z);

        }

    }

}
