using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handling all spawner events
/// </summary>
public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private GameObject[] _enemyObjects;
    [SerializeField] private GameObject[] _powerupObjects;
    [SerializeField] private float _spawnTimer = 2f;
    [SerializeField] private GameObject _enemyContainer;

    bool _stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawner()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    #region Enemy Spawn
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
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int RandomPowerUp = Random.Range(0, _powerupObjects.Length);
            Instantiate(_powerupObjects[RandomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    #endregion

}
