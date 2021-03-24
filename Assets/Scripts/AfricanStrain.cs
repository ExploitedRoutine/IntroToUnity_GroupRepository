using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfricanStrain : MonoBehaviour
{
    [SerializeField] private float _virusSpeed = 3f;
    [SerializeField] private GameObject _evilVaccinePrefab;
    [SerializeField] private float _incidentRate = 2f;

    private float _canInfect = -1f;



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (_virusSpeed * Time.deltaTime));
        Infect();
    }

    public void Infect()
    {
        if (Time.time > _canInfect)
        {
            _canInfect = Time.time + _incidentRate;
            Instantiate(_evilVaccinePrefab, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity);
        }
    }
    void OnTriggerEnter(Collider other)
    {
     //if player is hit deal damage or kill
         if (other.CompareTag("Player"))
         {
             other.GetComponent<Player>().Damage();
             Destroy(this.gameObject);
         }
     //if vaccine is hit destroy it and the vaccine, If its UV light just destroy virus
        else if (other.CompareTag("Vaccine")) 
         {
             if (!other.name.Contains("UVLight"))
             {
                 Destroy(other.gameObject);
             }
             GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(5);
             Destroy(this.gameObject);
         }
    }
}
