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
        // if score > 0 transition to highscore screen
        if (PlayerPrefs.GetInt("highscore") != 0)
        {
            _highscoreMenu.SetActive(true);
            _mainMenu.SetActive(false);
            
        }
        
    }
    
}
