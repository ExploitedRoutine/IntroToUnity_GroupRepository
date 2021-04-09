using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    [SerializeField] 
    private GameObject _shieldPrefab;

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
    
    
    //used for player spin/rotation, not functional atm
    //[SerializeField]
    //private float _spinSpeed = 6;
    
    [Header("PowerUp Settings")]
    [SerializeField]
    private bool _isUVOn = false;

    [SerializeField]
    public bool _isShieldOn = false;
    
    
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
       // LoadMenuScene();
    }


    /*private void LoadMenuScene()
    {
        if (_lives <= 0)
        {
            StartCoroutine("BackToMenu");
        }
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Menu");
    }*/
    
    // player damage
    public void Damage()
    {
        //reduce _lives by one
        _lives -= 1;
        
        
        _uiManager.UpdateHealth(_lives);
        if (_lives <= 0)
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
    
   

    
    
    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);
    }
    
    // define player movement
    void PlayerMovement()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");
       
       
       //transform.Rotate(new Vector3(0f,horizontalInput* _spinSpeed * Time.deltaTime, 0f), Space.Self);
       
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
            //transform.rotation = Quaternion.identity;
        }
        else if (transform.position.x > 6.5f)
        {
            transform.position = new Vector3(-6.5f, transform.position.y, 0);
            //transform.rotation = Quaternion.identity;
        }
    }


    public void ActivateShield()
    {
        //shieldPrefab does not yet exist
        Debug.Log("ActivateShield is called ");
        _isShieldOn = true;
        Instantiate(_shieldPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity, this.gameObject.transform);  
        //_lives++;
        //_uiManager.UpdateHealth(_lives);
        
    }
    
    
    // Here start the PowerUp functions
    
    // add live with first aid kit
    public void AddLive(int extraLives)
    {
        Debug.Log("addlives called");
        _lives += extraLives;
        _uiManager.UpdateHealth(_lives);
        
    }
    
    // for now this is the one with the crate and the UV light
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

    
    // speed up - Coffee cup
    public void SpeedUp(int manipulateSpeed)
    {
        StartCoroutine(DeactivateSpeedUp());
        _speed += manipulateSpeed;
        
        
        
    }
   
    IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(_powerUpTimeout);
        _speed -= GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed;
        //_speed -= gameObject.GetComponent<PowerUpsCollectible>().manipulateSpeed;
        //problem remains this debug gives out that increased speed is 5 and not 7
        //could be fixed by attaching another gameobject to this script but not a nice solution
        
        Debug.Log("increasedSpeed = " + GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed);
    }

    public void SlowDown(int manipulateSpeed)
    {
        StartCoroutine(DeactivateSlowDown());
        _speed -= manipulateSpeed;
        if (_speed < 0)
        {
            _speed = 0;
        }
    }
    IEnumerator DeactivateSlowDown()
    {
        yield return new WaitForSeconds(_powerUpTimeout);
        _speed += GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed;
        Debug.Log("manipulateSpeed = " + GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed);
    }
}
