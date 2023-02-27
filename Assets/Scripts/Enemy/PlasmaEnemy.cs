using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaEnemy: Enemy
{

    protected override void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < 3)
        {
            if(transform.position.x < -6f)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            } 
            else if (transform.position.x > 6)
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
        }
    }

}
