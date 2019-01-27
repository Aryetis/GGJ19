using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelBox : MonoBehaviour
{
    public string sceneNameToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Totem"))
        {
            // TODO fate out anim
            other.gameObject.GetComponent<TotemBehavior>().enabled = false;
            Invoke("LoadNextLevel", 2.0f);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            // TODO print UI message
            Debug.Log("plz gimme totem");
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }

}
