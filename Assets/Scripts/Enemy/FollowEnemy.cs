using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is inheritiing
/// </summary>
public class FollowEnemy : MonoBehaviour
{
    #region Follow Variables
    [Header("Follow Settings")]
    [SerializeField] private float _distanceToEnemy;

    private Transform _playerTarget;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _playerTarget = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
