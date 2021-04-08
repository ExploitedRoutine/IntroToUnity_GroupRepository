using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
    [Header("Vaccine Parameter")]
    [SerializeField] 
    private float _vaccineSpeed = 7f;
    
    
    [SerializeField] 
    private float _spinSpeed = 20f;
    
    [SerializeField] 
    private bool _rotationOn = true;
    
    //[SerializeField] 
    //private float _rotationspeed = 150f;
    
   void Update()
   {
       
       if (_rotationOn && !name.Contains("UVLight"))
       {
           transform.Rotate(new Vector3(0f, _spinSpeed * Time.deltaTime, 0f), Space.Self);
       }
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
       if (other.CompareTag("Player") && name.Contains("Evil"))
       {
           other.GetComponent<Player>().Damage();
           GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
           Destroy(this.gameObject);
       }

       if (other.CompareTag("Virus"))
       {
           
           if (name.Contains("UVLight"))
           {
               Destroy(other.gameObject);
           }
           else if (!name.Contains("Coronavirus501V2"))
           
           {
               if (name.Contains("B117"))
               {
                   GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(3);
               }
               else
               {
                   GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
               }
               
               Destroy(this.gameObject);
               Destroy(other.gameObject);
           }
           
       }
       //if vaccine is hit destroy it and the vaccine, If its UV light just destroy virus
       else if (other.CompareTag("Vaccine"))
       {
           if (!other.name.Contains("UVLight"))
           {
               Destroy(other.gameObject);
           }
   
           /*if(name.Contains("B117"))
           {
               GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(3);
           }
           else
           {
               GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
           } */
            
           Destroy(this.gameObject);
       }
   }
}