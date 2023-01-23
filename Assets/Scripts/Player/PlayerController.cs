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
    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;

    [Header("Shield Settings")]
    [SerializeField] private GameObject _playerShield;
    [SerializeField] private SpriteRenderer _shieldRenderer;
    [SerializeField] private Color _shieldColor;
    private int _shieldLevel;
    

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
    [SerializeField] private int _ammoCount = 15;

    //Script communciation
    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    //checking for powerups states
    private bool _tripleShotActive = false;
    private bool _shieldActive = false;

    //tracking player score
    private int _score;

    //tracking Thrust variables
    private int _thrustPower = 100;
    private float _currentThrust;
    private bool _gasDrain = false;
    private bool _refillNeeded = false;

    //Everything pretaining to audio
    [Header("Audio Components")]
    private AudioSource _audioSource;
    [SerializeField]private AudioClip _laserSound;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentThrust = _thrustPower;

        //setting starting position
        transform.position = new Vector3(0, 0, 0);
        _canFire = true;

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }

        if(_audioSource == null)
        {
            Debug.LogError("The audio source is null.");
        }

        _uiManager.SetMaxThrust(_thrustPower);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ThrusterActiviated();

        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            FireLaser();
            if (_ammoCount > 0 && _tripleShotActive == false)
            {
                _ammoCount--;
                _uiManager.AmmoCheck(_ammoCount);
            } 
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
        _audioSource.PlayOneShot(_laserSound);
        Vector3 firePosition = new Vector3(transform.position.x, transform.position.y + _offsetY, 0);


        if (_tripleShotActive == true)
        {
            Instantiate(_tripleShot, firePosition, Quaternion.identity);
        } 
        else if (_ammoCount > 0)
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

    #region Public Methods
    /// <summary>
    /// Handling player lives and communicates to spawn manager to stop spawning
    /// </summary>
    public void Damage()
    {
        if (_shieldActive == true)
        {
            _shieldLevel--;

            if(_shieldLevel == 2)
            {
                _shieldColor.a = .6f;
                _shieldRenderer.color = _shieldColor;
                return;
            } 
            else if(_shieldLevel == 1)
            {
                _shieldColor.a = .3f;
                _shieldRenderer.color = _shieldColor;
                return;
            } 
            else if(_shieldLevel <= 0)
            {
                _shieldActive = false;
                _shieldRenderer.color = _shieldColor;
                _playerShield.SetActive(false);
                return;
            }
            
        }

        _lives -= 1;
        _uiManager.UpdateLives(_lives);

        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        } 
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

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

    #region PowerUp Methods
    /// <summary>
    /// Handling Triple Shot power up activation
    /// </summary>
    public void TripleShotActivated()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }

    /// <summary>
    /// Handles speed power up activation
    /// </summary>
    public void SpeedPowerActivated()
    {
        _speed += 5;
        StartCoroutine(SpeedUpRoutine());
    }

    public void ShieldPowerActivated()
    {
        _shieldLevel = 3;
        _shieldColor.a = 1.0f;
        _shieldRenderer.color = _shieldColor;
        _shieldActive = true;
        _playerShield.SetActive(true);
    }

    public void HealthGranted()
    {
        if(_lives < 3)
        {
            _lives += 1;
        }

        _uiManager.UpdateLives(_lives);

        //fixing engines
        if(_lives == 2)
        {
            _rightEngine.SetActive(false);
        } 
        else if(_lives == 3)
        {
            _leftEngine.SetActive(false);
        }
    }

    /// <summary>
    /// Below is handing all routines for powerups
    /// </summary>
    /// <returns></returns>
    IEnumerator TripleShotRoutine()
    {
        yield return new WaitForSeconds(10);
        _tripleShotActive = false;
    }

    IEnumerator SpeedUpRoutine()
    {
        yield return new WaitForSeconds(8);
        _speed -= 5;
    }
    #endregion

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    #endregion

    #region Special Abilities

    #region Thrust Methods
    void ThrusterActiviated()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && _currentThrust > 1)
        {
            StartCoroutine(ThrustDeplet());
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && _refillNeeded == false)
        {
            _speed = 12;
            _refillNeeded = false;
        } 
        
        if(Input.GetKeyUp(KeyCode.LeftShift) || _refillNeeded)
        {
            _refillNeeded = true;
            _gasDrain = false;
            _speed = 6;
            StopCoroutine(ThrustDeplet());
            StartCoroutine(ThrustRefill());
        }

        if(_currentThrust < 2) { _refillNeeded = true;  }
    }

    IEnumerator ThrustDeplet()
    {
        _gasDrain = true;
        while(_currentThrust > 0 && _gasDrain == true)
        {
            yield return new WaitForSeconds(.2f);
            _currentThrust -= 2f;
            _uiManager.SetThrust(_currentThrust);
        }
    }

    IEnumerator ThrustRefill()
    {
        while(_currentThrust < 100 && _gasDrain == false)
        {
            yield return new WaitForSeconds(.2f);
            _currentThrust += 1f;
            _uiManager.SetThrust(_currentThrust);
        }

        if(_currentThrust >= 100) { _refillNeeded = false; }
    }
    #endregion

    public void AmmoReload()
    {
        _ammoCount = 15;
        _uiManager.AmmoCheck(_ammoCount);
    }

    #endregion

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyLaser")
        {
            Damage();
            Destroy(collision.gameObject);
        }
    }
}
