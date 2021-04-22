using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
  
 
    
    [Header("External Components")]
    [SerializeField]
    private UIManager _uiManager;
    
    [SerializeField]
    private PowerUpsCollectible _powerUpsCollectible;
    
    [SerializeField] 
    private GameObject _uvLightPrefab;

    [SerializeField] 
    private GameObject _vaccinePrefab;

    [SerializeField] 
    private GameObject _shieldPrefab;
    
    [SerializeField] 
    private SpawnManager _spawnMananger;

    [SerializeField] 
    private GameObject _vaccines;
    
    [Header("Vaccine Parameters")]
    [SerializeField] 
    private float _vaccinationRate = 0.3f;

   

  
    
    [Header("Player Settings")]
    [SerializeField] 
    private int _lives = 3;
    
    private float _canVaccinate = -1f;
    
    [SerializeField]
    private float _speed = 7f;
    
    
    [Header("PowerUp Settings")]
    [SerializeField]
    private bool _isUVOn = false;

    [SerializeField]
    public bool _isShieldOn = false;
    
    [SerializeField] 
    public bool _freezeCorona = false;
    
    private Vector3 scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
    
    [SerializeField] 
    private float _powerUpTimeout = 5f;
    
    
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

    
    
    // Here start the Player Settings Functions
    
    //define behaviour when Player is damaged
    public void Damage(int damageInt)
    {
        //reduce _lives by one
        _lives -= damageInt;
        
        
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
    
    // relays the current score to the UI managers
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

    
    // Here start the PowerUp functions

    // activate shield with shield powerup
    public void ActivateShield()
    {
        _isShieldOn = true;
        Instantiate(_shieldPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity, this.gameObject.transform);
    }
    
    
    // add live with first aid kit
    public void AddLive(int extraLives)
    {
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
    public void SpeedUp(float manipulateSpeed)
    {
        StartCoroutine(DeactivateSpeedUp());
        _speed += manipulateSpeed;
        
    }
   
    IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(_powerUpTimeout);
        //_speed -= GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed;
        //_speed -= gameObject.GetComponent<PowerUpsCollectible>().manipulateSpeed;
        //problem remains this debug gives out that increased speed is 5 and not 7
        //could be fixed by attaching another gameobject to this script but not a nice solution
        _speed -= _powerUpsCollectible.manipulateSpeed;
        Debug.Log("increasedSpeed = " + GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed);
    }

    //slows down the player by manipulate speed.     
    public void SlowDown(float manipulateSpeed)
    {
        StartCoroutine(DeactivateSlowDown());
        _speed -= manipulateSpeed;
        
        if (_speed < 0)
        {
            _speed = 2;
        }
        
    }
    IEnumerator DeactivateSlowDown()
    {
        //Debug.Log("manipulateSpeed = " + GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed);
        yield return new WaitForSeconds(_powerUpTimeout);
        _speed += _powerUpsCollectible.manipulateSpeed;
        //GameObject.FindWithTag("Powerup").GetComponent<PowerUpsCollectible>().manipulateSpeed;

    }
    
    //freezes all the corona viruses in mid air 
    public void FreezeCorona()
    {
        _freezeCorona = true;
        StartCoroutine(StopFreezeCorona());
    }

    IEnumerator StopFreezeCorona()
    {
        yield return new WaitForSeconds(_powerUpTimeout);
        _freezeCorona = false;
    }

    //lets the player double its size
    public void ScaleUp()
    {
        StartCoroutine(DeactivateScaleUp());
        transform.localScale += scaleChange;
    }

    IEnumerator DeactivateScaleUp()
    {
        yield return new WaitForSeconds(_powerUpTimeout);
        transform.localScale -= scaleChange;
    }
    
}
