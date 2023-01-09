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



    // Start is called before the first frame update
    void Start()
    {

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
                player.TripleShotActivated();
            }

            Destroy(this.gameObject);
        }
    }

    #endregion
}
