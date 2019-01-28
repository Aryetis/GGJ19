using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneManager : MonoBehaviour
{
    //private static SceneSwitcher ss;

    void Start()
    {
        Invoke("LoadMainMenu", 6.0f);
        //ss.LoadScene("MainMenu");   
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
