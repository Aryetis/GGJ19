using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWebBehaviour : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceWithWeb = 20;

    private GameObject attachedPillar;
    private float initialSpeed;

    // Start is called before the first frame update
    void Start()
    {
        attachedPillar = null;
        initialSpeed = GetComponent<PlayerMovement>().m_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, attachedPillar.transform.position) >= maxDistanceWithWeb)
        {
            transform.position = transform.position - (attachedPillar.transform.position - transform.position);
        }
    }

    public GameObject getAttachedPillar()
    {
        return attachedPillar;
    }

    public void attachPillar(GameObject pillar)
    {
        if(pillar.GetComponent<WebPillarBehaviour>() == null)
        {
            Debug.Log("ERROR ! Expecting a pillar");
        }
        attachedPillar = pillar;
    }

    public void detachPillar()
    {
        attachedPillar = null;
    }

}
