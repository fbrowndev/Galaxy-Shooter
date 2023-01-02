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
    [SerializeField] float _speed = 5;
    [SerializeField] GameObject _laserObject;

    //Player Boundary Variables
    float _boundaryX = 10.5f;
    [Header("Player Boundaries")]
    [SerializeField] float _upperY = 0;
    [SerializeField]float _lowerY = -3.45f;



    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //setting new position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Fire();
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
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_laserObject, transform.position, transform.rotation);
        }
    }
    #endregion
}
