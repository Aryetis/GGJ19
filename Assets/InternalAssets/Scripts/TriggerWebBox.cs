using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWebBox : MonoBehaviour
{
    [SerializeField]
    private Side triggerSide;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponentInParent<WebBoxBehaviour>().playerTouchedTrigger(triggerSide);
            if (other.GetComponentInParent<PlayerWebBehaviour>().canAttachBox(triggerSide))
                other.GetComponent<DisplayHelp>().showHelp('Y');
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<WebBoxBehaviour>().playerExitTrigger(triggerSide);
            if (other.GetComponentInParent<PlayerWebBehaviour>().canAttachBox(triggerSide))
                other.GetComponent<DisplayHelp>().hideHelp('Y');
        }
    }
}
