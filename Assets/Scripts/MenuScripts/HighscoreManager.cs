using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    private Transform _entryContainer;
    private Transform _entryTemplate;
    

    private void Awake()
    {
        _entryContainer = transform.Find("ScoreTemplateContainer");
        _entryTemplate = transform.Find("ScoreTemplate");
        
        _entryTemplate.gameObject.SetActive(false);
        
        float templateHeight = 30f;
        
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(_entryTemplate, _entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
        }
    }
}
