using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlayerCapsuleBehavior : MonoBehaviour
{
    private int nbrCollisions = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("Totem") || other.gameObject.CompareTag("TotemField") || other.gameObject.CompareTag("IgnoredByTotemExit")))
        {
            Debug.Log("COLLIDING WITH : " + other.gameObject.name);
            ++nbrCollisions;
            transform.parent.GetComponent<TotemBehavior>().allowUnfusion = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.gameObject.CompareTag("Totem") || other.gameObject.CompareTag("TotemField") || other.gameObject.CompareTag("IgnoredByTotemExit")))
        {
            Debug.Log("UNCOLLIDING WITH : " + other.gameObject.name);
            --nbrCollisions;
            if (nbrCollisions <= 0)
                transform.parent.GetComponent<TotemBehavior>().allowUnfusion = true;
        }
    }
}
