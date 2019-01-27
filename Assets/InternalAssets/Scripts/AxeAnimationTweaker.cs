using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AxeAnimationTweaker : MonoBehaviour
{
    public float animSpeed = 1.0f;
    [Range(0, 10)]public float initialTime = 0.0f;

    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        if (anim == null)
        {
            Debug.Break();
            Debug.Log("OOPS");
        }

        SetSpeed(animSpeed);
        SetTime(initialTime);
    }

    public void SetSpeed(float f)
    {
        anim["Anim_GreatAxeTrap_Play"].speed = f;
    }

    public void SetTime(float f)
    {
        anim["Anim_GreatAxeTrap_Play"].time = f;
    }

    private void Update()
    {
    }

    public void StopAnim()
    {

        //anim["Anim_GreatAxeTrap_Play"].wrapMode = WrapMode.Once; // This skip the animation to the end FU*** YOU UNITY
        // Let's compute the remaining time of the animation and then change the wrap mode
        float foo = 0.0f;
            foo = anim["Anim_GreatAxeTrap_Play"].length*2.0f - (anim["Anim_GreatAxeTrap_Play"].time % anim["Anim_GreatAxeTrap_Play"].length * 2.0f);

        Invoke("StopNOW", foo);
    }

    private void StopNOW()
    {
        anim["Anim_GreatAxeTrap_Play"].wrapMode = WrapMode.Once;
    }

    public void RunAnim()
    {
        anim["Anim_GreatAxeTrap_Play"].wrapMode = WrapMode.Loop;
        anim.Play("Anim_GreatAxeTrap_Play");
    }
}
