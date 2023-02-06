using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Handling the behavior of the enemy
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Enemy Variables
    [Header("Enemy Settings")]
    [SerializeField] protected float _speed = 4;
    [SerializeField] protected int _enemyValue;

    protected PlayerController _player;
    protected Animator _anim;
    protected AudioSource _audioSource;
    #endregion

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();

        _anim = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();

        if(_player == null)
        {
            Debug.LogError("PlayerController is Null");
        }

        if(_anim == null)
        {
            Debug.LogError("Animator is null.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
        }
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    #region Enemy Movement
    protected virtual void Movement()
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1.5f);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
           
            if(_player != null)
            {
                _player.AddScore(_enemyValue);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1.5f);
            
        }
    }

    #endregion
}
