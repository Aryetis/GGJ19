using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Front, Right, Back, Left, None };

public class WebPillarBehaviour : MonoBehaviour
{
    private List<Side> touchedSides = new List<Side>();
    private Side currentSide = Side.None;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSide != Side.None && player.GetComponent<PlayerWebBehaviour>().enabled == true 
            && (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.V)) && player.GetComponent<PlayerWebBehaviour>().getAttachedPillar() == null)
        {
            player.GetComponent<PlayerWebBehaviour>().attachPillar(gameObject, currentSide);
        }
    }

    public void playerTouchedTrigger(Side side)
    {
        if(touchedSides.Count == 0)
        {
            currentSide = side;
        }

        touchedSides.Add(side);
    }

    public void playerExitTrigger(Side side)
    {
        touchedSides.Remove(side);
        if (touchedSides.Count > 0)
        {
            currentSide = touchedSides[0];
        }
        else { currentSide = Side.None; }
    }
}
