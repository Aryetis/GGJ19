using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadContinue()
    {

    }

    public void LoadOptions()
    {

    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
