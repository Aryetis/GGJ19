using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    GameObject tb;
    PlayerMovement pm;

    private void Start()
    {
        tb = GameObject.Find("Totem");
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyUp("joystick button 6") || Input.GetKeyDown(KeyCode.Escape))
        {
            reloadScene();
        }
    }
    public void killPlayer()
    {
        transform.Find("CharaVisu").gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        pm.enabled = false;
        tb.GetComponent<TotemBehavior>().enabled = false;
        Invoke("reloadScene", 3);
    }

    public void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void killTotem()
    {
        tb.GetComponent<TotemBehavior>().enabled = false;
        pm.enabled = false;
        Invoke("reloadScene", 3);
    }
}
