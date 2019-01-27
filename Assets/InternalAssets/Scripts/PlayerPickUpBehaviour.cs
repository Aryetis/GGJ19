using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private int totemLevel=0;

    private GameObject totem;
    private Transform totemVisu;


    // Start is called before the first frame update
    void Start()
    {
        totem = GameObject.Find("Totem");
        totemVisu = totem.transform.Find("TotemVisu");
        changeTotemLevel(totemLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTotemLevel(int level)
    {
        totemLevel = level;
        switch (level)
        {
            case 0:
                foreach (Transform visu in totemVisu)
                {
                    visu.gameObject.SetActive(false);
                }

                totem.SetActive(false);
                GetComponent<ChargePower>().enabled = false;
                GetComponent<PlayerWebBehaviour>().enabled = false;
                break;
            case 1:
                foreach (Transform visu in totemVisu)
                {
                    visu.gameObject.SetActive(visu.name == "Turtle");
                }


                totem.SetActive(true);
                GetComponent<ChargePower>().enabled = false;
                GetComponent<PlayerWebBehaviour>().enabled = false;
                break;
            case 2:
                foreach (Transform visu in totemVisu)
                {
                    visu.gameObject.SetActive(visu.name == "Turtle" || visu.name == "Aries");
                }
                totem.SetActive(true);
                GetComponent<ChargePower>().enabled = true;
                GetComponent<PlayerWebBehaviour>().enabled = false;
                break;
            case 3:
                foreach (Transform visu in totemVisu)
                {
                    visu.gameObject.SetActive(true);
                }

                totem.SetActive(true);
                GetComponent<ChargePower>().enabled = true;
                GetComponent<PlayerWebBehaviour>().enabled = true;
                break;

        }
    }
}
