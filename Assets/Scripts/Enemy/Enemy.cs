using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handling the behavior of the enemy
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Enemy Variables
    [Header("Enemy Settings")]
    [SerializeField] private float _speed = 4;
    [SerializeField] private int _enemyValue;

    private PlayerController _player;
    #endregion

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    #region Enemy Movement
    void Movement()
    {
        Vector3 respawnPosition = new Vector3((Random.Range(-8,8)), 8,0);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5)
        {
            transform.position = respawnPosition;
        }

    }

    #endregion


    #region Collision Handlers
    void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player")
        {
            PlayerController player = other.transform.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
           
            if(_player != null)
            {
                _player.AddScore(_enemyValue);
            }

            Destroy(this.gameObject);
            
        }
    }

    #endregion
}
