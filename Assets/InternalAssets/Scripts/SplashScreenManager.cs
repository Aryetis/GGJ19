using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    //private static SceneSwitcher ss;

    void Start()
    {
        Invoke("LoadMainMenu", 4.0f);
        //ss.LoadScene("MainMenu");   
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
