using System.Collections;
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
            if (other.GetComponentInParent<PlayerWebBehaviour>().enabled == true && other.GetComponentInParent<PlayerWebBehaviour>().getAttachedPillar() == null)
                other.GetComponent<DisplayHelp>().showHelp('Y');
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<WebPillarBehaviour>().playerExitTrigger(triggerSide);
            if (other.GetComponentInParent<PlayerWebBehaviour>().enabled == true && other.GetComponentInParent<PlayerWebBehaviour>().getAttachedPillar() == null)
                other.GetComponent<DisplayHelp>().hideHelp('Y');
        }
    }
}
