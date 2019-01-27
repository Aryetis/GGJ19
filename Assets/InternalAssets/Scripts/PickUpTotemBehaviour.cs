using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTotemBehaviour : MonoBehaviour
{
    [SerializeField]
    private int levelTotemPickup;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Totem"))
        {
            if (player.GetComponent<PlayerPickUpBehaviour>().totemLevel < levelTotemPickup)
                player.GetComponent<PlayerPickUpBehaviour>().changeTotemLevel(levelTotemPickup);
            gameObject.SetActive(false);
        }
    }
}
