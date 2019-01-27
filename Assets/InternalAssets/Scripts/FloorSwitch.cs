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
                    tiLinkedObj.ToggleOn();
            }
            else
                tiLinkedObj.ToggleOn();
        }
    }

    public void DecreaseInteractors()
    {
        --nbrOfInteractorOnButtons;
        if (nbrOfInteractorOnButtons <= 0)
        {
            tiLinkedObj.ToggleOff();
            toggled = false;
        }
    }
}
