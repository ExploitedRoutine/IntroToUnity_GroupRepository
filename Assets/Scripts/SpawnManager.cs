using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField]
    private List<GameObject> _virusPrefabs;
    [SerializeField] 
    private List<GameObject> _powerupPrefabs;

    
    
    [Header("Spawning Parameters")]
    [SerializeField] 
    private float _delay = 2f;
    
    [SerializeField] 
    private float _powerUpsSpawnRate = 18f;
    
    [Range(0f,1f)]
    [SerializeField] private float _normalCoronaSpawnChance;
    [Range(0f,1f)]
    [SerializeField] private float _chanceModifier = 0.03f;
    
    
    //these bools are there in order to make it possible to only spawn powerups or viruses respectively,
    // _spawningOn enables or disables spawning in general
    [Header("Spawning Settings")]
    [SerializeField]
    private bool _spawningOn = true;

    [SerializeField]
    private bool _coronaSpawnOn = true;

    [SerializeField] 
    private bool _powerupSpawnOn = true; 
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerUp());
    }
    
    
    

    // this function is supposed to ensure that the viruses dont always spawn at the exact same time and thus form a consistent line
    // but more or less (+-3 seconds).
    private float _randomizeVirusSpawnRate()
    {
        float randomVirusSpawnRate = Random.Range(-2f, 0f);

        return randomVirusSpawnRate;
    }
    
    
    
    public void onPlayerDeath()
    {
        _spawningOn = false;
    }

    private int SelectVirusIndex()
    {
        int virusIndex = 0;
        if (_normalCoronaSpawnChance < Random.Range(0f,1f))
        {
            _normalCoronaSpawnChance += _chanceModifier;
            virusIndex = 0;
        }
        else
        {
            virusIndex = Random.Range(1, _virusPrefabs.Count);
        }

        return virusIndex;
    }

    private int SelectPowerupIndex()
    {

        int powerupIndex = Random.Range(0, _powerupPrefabs.Count);
        return powerupIndex;
    }
    
    IEnumerator SpawnSystem()
    {
        if (!_spawningOn) yield break;
        while (_coronaSpawnOn)
        {
            // spawn a new virus
            Instantiate(_virusPrefabs[SelectVirusIndex()], new Vector3(Random.Range(-7.0f, 7.0f), 4.5f, 0f), Quaternion.identity, this.transform);
            // suspend execution for 2 seconds
            yield return new WaitForSeconds(_delay + _randomizeVirusSpawnRate());
        }
        // this expression is true as long as the game is running.

    }

    IEnumerator SpawnPowerUp()
    {
        if (!_spawningOn) yield break;
        while (_powerupSpawnOn)
        {
            Instantiate(_powerupPrefabs[SelectPowerupIndex()], new Vector3(Random.Range(-7.0f, 7.0f), 4.5f, 0f), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUpsSpawnRate);
        }
    }
}   