using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.DeleteKey("highscore");
        SceneManager.LoadScene("Prototype");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
