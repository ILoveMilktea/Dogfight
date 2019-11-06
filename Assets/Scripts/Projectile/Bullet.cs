using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bullet(총알) 클래스
public class Bullet : Projectile
{    
    void Update()
    {
        float moveDistance = Move();

        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
        CheckMoveDistance();

    }    
}
