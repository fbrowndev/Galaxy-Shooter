using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handling all spawner events
/// </summary>
public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] GameObject[] enemyObjects;
    [SerializeField] float _spawnTimer = 2f;
    [SerializeField] GameObject _enemyContainer;

    bool _stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Enemy Spawn
    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(enemyObjects[0], spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTimer);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    #endregion

}
