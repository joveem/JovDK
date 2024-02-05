// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Physics.Triggers
{
    public partial class TriggerEmitter : MonoBehaviour
    {
        void OnTriggerEnter(Collider _other)
        {
            if (OnTriggerEnterCallback != null)
                OnTriggerEnterCallback(_other);
        }

        void OnTriggerExit(Collider _other)
        {
            if (OnTriggerExitCallback != null)
                OnTriggerExitCallback(_other);
        }

        void OnTriggerStay(Collider _other)
        {
            if (OnTriggerStayCallback != null)
                OnTriggerStayCallback(_other);
        }
    }
}
