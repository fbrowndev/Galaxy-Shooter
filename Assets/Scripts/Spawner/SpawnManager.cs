using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
class WaveManager
{
    public int waveID;
    public float waveRate;
    public int enemySpawnTotal;
    public GameObject[] enemySpawn;
}



/// <summary>
/// Handling all spawner events
/// </summary>
public class SpawnManager : MonoBehaviour
{

    #region Variables
    [Header("Spawn Objects")]
    public GameObject[] _enemyObjects;
    [SerializeField] private GameObject[] _commonPowerups, _uncommonPowerups, _rarePowerups;

    [Header("Enemy Controls")]
    [SerializeField] private float _spawnTimer = 2f;
    [SerializeField] private GameObject _enemyContainer;

    [Header("Item Probabiltiy")]
    [SerializeField][Range(0,1)] private float _commonSpawn, _uncommonSpawn;

    [ReadOnly] private int _spawnTime = 3;

    bool _stopSpawning;

    [Header("Wave Management")]
    [SerializeField][NonReorderable] private WaveManager[] _waveManager;
    #endregion

    public void StartSpawner()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    #region Spawn Routines
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int RandomEnemy = Random.Range(0, _enemyObjects.Length);
            GameObject newEnemy = Instantiate(_enemyObjects[RandomEnemy], spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTimer);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(_spawnTime);
        while(_stopSpawning == false)
        {
            PowerUpCreation();
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    void PowerUpCreation()
    {
        float spawnRarity = Random.Range(0f, 1f);
        Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);

        GameObject[] spawnGroup; 

        if(spawnRarity >= _commonSpawn)
        {
            spawnGroup = _commonPowerups;
        }
        else if(spawnRarity < _commonSpawn && spawnRarity > _uncommonSpawn)
        {
            spawnGroup = _uncommonPowerups;
        }
        else
        {
            spawnGroup = _rarePowerups;
        }

        Instantiate(spawnGroup[Random.Range(0, spawnGroup.Length )], spawnPos, Quaternion.identity);
    }


    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    #endregion

    void WaveController()
    {
        Debug.Log("Controlling Wave");
    }


}
