using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handling all controls for the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Player Variables
    [Header("Player Controls")]
    [SerializeField] float speed = 5;
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
    }

    #region Movement Methods
    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
    #endregion
}
