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

    [Header("Audio")]
    [SerializeField] private AudioClip _pickupSound;

    private Transform _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
        SuperCollect();
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

            AudioSource.PlayClipAtPoint(_pickupSound, transform.position);
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
                    case 3:
                        player.HealthGranted();
                        break;
                    case 4:
                        player.AmmoReload();
                        break;
                    case 5:
                        player.PhotonLasersActivated();
                        break;
                    case 6:
                        player.NegativeAmmoCache();
                        break;
                    default:
                        Debug.Log("Default Case");
                        break;
                }
            }

            Destroy(this.gameObject);
        }

        if(collision.tag == "EnemyLaser")
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    void SuperCollect()
    {
        if (Input.GetKey(KeyCode.C))
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, 2f * Time.deltaTime);
        }
    }
}
