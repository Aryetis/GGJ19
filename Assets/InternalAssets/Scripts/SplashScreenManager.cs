using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    private static SceneSwitcher ss;

    void Start()
    {
        ss.LoadScene("MainMenu");   
    }
}
