using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaEnemy: Enemy
{

    private Vector3 _endPosition = new Vector3(0, 3, 0);
    private Vector3 _leftPosition = new Vector3(-6, 3, 0);
    private Vector3 _rightPosition = new Vector3(6, 3, 0);

    private bool _roamingActive = false;

    protected override void Movement()
    {
        if (transform.position != _endPosition && _roamingActive == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, _speed * Time.deltaTime);
        }

        if (transform.position.y == _endPosition.y)
        {
            _roamingActive = true;

            if (_roamingActive == true && transform.position != _leftPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _leftPosition, _speed * Time.deltaTime);
            }
            else if (_roamingActive == true && transform.position == _leftPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _rightPosition, _speed * Time.deltaTime);
            }
        }
    }

}
