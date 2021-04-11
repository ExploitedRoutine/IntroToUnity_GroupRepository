using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpsCollectible : MonoBehaviour
{

    [Header("PowerUp Paramter")]
    [SerializeField] 
    private float _speed = 2f;

    [SerializeField]
    private int extraLives = 1;

    //[SerializeField]
    public int manipulateSpeed = 5;
    
    [SerializeField] 
    private float _spinSpeed = 40f;

    private void Start()
    {
        //rotate addLife object by 90° 
        if (name.Contains("AddLive_Powerup"))
        {
            transform.Rotate(new Vector3(90f,0f,180f), relativeTo:Space.Self);
        }

        if (name.Contains("ScaleUp_Powerup"))
        {
            transform.Rotate(new Vector3(-90f,0f,0f), relativeTo:Space.Self);
        }

        if (name.Contains("Shield_Powerup"))
        {
            transform.Rotate(new Vector3(0f,180f,0f), relativeTo:Space.Self);
        }
        
    }

    // called once per frame
    void Update()
    {
        if (name.Contains("AddLive"))
        { 
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        if (name.Contains("ScaleUp_Powerup"))
        {
            transform.Translate(Vector3.back * (_speed * Time.deltaTime));
            transform.Rotate(new Vector3(0f, 0f, _spinSpeed * Time.deltaTime), Space.Self);
        }


        if (!name.Contains("AddLive")  && !name.Contains("ScaleUp_Powerup"))
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
            if (!name.Contains("Freeze_Powerup"))
            {
                transform.Rotate(new Vector3(0f, _spinSpeed * Time.deltaTime, 0f), Space.Self);
            }
        }
       
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            if (name.Contains("AddLive"))
            { 
                GameObject.FindWithTag("Player").GetComponent<Player>().AddLive(extraLives);  
                Destroy(this.gameObject); 
            }   
            
            if (name.Contains("UVLight_Powerup")) 
            { 
                Destroy(this.gameObject); 
                other.GetComponent<Player>().ActivatePowerUp(); 
            }
            
            if (name.Contains("SpeedUp_Powerup"))
            {
                // call public function from player 
                Destroy(this.gameObject); 
                other.GetComponent<Player>().SpeedUp(manipulateSpeed);
            }
            
            if (name.Contains("SlowDown_Powerup"))
            {
                Destroy(this.gameObject);
                other.GetComponent<Player>().SlowDown(manipulateSpeed);
            }
            
            if (name.Contains("Shield_Powerup"))
            {
                Destroy(this.gameObject);
                other.GetComponent<Player>().ActivateShield();
                
            }
            
            if (name.Contains("Freeze_Powerup"))
            {
                Destroy(this.gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>().FreezeCorona();
                Debug.Log("Collision detected");
            }

            if (name.Contains("ScaleUp_Powerup"))
            {
                Destroy(this.gameObject);
                other.GetComponent<Player>().ScaleUp();
            }
            
            //Random powerupfunction
            if (name.Contains("Random_Powerup"))
            {
                // random range from one to the amount of powerups we have 
                // havent figured out a way to make this integer a variable that depends on the number of powerups we have 
                // idea: we could make the list in the spawnmanager public and get the list length that way 
                // but then we couldnt decide which of these functions should be "inside" the crate. 
                // by extending the range eg 1, 8 we could also weight th chances a special powerup spawns
                int powerupIndex = Random.Range(1, 10);
                
                
                if (powerupIndex == 1)
                {
                    // UV light
                    Destroy(this.gameObject); 
                    other.GetComponent<Player>().ActivatePowerUp(); 
                }
                
                
                
                if (powerupIndex == 2)
                {
                    //add Live
                    GameObject.FindWithTag("Player").GetComponent<Player>().AddLive(extraLives);  
                    Destroy(this.gameObject); 
                }

                if (powerupIndex == 3)
                {
                    // speed up powerup
                    Destroy(this.gameObject); 
                    other.GetComponent<Player>().SpeedUp(manipulateSpeed);
                }
                
                if (powerupIndex == 4)
                {
                    // freeze corona powerup
                    Destroy(this.gameObject);
                    GameObject.FindWithTag("Player").GetComponent<Player>().FreezeCorona();
                }
                
                if (powerupIndex == 5)
                {
                    // activate shield powerup
                    Destroy(this.gameObject);
                    other.GetComponent<Player>().ActivateShield();
                }
                
                if (powerupIndex > 5 && powerupIndex < 9)
                {
                    //ScaleUp_powerup
                    Destroy(this.gameObject);
                    other.GetComponent<Player>().ScaleUp();
                }
                
                if (powerupIndex > 8)
                {
                    // slow down powerup
                    Destroy(this.gameObject);
                    other.GetComponent<Player>().SlowDown(manipulateSpeed);
                }
                
            }
        }
    }
}
