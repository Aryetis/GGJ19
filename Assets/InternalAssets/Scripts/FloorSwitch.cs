using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    private enum SwitchType
    {
        Toggle, Hold, Multiple
    }
    private enum ActionType
    {
        OpenDoor, TurnOff
    }

    [SerializeField] private SwitchType switchType;
    [SerializeField] private ActionType actionType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
