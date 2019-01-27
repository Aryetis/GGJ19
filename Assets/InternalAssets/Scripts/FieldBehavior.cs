using System.Collections;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Outline outline = other.gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.cyan;
            outline.OutlineWidth = 5f;
        }
        if (other.GetComponent<TurretBehaviour>())
        {
            other.GetComponent<TurretBehaviour>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TurretBehaviour>())
        {
            other.GetComponent<TurretBehaviour>().enabled = true;
        }
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject.GetComponent<Outline>());
        }
    }
}
