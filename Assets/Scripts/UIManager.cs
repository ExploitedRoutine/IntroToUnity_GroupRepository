using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score = 0;
    private int _health = 3;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _healthText;

    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Health: " + _health;
    }

    public void UpdateHealth(int health)
    {
        Color healthcolor = Color32.Lerp(Color.red, Color.green, Mathf.Clamp01(health));
        _healthText.color = healthcolor;
        _healthText.text = "Health: " + health;
    }
    
    public void ShowGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }
    
}
