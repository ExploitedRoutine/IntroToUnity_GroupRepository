using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsCollectible : MonoBehaviour
{
    [Header("PowerUp Paramter")]
    [SerializeField] 
    private float _speed = 2f;
    
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
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ActivatePowerUp();
            Destroy(this.gameObject);
        }
    }
}
