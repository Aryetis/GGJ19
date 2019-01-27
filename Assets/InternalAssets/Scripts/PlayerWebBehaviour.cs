using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWebBehaviour : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceWithWeb = 20;

    [SerializeField]
    private float stringMoveSpeed = 0.5f;

    [SerializeField]
    private float damageTooFar = 15;

    [SerializeField]
    private float damageObstructed = 7;

    private GameObject attachedPillar;
    private GameObject attachedBox;
    private float initialSpeed;
    private CharacterController CC;
    private Side attachedSide=Side.None;

    private Vector3 target;
    private bool isBoxMoving = false;

    private float webLife = 100f;

    // Start is called before the first frame update
    void Start()
    {
        attachedPillar = null;
        initialSpeed = GetComponent<PlayerMovement>().m_speed;
        GetComponentInChildren<LineRenderer>().enabled = false;
        CC = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isBoxMoving)
        {
            moveBoxTowardPillar(0);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(attachedPillar != null)
        {
            if (!isBoxMoving)
            {
                GetComponent<LineRenderer>().SetPosition(0, transform.position);
            }
            else
            {
                GetComponent<LineRenderer>().SetPosition(0, attachedBox.transform.position);
            }
            bool hasTakenDamage = false;
            if (Vector3.Distance(transform.position, attachedPillar.transform.position) >= maxDistanceWithWeb)
            {
                Vector3 desiredPosition = attachedPillar.transform.position - transform.position;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.015f);
                transform.position = smoothedPosition;
                hasTakenDamage = true;
                Debug.Log("damageToofar");
                webLife -= damageTooFar;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, (attachedPillar.transform.position - transform.position), out hit) && hit.transform.gameObject != attachedPillar)
            {
                Debug.Log(hit.collider.gameObject);
                hasTakenDamage = true;
                webLife -= damageObstructed;
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

    public void attachPillar(GameObject pillar, Side side)
    {
        if(pillar.GetComponent<WebPillarBehaviour>() == null)
        {
            Debug.Log("ERROR ! Expecting a pillar");
        }
        attachedPillar = pillar;
        GetComponentInChildren<LineRenderer>().SetPosition(1, pillar.transform.position);
        GetComponentInChildren<LineRenderer>().enabled = true;
        webLife = 100;
        attachedSide = side;
        GetComponent<DisplayHelp>().hideHelp('Y');
    }

    public void detachPillar()
    {
        if (!isBoxMoving)
        {
            GetComponentInChildren<LineRenderer>().enabled = false;
            attachedPillar = null;
            attachedSide = Side.None;
        }
    }

    public bool canAttachBox(Side side)
    {
        if (getAttachedPillar() == null)
            return false;
        switch (attachedSide)
        {
            case Side.Left:
                if (side == Side.Right)
                {
                    return true;
                }
                break;
            case Side.Right:
                if (side == Side.Left)
                {
                    return true;
                }
                break;
            case Side.Front:
                if (side == Side.Back)
                {
                    return true;
                }
                break;
            case Side.Back:
                if (side == Side.Front)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public void attachBox(GameObject box, Side side)
    {
        switch (attachedSide)
        {
            case Side.Left:
                if(side == Side.Right)
                {
                    startMovingBox(box);
                }
                break;
            case Side.Right:
                if(side == Side.Left){
                    startMovingBox(box);
                }
                break;
            case Side.Front:
                if(side == Side.Back)
                {
                    startMovingBox(box);
                }
                break;
            case Side.Back:
                if(side == Side.Front)
                {
                    startMovingBox(box);
                }
                break;
                
        }
    }

    private void startMovingBox(GameObject box)
    {
        attachedBox = box;
        foreach (Transform pillarTrigger in attachedPillar.transform)
        {
            if (pillarTrigger.GetComponent<TriggerWebScript>().triggerSide == attachedSide)
            {
                this.target = pillarTrigger.position;
                isBoxMoving = true;
                attachedBox.GetComponent<Collider>().enabled = false;
                break;
            }
         }

    }

    private void moveBoxTowardPillar(float progress)
    {
        attachedBox.transform.position = Vector3.MoveTowards(attachedBox.transform.position, target, stringMoveSpeed);
        if(attachedBox.transform.position == target)
        {
            attachedBox.GetComponent<Collider>().enabled = true;
            attachedBox = null;
            isBoxMoving = false;
            detachPillar();
        }
    }
}
