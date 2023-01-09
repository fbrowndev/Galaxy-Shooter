using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    #region Laser Variables
    [Header("Movement")]
    [SerializeField] private float _speed;

    #endregion

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
    }
}
