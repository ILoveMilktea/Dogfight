using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    Vector3 prevPlayerPos;
    public Vector3 distance;
    public Transform player;
    public Camera mainCamera;

    private float bottomSpace;
    private float topSpace;
    private float mapHeight;

    // Start is called before the first frame update
    void Start()
    {
        prevPlayerPos = player.position;
        //bottom
        bottomSpace = mainCamera.orthographicSize * Mathf.Sin(Mathf.PI * (mainCamera.transform.eulerAngles.x / 180));
        bottomSpace -= 1;

        //top
        float lenX = Mathf.Abs(distance.z) + player.position.y * Mathf.Tan(Mathf.PI * ((90 - mainCamera.transform.eulerAngles.x) / 180));
        topSpace = bottomSpace + 2 * lenX;

        mapHeight = 40;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 next = player.transform.position + distance;
        next.x = distance.x;
        
        transform.position = Vector3.Lerp(transform.position, next, Time.deltaTime * 3);

        if(transform.position.z < bottomSpace)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomSpace);
        }
        else if(transform.position.z > mapHeight - topSpace)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, mapHeight - topSpace);
        }

    }
}
