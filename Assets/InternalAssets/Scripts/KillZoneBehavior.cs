using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneBehavior : TogglableInterface
{
    private static PlayerDeath pd;
    private bool armed = true;

    void Start()
    {
        pd = GameObject.Find("Player").GetComponent<PlayerDeath>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (armed && (col.gameObject.CompareTag("Player")))
        {
            pd.killPlayer();
        }
        else if (armed && gameObject.CompareTag("Totem") && TotemBehavior.PlayerFusioned)
        {
            pd.killTotem();
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
