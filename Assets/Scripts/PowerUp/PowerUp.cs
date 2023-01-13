using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles behavior for powerup
/// </summary>
public class PowerUp : MonoBehaviour
{
    [Header("PowerUp Settings")]
    [SerializeField] private float _speed;

    //powerup IDs
    [Header("PowerUp Identifier")]
    [SerializeField] private int _powerupID;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource= GetComponent<AudioSource>();

        //null check
        if(_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }


    #region Movement
    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region Collision Detector
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController player = collision.transform.GetComponent<PlayerController>(); 

            if(player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActivated();
                        break;
                    case 1:
                        player.SpeedPowerActivated();
                        break;
                    case 2:
                        player.ShieldPowerActivated();
                        break;
                    default:
                        Debug.Log("Default Case");
                        break;
                }
            }

            _audioSource.Play();
            Destroy(this.gameObject);
        }
    }

    #endregion
}
