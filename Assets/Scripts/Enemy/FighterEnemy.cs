using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : MonoBehaviour
{
    #region Enemy Variables
    [Header("Enemy Settings")]
    [SerializeField]  private float _speed = 4;
    [SerializeField] private int _enemyValue;
    [SerializeField] private GameObject _enemyLaser;

    private PlayerController _player;
    private Animator _anim;
    private AudioSource _audioSource;

    private float _fireRate;
    private float _canFire;

    private Transform _powerUp;
    private Transform _playerLaser;

    #endregion

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();

        _anim = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("PlayerController is Null");
        }

        if (_anim == null)
        {
            Debug.LogError("Animator is null.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
        }

        _playerLaser = GameObject.FindGameObjectWithTag("Laser").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        EnemyLaser();
        EnemyDodge();
        PowerUpDetected();
    }

    #region Enemy Movement
    void Movement()
    {
        Vector3 respawnPosition = new Vector3((Random.Range(-8, 8)), 8, 0);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5)
        {
            transform.position = respawnPosition;
        }

    }

    /// <summary>
    /// Will handle logic for enemy dodging laser
    /// </summary>
    void EnemyDodge()
    {
        if(_playerLaser != null)
        {
            float distance = Mathf.Abs(_playerLaser.transform.position.x - transform.position.x);

            if(distance < 4f)
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
        }
    }

    #endregion

    #region Enemy Attack
    void EnemyLaser()
    {
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 5f);
            _canFire = Time.time + _fireRate;
            Instantiate(_enemyLaser, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Will check to see if a powerup is nearby
    /// </summary>
    void PowerUpDetected()
    {
        _powerUp = GameObject.FindGameObjectWithTag("PowerUp").GetComponent<Transform>();

        if (_powerUp != null)
        {
            float distance = Mathf.Abs(_powerUp.transform.position.y - transform.position.y);

            if (distance < 3f)
            {
                Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            }
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