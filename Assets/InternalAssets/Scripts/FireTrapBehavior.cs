using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapBehavior : TogglableInterface
{
    private static PlayerDeath pd;
    private bool armed = true;
    private GameObject fire;

    void Start()
    {
        pd = GameObject.Find("Player").GetComponent<PlayerDeath>();
        fire = transform.parent.Find("Eff_Fire").gameObject;
    }

    private void OnTriggerEnter(Collider col)
    {
        if ( armed && col.gameObject.CompareTag("Player") )
        {
            pd.killPlayer();
        }
        else if (armed && col.gameObject.CompareTag("Totem") && TotemBehavior.PlayerFusioned)
        {
            pd.killTotem();
        }
    }

    public override void ToggleOff()
    {
        armed = true;
        fire.SetActive(true);
    }

    public override void ToggleOn()
    {
        armed = false;
        fire.SetActive(false);
    }
}
