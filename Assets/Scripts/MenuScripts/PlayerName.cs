using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public string _nameOfPlayer;
    public string _saveName;

    public Text _inputText;
    public Text _savedName;


    private void Update()
    {
        _nameOfPlayer = PlayerPrefs.GetString("name", "Anonymous");
        _savedName.text = _nameOfPlayer;
    }

    public void SetName()
    {
        _saveName = _inputText.text;
        PlayerPrefs.SetString("name", _saveName);
    }
}
