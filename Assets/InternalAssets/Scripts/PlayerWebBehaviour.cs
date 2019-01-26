using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWebBehaviour : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceWithWeb = 20;

    private GameObject attachedPillar;
    private float initialSpeed;
    private CharacterController CC;

    private float webLife = 100f;

    // Start is called before the first frame update
    void Start()
    {
        attachedPillar = null;
        initialSpeed = GetComponent<PlayerMovement>().m_speed;
        GetComponentInChildren<LineRenderer>().enabled = false;
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(attachedPillar != null)
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            bool hasTakenDamage = false;
            if (Vector3.Distance(transform.position, attachedPillar.transform.position) >= maxDistanceWithWeb)
            {
                Vector3 desiredPosition = attachedPillar.transform.position - transform.position;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.015f);
                transform.position = smoothedPosition;
                hasTakenDamage = true;
                webLife -= 15;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, (attachedPillar.transform.position - transform.position), out hit) && hit.transform.gameObject != attachedPillar)
            {
                hasTakenDamage = true;
                webLife -= 5;
            }
            if (!hasTakenDamage)
            {
                webLife = Mathf.Min(100, webLife+2);
            }
            if(Input.GetKeyDown("joystick button 1") || webLife <= 0){
                detachPillar();
            }
            GetComponent<LineRenderer>().material.color = Color.Lerp(Color.red, Color.green, webLife/100);
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
        GetComponentInChildren<LineRenderer>().SetPosition(1, pillar.transform.position);
        GetComponentInChildren<LineRenderer>().enabled = true;
        webLife = 100;
    }

    public void detachPillar()
    {
        GetComponentInChildren<LineRenderer>().enabled = false;
        attachedPillar = null;
    }

}
