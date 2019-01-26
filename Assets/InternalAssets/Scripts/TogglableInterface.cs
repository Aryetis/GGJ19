using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TogglableInterface : MonoBehaviour
{
    public abstract void ToggleOn(); // Called whenever the button turns on

    public abstract void ToggleOff();  // Called whenever the button turns off
}
