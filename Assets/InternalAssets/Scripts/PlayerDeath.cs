using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    TotemBehavior tb;

    private void Start()
    {
        tb = GameObject.Find("Totem").GetComponent<TotemBehavior>();
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
        GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(ReloadSceneAfterTime(3));
    }

    public void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void killTotem()
    {
        tb.enabled = false;
        StartCoroutine(ReloadSceneAfterTime(3));
    }

    IEnumerator ReloadSceneAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
