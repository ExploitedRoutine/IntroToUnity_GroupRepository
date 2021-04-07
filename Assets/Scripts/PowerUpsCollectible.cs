using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsCollectible : MonoBehaviour
{
    [Header("PowerUp Paramter")]
    [SerializeField] 
    private float _speed = 2f;

    [SerializeField]
    private int extraLives = 1;
    
    [SerializeField] 
    private float _spinSpeed = 20f;
    
    // called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        
        if (name.Contains("AddLive") ||  name.Contains("UV Powerup"))
        {
            transform.Rotate(new Vector3(0f, _spinSpeed * Time.deltaTime, 0f), Space.Self);
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
            if (name.Contains("UV Powerup")) 
            { 
                Destroy(this.gameObject); 
                other.GetComponent<Player>().ActivatePowerUp(); 
            }
        }
    }
}
