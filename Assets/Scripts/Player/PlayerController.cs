using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handling all controls for the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Player Variables
    [Header("Player Variables")]
    [SerializeField] private float _speed = 5;
    [SerializeField] private GameObject _laserObject;
    [SerializeField] private GameObject _tripleShot; //powerup object

    //Player Boundary Variables
    float _boundaryX = 10.5f;
    [Header("Player Boundaries")]
    [SerializeField] private float _upperY = 0;
    [SerializeField] private float _lowerY = -3.45f;

    //offset for firing
    [Header("Firing Settings")]
    [SerializeField] private float _offsetY = 5;
    [SerializeField] private float _firingRate = 0.5f;
    bool _canFire;

    [Header("Player Settings")]
    [SerializeField] private int _lives = 3;

    private SpawnManager _spawnManager;

    //checking for powerups states
    [Header("PowerUp States")]
    [SerializeField] private bool _tripleShotActive = false;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //setting new position
        transform.position = new Vector3(0, 0, 0);
        _canFire = true;

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            FireLaser();
        }          
    }

    #region Movement Methods
    /// <summary>
    /// Handling all movement for player sprite
    /// </summary>
    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        PlayerBounds();
    }

    /// <summary>
    /// Player wrapping on x-axis and restricted on y-axis
    /// </summary>
    void PlayerBounds()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _lowerY, _upperY), 0);

        if(transform.position.x >= _boundaryX)
        {
            transform.position = new Vector3(-_boundaryX, transform.position.y, 0);
        } 
        else if(transform.position.x <= -_boundaryX)
        {
            transform.position = new Vector3(_boundaryX, transform.position.y, 0);
        }
    }
    #endregion

    #region Firing Methods
    void FireLaser()
    {
        Vector3 firePosition = new Vector3(transform.position.x, transform.position.y + _offsetY, 0);


        if (_tripleShotActive == true)
        {
            Instantiate(_tripleShot, firePosition, Quaternion.identity);
        } 
        else
        {
            Instantiate(_laserObject, firePosition, Quaternion.identity);
        }

        _canFire = false;
        StartCoroutine(LaserTimer());


    }

    IEnumerator LaserTimer()
    {
        yield return new WaitForSeconds(_firingRate);
        _canFire = true;
    }
    #endregion

    public void Damage()
    {
        _lives -= 1;

        if(_lives < 1)
        {
            //Communicate with the spawn manager
            //let them know to stop spawning

            if(_spawnManager != null)
            {
                _spawnManager.OnPlayerDeath();
            }

            Destroy(this.gameObject);
        }
    }
}
