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
        Debug.Log("Checking whether game can be closed. Seeing this means it works :P");
        Application.Quit();
    }

    
}
