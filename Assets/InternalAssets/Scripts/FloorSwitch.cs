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

    private TogglableInterface tiLinkedObj;
    private int nbrOfInteractorOnButtons = 0;
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
    }

    private void OnTriggerEnter(Collider col)
    {
        if (switchType == SwitchType.Toggle || switchType == SwitchType.OneWay)
        {
            if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Totem")
                || col.gameObject.CompareTag("MovableBlock"))
            {
                // TODO anim button
                IncreaseInteractors();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (switchType == SwitchType.Toggle)
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
            tiLinkedObj.ToggleOn();
            toggled = true;
        }
Debug.Log("Increased nbrOfInteractorOnButtons to : " + nbrOfInteractorOnButtons);
    }

    public void DecreaseInteractors()
    {
        --nbrOfInteractorOnButtons;
        if (nbrOfInteractorOnButtons <= 0)
        {
            tiLinkedObj.ToggleOff();
            toggled = false;
        }
Debug.Log("Decreased nbrOfInteractorOnButtons to : " + nbrOfInteractorOnButtons);
    }
}
