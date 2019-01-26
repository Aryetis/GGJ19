using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrapBehavior : TogglableInterface
{
    public float timeToActivate = 0.6f;
    public float timeToStayOpen = 3.0f;

    private float timerToActivate, timerToReset;
    private GameObject leftDoor, rightDoor;
    private bool ticking = false;
    private bool open = false;
    private bool armed = true;

    // Start is called before the first frame update
    void Start()
    {
        leftDoor = transform.parent.Find("LeftDoor").gameObject;
        rightDoor = transform.parent.Find("RightDoor").gameObject;
        timerToActivate = timeToActivate;
        timerToReset = timeToStayOpen;
    }

    void Update()
    {
        if(ticking)
        {
            timerToActivate -= Time.deltaTime;
            if (timerToActivate <= 0.0f)
            {
                ticking = false;
                open = true;
                leftDoor.SetActive(false);
                rightDoor.SetActive(false);
            }
        }

        if(open)
        {
            timerToReset -= Time.deltaTime;
            if (timerToReset <= 0.0f)
            {
                leftDoor.SetActive(true);
                rightDoor.SetActive(true);
                open = false;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && armed)
        {
            ticking = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ticking = false;
            timerToActivate = timeToActivate;
        }
    }

    public override void ToggleOff()
    {
        armed = true;
    }

    public override void ToggleOn()
    {
        armed = false;
    }
}
