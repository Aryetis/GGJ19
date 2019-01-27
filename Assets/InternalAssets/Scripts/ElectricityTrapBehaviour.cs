using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityTrapBehaviour : MonoBehaviour
{
    private bool totemOnMe = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TotemField"))
        {
            totemOnMe = true;
            foreach (Transform childLightning in transform)
            {
                if (childLightning.name == "Activated")
                    childLightning.gameObject.SetActive(false);
            }
        }
        if(other.gameObject.CompareTag("Player") && !totemOnMe)
        {
            foreach (Transform childLightning in transform)
            {
                if(childLightning.name == "Triggered")
                    childLightning.gameObject.SetActive(true);
            }
            other.gameObject.GetComponent<PlayerDeath>().killPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TotemField"))
        {
            totemOnMe = false;
            foreach (Transform childLightning in transform)
            {
                if (childLightning.name == "Activated")
                    childLightning.gameObject.SetActive(true);
            }
        }
    }
}
