using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    public GameObject _entryContainer;
    public GameObject _entryTemplate;

    private void Start()
    {
        AddToHighscore(PlayerPrefs.GetInt("highscore"),PlayerPrefs.GetString("name"));
        ParseHighscore();


    }


    private void ParseHighscore()
    {
        
        
        string highscoreList = PlayerPrefs.GetString("highscorelist");
        string[] highscoreEntryList = highscoreList.Split(';');
    
        List<string> topTen = new List<string>();
        foreach (string highscoreEntry in highscoreEntryList)
        {
            if (highscoreEntry == "")
            {
                continue;
            }
            string[] nameAndScore = highscoreEntry.Split(':');
            
            int index = 0;
            for (index = 0; index < topTen.Count; index++)
            {
                if (index == 10)
                {
                    break;
                }
                string[] topTenNameAndScore = topTen[index].Split(':');
                
                if (int.Parse(topTenNameAndScore[1]) < int.Parse(nameAndScore[1]))
                {
                    break;
                }
            }

            if (index < 10)
            {
                topTen.Insert(index, highscoreEntry);
            }
        }

        int placement = 1;
        foreach (string topTenEntry in topTen)
        {
            GameObject score = Instantiate(_entryTemplate, _entryContainer.transform);
            string[] topTenNameAndScore = topTenEntry.Split(':');
            score.GetComponent<score>().SetScore(placement, topTenNameAndScore[0], int.Parse(topTenNameAndScore[1]));
            placement++;

            if (placement >= 11)
            {
                break;
            }

        }
        
    }

    private void AddToHighscore(int highscore, string playername)
    {
        if (PlayerPrefs.GetString("highscorelist") != null)
        {
            PlayerPrefs.SetString("highscorelist", PlayerPrefs.GetString("highscorelist") + ";" + playername + ":" + highscore); 
        }
        else
        {
            PlayerPrefs.SetString("highscorelist", playername + ":" + highscore);
        }
        
        PlayerPrefs.DeleteKey("name");
        PlayerPrefs.DeleteKey("highscore");
        
    }

}
