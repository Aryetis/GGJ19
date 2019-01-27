using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehavior : TogglableInterface
{
    private AxeAnimationTweaker aat;
    private Collider leftColBox, rightColBox;
    
    void Start()
    {
        aat = GetComponent<AxeAnimationTweaker>();
        leftColBox = transform.Find("GreatAxe").Find("LeftColBox").gameObject.GetComponent<BoxCollider>();
        rightColBox = transform.Find("GreatAxe").Find("RightColBox").gameObject.GetComponent<BoxCollider>();
    }

    public override void ToggleOff()
    {
        leftColBox.enabled = true;
        rightColBox.enabled = true;
        aat.RunAnim();
    }

    public override void ToggleOn()
    {
        leftColBox.enabled = false;
        rightColBox.enabled = false;
        aat.StopAnim();
    }
}
