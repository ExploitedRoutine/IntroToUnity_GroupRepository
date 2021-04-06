using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
    [Header("Vaccine Parameter")]
    [SerializeField] 
    private float _vaccineSpeed = 7f;
    
    //[SerializeField] 
    //private float _rotationspeed = 150f;
    
   void Update()
   {
       if (CompareTag("Vaccine"))
       {
           transform.Translate(Vector3.up * (Time.deltaTime * _vaccineSpeed));
           if (transform.position.y > 7f)
           {
               Destroy(this.gameObject);
           }
       }
       else
       {
           transform.Translate(Vector3.down * (Time.deltaTime * _vaccineSpeed));
           if (transform.position.y < -7f)
           {
               Destroy(this.gameObject);
           }
       }
   }
   void OnTriggerEnter(Collider other) 
   {
       //if player is hit deal damage or kill
       if (other.CompareTag("Player"))
       {
           //other.GetComponent<Player>().Damage();
           //estroy(this.gameObject);
       }
       //if vaccine is hit destroy it and the vaccine, If its UV light just destroy virus
       else if (other.CompareTag("Vaccine"))
       {
           if (!other.name.Contains("UVLight"))
           {
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