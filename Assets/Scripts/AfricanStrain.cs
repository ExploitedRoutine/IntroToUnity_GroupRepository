using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfricanStrain : MonoBehaviour
{
    
    [Header("External Components")]
    [SerializeField]
    private GameObject _evilVaccinePrefab;
   
    [Header("Virus Parameter")]
    [SerializeField]
    private float _virusSpeed = 3f;
    
    [SerializeField]
    private float _bigVirusSpeed = 1f;

    [SerializeField] 
    private float _incidentRate = 2f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private Vector3 scaleChange = new Vector3(30f, 30f, 30f);
    
    private float _canInfect = -1f;

    private void Start()
    {
        if (name.Contains("BIGCorona"))
        {
            transform.localScale += scaleChange;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindWithTag("Player").GetComponent<Player>()._freezeCorona == false)
        {
            if (!name.Contains("BIGCorona"))
            {
                transform.Translate(Vector3.down * (_virusSpeed * Time.deltaTime));
                
            }
            else
            {
                transform.Translate(Vector3.down * (_bigVirusSpeed * Time.deltaTime));
            }
            Infect();
        }
        
    }

    private void Infect()
    {
        if (Time.time > _canInfect)
        {
            _canInfect = Time.time + _incidentRate;
            Instantiate(_evilVaccinePrefab, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity);
        }
    }
    
    
    public void Damage()
    {
        
        Debug.Log("Damage function called");
        //reduce _lives by one
        _lives -= 1; 
        if (_lives <= 0)
        {
            Destroy(this.gameObject);
            GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(5);
        }
    }
    void OnTriggerEnter(Collider other)
    {
     //if player is hit deal damage or kill
         if (other.CompareTag("Player"))
         {
             if (name.Contains("BIGCorona"))
             {
                 other.GetComponent<Player>().Damage(_lives);
                 Destroy(this.gameObject);
             }
             else
             {
                 other.GetComponent<Player>().Damage(1);
                 Destroy(this.gameObject);
             }
         }
     //if vaccine is hit destroy it and the vaccine, If its UV light just destroy virus
        
         else if (other.CompareTag("Vaccine")) 
         {
             if (name.Contains("BIGCorona"))
             {
                 Damage();
                 Destroy(other.gameObject);
                 GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
             }
             else if (!other.name.Contains("UVLight") && !name.Contains("BIGCorona") )
             {
                 Destroy(this.gameObject);
                 Destroy(other.gameObject);
                 GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(2);
             }
         }
    }
}
