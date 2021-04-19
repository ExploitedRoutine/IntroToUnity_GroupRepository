using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    
    [Header("UI Text")]
    [SerializeField] 
    private Text _scoreText;
    [SerializeField] 
    private Text _gameOverText;
    [SerializeField]
    private Text _healthText;

    [SerializeField] public string _saveNameAndScore;

    //Values that are shown in the beginning
    private int _score = 0;
    private int _health = 3;
    public float waitTime = 3f;
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Health: " + _health;
        Debug.Log("hello" + PlayerPrefs.GetString("name"));
    }

    public void UpdateHealth(int health)
    {
        Color healthcolor = Color32.Lerp(Color.red, Color.green, Mathf.Clamp01(health));
        _healthText.color = healthcolor;
        _healthText.text = "Health: " + health;

        if (health <= 0)
        {
            PlayerPrefs.SetInt("highscore", _score);
            Debug.Log("Hey " + PlayerPrefs.GetString("name") + " you reached points: "+ _score);
            StartCoroutine(BackToMenu(waitTime));
        }
    }

    IEnumerator BackToMenu(float waitTime)
    {
        Debug.Log("Going back to menu in 3s...");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Menu");
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
