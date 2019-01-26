﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBehavior : MonoBehaviour
{
    [Range(0,1)] public float RefillPerSecond;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Energy.AddEnergy(RefillPerSecond * Time.fixedDeltaTime);
        }
    }
}
