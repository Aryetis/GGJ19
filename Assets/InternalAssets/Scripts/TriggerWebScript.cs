﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWebScript : MonoBehaviour
{
    [SerializeField]
    public Side triggerSide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<WebPillarBehaviour>().playerTouchedTrigger(triggerSide);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<WebPillarBehaviour>().playerExitTrigger(triggerSide);
        }
    }
}
