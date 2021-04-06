using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsCollectible : MonoBehaviour
{
    [Header("PowerUp Paramter")]
    [SerializeField] 
    private float _speed = 2f;

    [SerializeField] private int extraLives = 1;
    
    // called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
       
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (name.Contains("AddLive"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().AddLive(extraLives);
        }

        if (name.Contains("UV Powerup"))
        {
            Destroy(this.gameObject);
            other.GetComponent<Player>().ActivatePowerUp(); 
        }
    }
}
