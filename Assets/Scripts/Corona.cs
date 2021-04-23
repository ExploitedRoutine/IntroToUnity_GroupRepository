using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Corona : MonoBehaviour
{
    [Header("Virus Parameter")]
    [SerializeField] 
    private float _virusSpeed = 3f;
    [SerializeField] 
    private float _horizontalVirusSpeed = 20f;
    
   

    void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponent<Player>()._freezeCorona == false)
        {
            transform.Translate(Vector3.down * (Time.deltaTime * _virusSpeed));


            if (name.Contains("B117"))
            {
                transform.Translate(Vector3.right * (Random.Range(-5f, 5f) * Time.deltaTime * _horizontalVirusSpeed));
            }
        }

        // respawn virus on top of the screen after y threshold
        if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f), 4.5f, 0f);
            
        }
    }
    
   
    
    void OnTriggerEnter(Collider other)
    {
        //if player is hit deal damage or kill
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage(1);
            Destroy(this.gameObject);
        }
        //if vaccine is hit destroy it and the vaccine, If its UV light just destroy virus
        else if (other.CompareTag("Vaccine"))
        {
            if (!other.name.Contains("UVLight"))
            {
                if (other.name.Contains("Shield"))
                {
                    GameObject.FindWithTag("Player").GetComponent<Player>()._isShieldOn = false;
                }
                Destroy(other.gameObject);
            }

            if(name.Contains("B117"))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(3);
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
            Destroy(this.gameObject);
        }
    }
}    
