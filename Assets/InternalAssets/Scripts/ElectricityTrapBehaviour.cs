using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityTrapBehaviour : MonoBehaviour
{
    private bool totemOnMe = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Totem")
        {
            totemOnMe = true;
            foreach (Transform childLightning in transform)
            {
                if (childLightning.name == "Activated")
                    childLightning.gameObject.SetActive(false);
            }
        }
        if(other.gameObject.tag == "Player" && !totemOnMe)
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
        if (other.gameObject.tag == "Totem")
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
