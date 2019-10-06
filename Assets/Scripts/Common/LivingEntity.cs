using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    //KnockBack Timer
    private float knockBackCount = 0;   
    private bool isKnockBack = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        health = startingHealth;
    }

    public void Update()
    {
       
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        Debug.Log("체력:"+health);

        if(health<=0 && !dead)
        {
            Die();
        }
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
        GameObject.Destroy(gameObject);
    }

    IEnumerator KnockBackTimer(float knockBackDuration)
    {
        yield return new WaitForSeconds(knockBackDuration);
        isKnockBack = false;
        StopKnockBack();
    }
}
