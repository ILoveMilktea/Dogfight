using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>, IPlayerMove
{
    private PlayerMove playerMove = new PlayerMove();
    public float moveSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Move(Vector3 dir, float amount)
    {
        //Vector3 targetPos = transform.position + dir * amount * moveSpeed;
        //transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime);

        transform.position = transform.position + dir * amount * moveSpeed * Time.deltaTime;
    }

    public void Stop()
    {
        transform.position = transform.position;
    }
}
