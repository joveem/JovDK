using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    // Start is called before the first frame update


    public Action<Collider> onTriggerEnterAction = null;
    public Action<Collider> onTriggerExitAction = null;
    public Action<Collider> onTriggerStayAction = null;

    private void OnTriggerEnter(Collider _other)
    {

        if (onTriggerEnterAction != null)
            onTriggerEnterAction(_other);

    }

    private void OnTriggerExit(Collider _other)
    {

        if (onTriggerExitAction != null)
            onTriggerExitAction(_other);

    }

    private void OnTriggerStay(Collider _other)
    {

        if (onTriggerStayAction != null)
            onTriggerStayAction(_other);

    }
}
