using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    
    [SerializeField]
    private float _speed = 7f;
    
    [Header("External Components")]
    [SerializeField] 
    private GameObject _uvLightPrefab;

    [SerializeField] 
    private GameObject _vaccinePrefab;

    //[SerializeField]
    //private GameObject _addLivePrefab; 
    
    [SerializeField] 
    private SpawnManager _spawnMananger;

    [Header("Vaccine Parameters")]
    [SerializeField] 
    private float _vaccinationRate = 0.3f;

    [SerializeField] 
    private float _powerUpTimeout = 5f;

    [SerializeField] 
    private GameObject _vaccines;
    
    [Header("Player Settings")]
    [SerializeField] 
    private int _lives = 3;


    
    
    private float _canVaccinate = -1f;
    [Header("PowerUp Settings")]
    [SerializeField]
    private bool _isUVOn = false;

    
    
    //[SerializeField] private bool _addLive = false;
    
    
    
    
    // called before  first frame update
    void Start()
    {
        _isUVOn = false;
        
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // called once per frame
    void Update()
    {
        //call important functions!
       PlayerMovement();
       PlayerBoundaries();
       Vaccinate();
    }

    // player damage
    public void Damage()
    {
        //reduce _lives by one
        _lives -= 1;
        _uiManager.UpdateHealth(_lives);
        if (_lives == 0)
        {
            if (_spawnMananger != null)
            {
                _spawnMananger.onPlayerDeath();
            }
            else
            {
                Debug.LogError("assign the spawn manager! else null reference error...");
            }
            _uiManager.ShowGameOver();
            Destroy(this.gameObject);
            Destroy(_spawnMananger.gameObject);
        }
    }
    
    public void AddLive(int extraLives)
    {
        Debug.Log("addlives called");
        _lives += extraLives;
        _uiManager.UpdateHealth(_lives);
        
    }
    
    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);
    }
    
    // define player movement
    void PlayerMovement()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");
       
       Vector3 playerTranslate = new Vector3(1f * horizontalInput * _speed * Time.deltaTime, 0f, 1f * verticalInput * _speed * Time.deltaTime);
        transform.Translate(playerTranslate);
    }

    // on space "shoot" vaccine or UV light (currently) 
    void Vaccinate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canVaccinate)
        {
            _canVaccinate = Time.time + _vaccinationRate;
            // assign time of execution and vaccRate to canVaccinate
            if (!_isUVOn)
            {
                Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, _vaccines.transform);   
            }
            else
            {
                Instantiate(_uvLightPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, _vaccines.transform);
            } 

        }
    }
    
    // setting screen boundary parameters
    void PlayerBoundaries()
    {
        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, 0f);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }
        if (transform.position.x < -6.5f)
        {
            transform.position = new Vector3(6.5f, transform.position.y, 0);
        }
        else if (transform.position.x > 6.5f)
        {
            transform.position = new Vector3(-6.5f, transform.position.y, 0);
        }
    }

    public void ActivatePowerUp()
    {
        
        _isUVOn = true;
        StartCoroutine(DeactivatePowerUp());
    }

    IEnumerator DeactivatePowerUp()
    {
        yield return new WaitForSeconds(_powerUpTimeout);
        _isUVOn = false;
    }
}
