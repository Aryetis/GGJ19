using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : TogglableInterface
{
    private GameObject DoorLeaf;

    void Start()
    {
        DoorLeaf = transform.Find("DoorLeaf").gameObject;
    }

    public override void ToggleOn()
    {
        DoorLeaf.SetActive(false);
    }

    public override void ToggleOff()
    {
        DoorLeaf.SetActive(true);
    }
}
