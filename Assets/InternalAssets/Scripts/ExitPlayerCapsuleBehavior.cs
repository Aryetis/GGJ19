using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlayerCapsuleBehavior : MonoBehaviour
{
    private int nbrCollisions = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("Totem") || other.gameObject.CompareTag("TotemField")))
        {
            ++nbrCollisions;
            transform.parent.GetComponent<TotemBehavior>().allowUnfusion = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.gameObject.CompareTag("Totem") || other.gameObject.CompareTag("TotemField")))
        {
            --nbrCollisions;
            if (nbrCollisions <= 0)
                transform.parent.GetComponent<TotemBehavior>().allowUnfusion = true;
        }
    }
}
