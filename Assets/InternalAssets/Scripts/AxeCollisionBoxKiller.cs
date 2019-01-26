using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollisionBoxKiller : MonoBehaviour
{
    private PlayerDeath pd;
    private Animation anim;
    private float animSpeed;
    // Start is called before the first frame update
    void Start()
    {
        pd = GameObject.Find("Player").GetComponent<PlayerDeath>();
        anim = transform.parent.parent.GetComponent<Animation>();
        if (anim == null)
        {
            Debug.Break();
            Debug.Log("OOPS");
        }
        animSpeed = transform.parent.parent.GetComponent<AxeAnimationTweaker>().animSpeed;
    }

    private void OnTriggerEnter(Collider col)
    {
        //ContactPoint contact = col.contacts[0];
        //if (contact.normal == transform.forward)
        {
            // TODO add col.normal direction check
            if (col.gameObject.CompareTag("Player"))
            {
                pd.killPlayer();
            }
            if (col.gameObject.CompareTag("Totem"))
            {
                anim["Anim_GreatAxeTrap_Play"].speed = 0;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //ContactPoint contact = col.contacts[0];
        //if (contact.normal == transform.forward)
        {
            // TODO add col.normal direction check
            if (col.gameObject.CompareTag("Totem"))
            {
                anim["Anim_GreatAxeTrap_Play"].speed = animSpeed;
            }
        }
    }
}
