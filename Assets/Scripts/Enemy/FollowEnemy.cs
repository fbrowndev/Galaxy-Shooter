using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is inheritiing from the enemy class
/// </summary>
public class FollowEnemy : Enemy
{
    #region Follow Variables
    [Header("Follow Settings")]
    [SerializeField] private float _distanceToEnemy;

    #endregion

    #region Enemy Movement
    protected override void Movement()
    {
        if (Vector3.Distance(transform.position, _playerTarget.position) < _distanceToEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerTarget.position, _speed * Time.deltaTime);
        }
        else
        {
            base.Movement();
        }
    }

    #endregion

    #region Collisions
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    #endregion
}
