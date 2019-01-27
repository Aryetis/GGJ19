using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    private enum SwitchType
    {
        Toggle, OneWay, ToggleCombined, OneWayCombined
    }

    [SerializeField] private GameObject GameObjectLinkedToAction;
    [SerializeField] private GameObject linkedButton;
    [SerializeField] private SwitchType switchType;
    [SerializeField] private Material turnedOnMat, turnedOffMat;

    public bool localstate
    {
        get { return m_localstate; }
        private set
        {
            m_localstate = value;
            GetComponent<Renderer>().material = (value) ? turnedOnMat : turnedOffMat;
        }
    }
    [HideInInspector] public bool combinedState = false;

    private TogglableInterface tiLinkedObj;
    private int nbrOfInteractorOnButtons;
    private FloorSwitch linkedFloorSwitch;
    private bool m_localstate = false;
    private int cpt;

    // Start is called before the first frame update
    void Start()
    {
        combinedState = false;
        m_localstate = false;
        cpt = 0;

        // Get linked object
        tiLinkedObj = GameObjectLinkedToAction.GetComponent<TogglableInterface>();
        if (tiLinkedObj == null)
        {
            tiLinkedObj = GameObjectLinkedToAction.GetComponentInChildren<TogglableInterface>();
        }
        if (tiLinkedObj == null)
        {
            Debug.Log("Floor switch :" + gameObject.name + " not linked to a TogglableInterface");
            Debug.Break();
        }

        // Get linked switch
        if (switchType == SwitchType.ToggleCombined || switchType == SwitchType.OneWayCombined)
        {
            if (linkedButton == null)
            {
                Debug.Log("Floor switch :" + gameObject.name + " is of type ToggleCombined yet not linked to another floor switch");
                Debug.Break();
            }
            linkedFloorSwitch = linkedButton.GetComponent<FloorSwitch>();
        }

        // Get materials
        if (turnedOffMat == null || turnedOnMat == null)
        {
            Debug.Log("Set turnedOn turnedOff materials for :" + gameObject.name);
            Debug.Break();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Totem")
       || col.gameObject.CompareTag("MovableBlock"))
            IncreaseInteractors();
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Totem")
       || col.gameObject.CompareTag("MovableBlock"))
            DecreaseInteractors();
    }

    public void IncreaseInteractors()
    {
        switch (switchType)
        {
            case SwitchType.Toggle :
            {
                cpt++;
                if (!localstate)
                {
                    localstate = true;
                    tiLinkedObj.ToggleOn();
                }
                break;
            }
            case SwitchType.OneWay:
            {
                if(!localstate)
                {
                    localstate = true;
                    tiLinkedObj.ToggleOn();
                }
                break;
            }
            case SwitchType.ToggleCombined:
            {
                cpt++;
                localstate = true;
                if (linkedFloorSwitch.localstate && !combinedState)
                {
                    combinedState = true;
                    linkedFloorSwitch.combinedState = true;
                    tiLinkedObj.ToggleOn();
                }
                break;
            }
            case SwitchType.OneWayCombined:
            {
                if (!localstate)
                {
                    localstate = true;
                    if (linkedFloorSwitch.localstate && !combinedState)
                    {
                        combinedState = true;
                        linkedFloorSwitch.combinedState = true;
                        tiLinkedObj.ToggleOn();
                    }
                }
                break;
            }
            default: break;
        }
    }

    public void DecreaseInteractors()
    {
        switch (switchType)
        {
            case SwitchType.Toggle:
            {
                cpt--;
                if (cpt <= 0)
                {
                    localstate = false;
                    tiLinkedObj.ToggleOff();
                }
                break;
            }
            case SwitchType.OneWay:
            { break; }
            case SwitchType.ToggleCombined:
            {
                cpt--;
                if (cpt <= 0)
                {
                    localstate = false;
                    if (combinedState)
                    {
                        combinedState = false;
                        linkedFloorSwitch.combinedState = false;
                        tiLinkedObj.ToggleOff();
                    }
                }
                break;
            }
            case SwitchType.OneWayCombined:
            {
                cpt--;
                if (!combinedState)
                {
                    if(cpt <= 0)
                    {
                        localstate = false;
                    }
                }
                break;
            }
            default: break;
        }
    }
}
