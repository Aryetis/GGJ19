using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    private enum SwitchType
    {
        Toggle, OneWay, ToggleCombined
    }

    [SerializeField] private GameObject GameObjectLinkedToAction;
    [SerializeField] private GameObject linkedButton;
    [SerializeField] private SwitchType switchType;
    [SerializeField] private Material turnedOnMat, turnedOffMat;
    //private Renderer interrupteurRenderer;

    public bool Toggled
    {
        get { return toggled; }
        private set { toggled = value; }
    }

    private TogglableInterface tiLinkedObj;
    private int nbrOfInteractorOnButtons = 0;
    private FloorSwitch linkedFloorSwitch;
    private bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
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
        if (switchType == SwitchType.ToggleCombined)
        {
            if (linkedButton == null)
            {
                Debug.Log("Floor switch :" + gameObject.name + " is of type ToggleCombined yet not linked to another floor switch");
                Debug.Break();
            }
            linkedFloorSwitch = linkedButton.GetComponent<FloorSwitch>();
        }
        if (turnedOffMat == null || turnedOnMat == null)
        {
            Debug.Log("Set turnedOn turnedOff materials for :" + gameObject.name);
            Debug.Break();
        }

        //interrupteurRenderer = transform.Find("Interupteur").GetComponent<Renderer>();
        //interrupteurRenderer.material = turnedOffMat;
    }

private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Totem")
            || col.gameObject.CompareTag("MovableBlock"))
        {
            // TODO anim button
            IncreaseInteractors();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (switchType == SwitchType.Toggle || switchType == SwitchType.ToggleCombined)
        {
            if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Totem")
            || col.gameObject.CompareTag("MovableBlock"))
            {
                // TODO anim button
                DecreaseInteractors();
            }
        }
    }

    public void IncreaseInteractors()
    {
        ++nbrOfInteractorOnButtons;
        if (!toggled)
        {
            toggled = true;
            if (switchType == SwitchType.ToggleCombined)
            {
                if (linkedFloorSwitch.Toggled)
                {
                    //GetComponent<Renderer>().material.SetColor("Albedo", Color.green);
                    //interrupteurRenderer.material = turnedOnMat;
                    GetComponent<Renderer>().material = turnedOnMat;
                    tiLinkedObj.ToggleOn();
                }
            }
            else
            {
                GetComponent<Renderer>().material = turnedOnMat;
                //GetComponent<Renderer>().material.SetColor("Albedo", Color.green);
                tiLinkedObj.ToggleOn();
            }
        }
    }

    public void DecreaseInteractors()
    {
        --nbrOfInteractorOnButtons;
        if (nbrOfInteractorOnButtons <= 0)
        {
            tiLinkedObj.ToggleOff();
            GetComponent<Renderer>().material = turnedOffMat;
            //GetComponent<Renderer>().material.SetColor("Albedo", Color.red);// = turnedOffMat;
            toggled = false;
        }
    }
}
