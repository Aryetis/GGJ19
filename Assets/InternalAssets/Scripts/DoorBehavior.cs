using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : TogglableInterface
{
    private GameObject DoorLeaf;
    private Vector3 upPosition, downPosition;
    private bool movingUp, movingDown;

    void Start()
    {
        DoorLeaf = transform.Find("DoorLeaf").gameObject;
        upPosition = DoorLeaf.transform.position;
        downPosition = DoorLeaf.transform.position + Vector3.down * 12.0f;
        movingUp = false;
        movingDown = false;
    }

    private void Update()
    {
        if (movingUp)
        {
            Vector3 smoothedPosition = Vector3.Lerp(DoorLeaf.transform.position, upPosition, 0.02f);
            DoorLeaf.transform.position = smoothedPosition;
            if ((DoorLeaf.transform.position - upPosition).magnitude <= 0.02f)
                movingUp = false;
        }
        else if (movingDown)
        {
            Vector3 smoothedPosition = Vector3.Lerp(DoorLeaf.transform.position, downPosition, 0.02f);
            DoorLeaf.transform.position = smoothedPosition;
            if ((DoorLeaf.transform.position - downPosition).magnitude <= 0.02f)
                movingDown = false;
        }
    }

    public override void ToggleOn()
    {
        movingDown = true;
        movingUp = false;
    }

    public override void ToggleOff()
    {
        movingDown = false;
        movingUp = true;
    }
}
