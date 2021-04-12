using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighscoreTransition : MonoBehaviour
{
    public GameObject _highscoreMenu;
    public GameObject _mainMenu;
    
    private void Awake()
    {
        Debug.Log("Sending player to Highscore table if points greater than 0");
        if (PlayerPrefs.GetInt("highscore") != 0)
        {
            _highscoreMenu.SetActive(true);
            _mainMenu.SetActive(false);
        }
        
    }
    
}
