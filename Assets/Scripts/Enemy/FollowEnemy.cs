using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is inheritiing
/// </summary>
public class FollowEnemy : Enemy
{
    #region Follow Variables
    [Header("Follow Settings")]
    [SerializeField] private float _distanceToEnemy;

    private Transform _playerTarget;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _playerTarget = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void Movement()
    {
        if(Vector3.Distance(transform.position, _playerTarget.position) > _distanceToEnemy)
        {
            _speed = 4;
            base.Movement();
        } 
        else
        {
            _speed = 2;
            Vector3.MoveTowards(transform.position, _playerTarget.position, _speed);
        }
    }


    #region Collision Handler
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

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
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
