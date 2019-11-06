using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : LivingEntity
{
    public enum State { Idle, Attacking ,Attacked, KnockBack};
    public float moveSpeed = 5;    

    public void Move()
    {

    }
}
