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
    private float _spinSpeed = 50f;

    [SerializeField]
    private bool _rotationOn = true;
    
    

    private Vector3 scaleChange = new Vector3(3.5f, 3.5f, 3.5f);
    

    private void Start()
    {
        if (name.Contains("Shield"))
        {
            transform.localScale += scaleChange;
        }
    }

    void Update()
    {

        if (_rotationOn && !name.Contains("UVLight"))
        {
            transform.Rotate(new Vector3(0f, _spinSpeed * Time.deltaTime, 0f), Space.Self);
        }

        if (CompareTag("Vaccine") && !name.Contains("Shield"))
        {
            transform.Translate(Vector3.up * (Time.deltaTime * _vaccineSpeed));
            if (transform.position.y > 7f)
            {
                Destroy(this.gameObject);
            }

        }
        /*else if (name.Contains("Shield"))
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z);
        } */
        else if (name.Contains("EvilVaccine"))
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
        if (name.Contains("Evil"))
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().Damage(1);
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
                Destroy(this.gameObject);
            }
            else if (other.name.Contains("Shield"))
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>()._isShieldOn = false;
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }

            else if (other.CompareTag("Vaccine"))
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
        }

        else if (other.CompareTag("Virus"))
        {
            if (name.Contains("UVLight"))
            {
                Destroy(other.gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }

            else if (other.name.Contains("Coronavirus501V2") || other.name.Contains("Coronavirus"))
            {
                
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
           
            else if (other.name.Contains("B117"))
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(3);
            }
            
            else if (other.name.Contains("BIGCorona"))
            {
                Destroy(this.gameObject);
                GameObject.FindWithTag("Virus").GetComponent<AfricanStrain>().Damage();
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
        }
        
        
        
        
    }
}