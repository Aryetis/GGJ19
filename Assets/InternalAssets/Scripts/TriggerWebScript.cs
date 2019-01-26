using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWebScript : MonoBehaviour
{
    [SerializeField]
    private Side triggerSide;

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<WebPillarBehaviour>().playerTouchedTrigger(triggerSide);
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<WebPillarBehaviour>().playerExitTrigger(triggerSide);
    }
}
