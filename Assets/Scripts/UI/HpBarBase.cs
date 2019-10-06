using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarBase : MonoBehaviour
{
    Vector3 screenPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log("target is " + screenPos.x + " pixels from the left");
    }
}
