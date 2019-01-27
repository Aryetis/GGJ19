using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelBox : MonoBehaviour
{
    public string sceneNameToLoad;

    private GameObject textBox;

    private void Start()
    {
        textBox = GameObject.Find("TextBoxInfo");
    }

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
            textBox.GetComponent<TextBoxBehaviour>().displayText("Don't leave without your totem !", 3);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }

}
