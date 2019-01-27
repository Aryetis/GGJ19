using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTotemBehaviour : MonoBehaviour
{
    [SerializeField]
    private int levelTotemPickup;

    [SerializeField]
    private string messagePickUp;

    [SerializeField]
    private float messagePickUpDuration;

    private GameObject player;
    private GameObject textBox;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textBox = GameObject.Find("TextBoxInfo");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Totem"))
        {
            if (player.GetComponent<PlayerPickUpBehaviour>().totemLevel < levelTotemPickup)
                player.GetComponent<PlayerPickUpBehaviour>().changeTotemLevel(levelTotemPickup);
            textBox.GetComponent<TextBoxBehaviour>().displayText(messagePickUp, messagePickUpDuration);
            gameObject.SetActive(false);
        }
    }
}
