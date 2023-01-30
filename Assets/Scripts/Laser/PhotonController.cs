using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour
{
    #region Photon Variables
    [Header("Photon Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToEnemy;

    [Header("Photon Boundries")]
    [SerializeField] private float boundaryX;
    [SerializeField] private float boundaryY;

    private Transform _enemyTarget;

    
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _enemyTarget = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
    }

    void Update()
    {
        if(transform.position.y < -boundaryY || transform.position.y > boundaryY || transform.position.x < -boundaryX || transform.position.x > boundaryX)
        {
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }


    void Move()
    {
        if (_enemyTarget != null)
        {
            if (Vector3.Distance(transform.position, _enemyTarget.position) < _distanceToEnemy)
            {
                transform.position = Vector3.MoveTowards(transform.position, _enemyTarget.position, _speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }



    }   
}
