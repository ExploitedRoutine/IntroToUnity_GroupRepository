using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public GameObject _entryContainer;
    public GameObject _entryTemplate;
    

    private void Awake()
    {
        
        float templateHeight = 20f;
        
        for (int i = 0; i < 10; i++)
        {
            GameObject entryTransform = Instantiate(_entryTemplate, _entryContainer.transform);
            entryTransform.gameObject.SetActive(true);
        } 
    }
}
