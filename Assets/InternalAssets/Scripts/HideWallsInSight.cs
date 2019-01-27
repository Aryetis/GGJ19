using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWallsInSight : MonoBehaviour
{
    public Material transparentMaterial;

    // <original, cloneGhosted>
    private HashSet<KeyValuePair<GameObject, Material>> hiddenGameObjects;

    void Start()
    {
        hiddenGameObjects = new HashSet<KeyValuePair<GameObject, Material>>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<GameObject, Material> pair in hiddenGameObjects)
        {
            pair.Key.GetComponent<Renderer>().material = pair.Value;
        }

        // Search for new walls to hide
        RaycastHit[] hitinfos;
        hitinfos = Physics.RaycastAll(transform.position, Camera.main.transform.position - transform.position, 200.0f);
        foreach (RaycastHit hit in hitinfos)
        {
            // Save it
            Renderer hitRenderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (hitRenderer == null)
                continue;
            KeyValuePair<GameObject, Material> gameObjectOriginalMat =
                new KeyValuePair<GameObject, Material>(hit.collider.gameObject, hitRenderer.material);
            hiddenGameObjects.Add(gameObjectOriginalMat);

            // Alter it
            hitRenderer.material = transparentMaterial;
        }
    }
}
