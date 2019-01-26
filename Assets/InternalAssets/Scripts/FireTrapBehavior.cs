using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapBehavior : MonoBehaviour
{
    private static PlayerDeath pd;

    void Start()
    {
        pd = GameObject.Find("Player").GetComponent<PlayerDeath>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player")
            || col.gameObject.CompareTag("Totem") && TotemBehavior.PlayerFusioned )
        {
            pd.killPlayer();
        }
    }
}
