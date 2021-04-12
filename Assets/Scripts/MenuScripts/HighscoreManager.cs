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
        PlayerPrefs.GetString("name");
        PlayerPrefs.GetInt("highscore");
        Debug.Log("Last Player was " + PlayerPrefs.GetString("name") + " with a score of" + PlayerPrefs.GetInt("highscore"));
    }

    private void Awake()
    {
        
        for (int i = 0; i < 10; i++)
        {
            GameObject entryTransform = Instantiate(_entryTemplate, _entryContainer.transform);
            entryTransform.gameObject.SetActive(true);

            // remove placeholder from list when loaded

            int rank = i + 1;
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

            GameObject.Find("Rank_pos").GetComponent<TextMeshProUGUI>().text = rankString;

        }
    }
}
