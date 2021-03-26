using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //this currently loads our game scene. if we create more levels we will need rework this part
        SceneManager.LoadScene("Prototype");
    }

    public void QuitGame()
    {
        Debug.Log("Checking whether game can be closed. Seeing this means it works :P");
        Application.Quit();
    }
}
