using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public GameObject _rank;
    public GameObject _name;
    public GameObject _highscore;

    public void SetScore(int rank, string name, int highscore)
    {
        SetName(name);
        SetRank(rank);
        SetHighscore(highscore);
    }
    
    public void SetName( string name)
    {
        _name.GetComponent<TextMeshProUGUI>().text = name;
    }
    
    public void SetRank(int rank)
    {
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "th";
                break;
            case 1:
                rankString = "1st";
                break;
            case 2:
                rankString = "2nd";
                break;
            case 3:
                rankString = "3rd";
                break;
        }
        _rank.GetComponent<TextMeshProUGUI>().text = rankString;
    }
    
    public void SetHighscore(int highscore)
    {
        _highscore.GetComponent<TextMeshProUGUI>().text = highscore.ToString(); 
    }
    
}
