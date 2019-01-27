using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrapBehavior : TogglableInterface
{
    public float timeToActivate = 0.6f;
    public float timeToStayOpen = 3.0f;

    private float timerToActivate, timerToReset;
    private bool ticking;
    private bool armed;
    private GameObject cube;
    private Vector3 cubeUpPosition, cubeDownPosition;
    private bool movingDown, movingUp;

    private int cpt;

    // Start is called before the first frame update
    void Start()
    {
        cube = transform.Find("CubeTaMere").gameObject;
        movingDown = false;
        movingUp = false;
        cpt = 0;
        ticking = false;
        armed = true;
        timerToActivate = timeToActivate;
        timerToReset = timeToStayOpen;
        cubeUpPosition = cube.transform.position;
        cubeDownPosition = cube.transform.position + Vector3.down * 4.0f;
    }

    void Update()
    {
        if(ticking)
        {
            timerToActivate -= Time.deltaTime;
            if (timerToActivate <= 0.0f) // FIRE ! (it will animation will make it come back in place
            {
                movingDown = true;
                ticking = false;
                timerToActivate = timeToActivate;
            }
        }

        if (movingDown)
        {
            Vector3 smoothedPosition = Vector3.Lerp(cube.transform.position, cubeDownPosition, 0.04f);
            cube.transform.position = smoothedPosition;
            if ((cube.transform.position - cubeDownPosition).magnitude <= 0.02f)
            {
                movingDown = false;
                movingUp = true;
            }
        }
        else if (movingUp)
        {
            Vector3 smoothedPosition = Vector3.Lerp(cube.transform.position, cubeUpPosition, 0.02f);
            cube.transform.position = smoothedPosition;
            if ((cube.transform.position - cubeUpPosition).magnitude <= 0.02f)
                movingUp = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Totem")) && armed)
        {
            cpt++;
            ticking = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Totem"))
        {
            cpt--;
            if (cpt <= 0)
            {
                ticking = false;
                timerToActivate = timeToActivate;
            }
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
