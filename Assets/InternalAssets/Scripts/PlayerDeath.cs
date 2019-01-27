using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    TotemBehavior tb;
    PlayerMovement pm;

    private void Start()
    {
        tb = GameObject.Find("Totem").GetComponent<TotemBehavior>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyUp("joystick button 6"))
        {
            reloadScene();
        }
    }
    public void killPlayer()
    {
        transform.Find("CharaVisu").gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        pm.enabled = false;
        tb.enabled = false;
        Invoke("reloadScene", 3);
    }

    public void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void killTotem()
    {
        tb.enabled = false;
        pm.enabled = false;
        Invoke("reloadScene", 3);
    }
}
