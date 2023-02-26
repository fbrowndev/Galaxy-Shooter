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
            _speed = 6;
            transform.position = Vector2.MoveTowards(transform.position, _playerTarget.position, _speed * Time.deltaTime);
        }
        else
        {
            _speed = 4;
            base.Movement();
        }
    }
    #endregion
}
