using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityTrapBehaviour : MonoBehaviour
{
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
        if(other.gameObject.tag == "Player")
        {
            foreach (Transform childLightning in transform)
            {
                childLightning.gameObject.SetActive(true);
            }
            other.gameObject.GetComponent<PlayerDeath>().killPlayer();
        }
    }
}
