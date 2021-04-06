using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("External Components")]
    
    //not necessary - is attached in the _powerupprefabs list
    //[SerializeField] private GameObject _UVLightPrefab;
    [SerializeField]
    private List<GameObject> _virusPrefabs;
    [SerializeField] 
    private List<GameObject> _powerupPrefabs;

    
    
    [Header("Spawning Parameters")]
    [SerializeField] 
    private float _delay = 2f;
    
    [SerializeField] 
    private float _powerUpsSpawnRate = 30f;
    
    
    [Range(0f,1f)]
    [SerializeField] private float _normalCoronaSpawnChance;
    [Range(0f,1f)]
    [SerializeField] private float _chanceModifier = 0.01f;
    
    
    //these bools are there in order to make it possible to only spawn powerups or viruses respectively,
    // _spawningOn enables or deables spawning in general,a bit redundant as we can just enable or deable the spawnmanager in Unity,
    // but for reasons of completeness is also made it serializable
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

    // create corona prefab after certain amount of time

    // this function is supposed to ensure that the powerups dont always spawn at the exact same time
    // but more or less (+-3 seconds). It is also used in spawnpowerup(). 
    //UPDATE: not essential anymore, function can be removed if deemed unnecessary
    // NOT USED AT THE MOMENT
    private float _randomizePowerupSpawnRate()
    {
        float powerupSpawnAdjustment = Random.Range(-3f, 3f);

        return powerupSpawnAdjustment;
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
        // this expression is true as long as the game is running.
        while (_spawningOn && _coronaSpawnOn)
        {
            Random.Range(0f, 1f);
            // spawn a new virus
            Instantiate(_virusPrefabs[SelectVirusIndex()], new Vector3(Random.Range(-8.5f, 8.5f), 4.5f, 0f), Quaternion.identity, this.transform);
            // suspend execution for 2 seconds
            yield return new WaitForSeconds(_delay);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (_spawningOn && _powerupSpawnOn)
        {
            Instantiate(_powerupPrefabs[SelectPowerupIndex()], new Vector3(Random.Range(-8.5f, 8.5f), 4.5f, 0f), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUpsSpawnRate);
        }
    }
}   