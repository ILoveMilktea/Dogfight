using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour,IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    //KnockBack Timer   
    private bool isKnockBack = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        health = startingHealth;
    }  

    public void TakeHit(float damage)
    {
        FightSceneController.Instance.DamageToCharacter(gameObject, (int)damage); // UI & data
        //health -= damage;
        //Debug.Log("체력:"+health);
        //FightSceneController.Instance.DamageToCharacter(gameObject, (int)damage); // UI & data

        //if(health<=0 && !dead)
        //{
        //    Die();
        //}

    }

    public void TakeKnockBack(Vector3 dir, float force, float knockBackDuration)
    {
        //dir : 날라갈 방향
        Debug.Log("KnockBack");
        GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.VelocityChange);
        isKnockBack = true;
        StartCoroutine(KnockBackTimer(knockBackDuration));

    }

    public void StopKnockBack()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void Die()
    {
        dead = true;
        StopAllCoroutines();
        FightSceneController.Instance.EnemyDead(gameObject);
    }

    IEnumerator KnockBackTimer(float knockBackDuration)
    {
        yield return new WaitForSeconds(knockBackDuration);
        isKnockBack = false;
        StopKnockBack();
    }
}
