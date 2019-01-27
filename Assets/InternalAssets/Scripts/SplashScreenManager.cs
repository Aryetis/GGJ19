using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    //private static SceneSwitcher ss;

    void Start()
    {
        SceneManager.LoadScene("MainMenu");
        //ss.LoadScene("MainMenu");   
    }
}
