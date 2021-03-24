using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] private GameObject _UVLightPrefab;
    
    [Header("Spawning Parameters")]
    [SerializeField] private float _delay = 2f;
    [SerializeField] private float _powerUpsSpawnRate = 30f;

    [SerializeField] private List<GameObject> _virusPrefabs;

    [Range(0f,1f)]
    [SerializeField] private float _normalCoronaSpawnChance;
    [Range(0f,1f)]
    [SerializeField] private float _chanceModifier = 0.01f;
    
    private bool _spawningOn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerUp());
    }

    // create corona prefab after certain amount of time

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
    IEnumerator SpawnSystem()
    {
        // this expression is true as long as the game is running.
        while (_spawningOn)
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
        while (_spawningOn)
        {
            Instantiate(_UVLightPrefab, new Vector3(Random.Range(-8.5f, 8.5f), 4.5f, 0f), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUpsSpawnRate);
        }
    }
}   