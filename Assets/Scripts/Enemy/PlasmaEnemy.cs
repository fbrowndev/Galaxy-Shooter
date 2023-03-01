using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaEnemy : Enemy
{
    #region Plasma Variables
    [SerializeField] private Transform _wayPoint1, _wayPoint2;
    [SerializeField] private GameObject _plasmaBeam;
    
    private bool _moveLeft;

    #endregion


    protected override void Movement()
    {
        if (_moveLeft)
        {
            transform.position = Vector2.MoveTowards(transform.position, _wayPoint1.position, _speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _wayPoint2.position, _speed * Time.deltaTime);
        }


        if(transform.position == _wayPoint1.position && _moveLeft)
        {
            _moveLeft = false;
        }
        else if(transform.position == _wayPoint2.position && _moveLeft == false)
        {
            _moveLeft = true;
        }
    }

    protected override void Attack()
    {
        StartCoroutine(PlasmaBeamRoutine());
    }

    IEnumerator PlasmaBeamRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 6));
            _plasmaBeam.SetActive(true);
            yield return new WaitForSeconds(Random.Range(4, 6));
            _plasmaBeam.SetActive(false);
        }
        
    }

}
