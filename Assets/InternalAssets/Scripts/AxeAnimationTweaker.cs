using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimationTweaker : MonoBehaviour
{
    public float animSpeed = 1.0f;

    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        if (anim == null)
        {
            Debug.Break();
            Debug.Log("OOPS");
        }
        anim["Anim_GreatAxeTrap_Play"].speed = animSpeed;
    }


}
