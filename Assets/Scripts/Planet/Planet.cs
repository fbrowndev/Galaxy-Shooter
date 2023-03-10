using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Planet Settings")]
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private GameObject _planetExplosion;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    private int _level = 1;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlanetRotation();
    }

    void PlanetRotation()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }


    #region Collision Handlers
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Laser")
        {
            GameObject explosion = Instantiate(_planetExplosion, transform.position, Quaternion.identity);

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            
            //Destroying the instantiated game object
            Destroy(explosion, 2f);

            _spawnManager.StartSpawner();

            _uiManager.UpdateWave(_level);
        }
    }

    #endregion
}
