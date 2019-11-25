using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{   
    public Button button;
    public Image prevPathLine;


    //private RectTransform imageRectTransform;
    //public float lineWidth = 1.0f;
    //public Vector3 pointA;
    //public Vector3 pointB;
    //// Use this for initialization
    //void Start()
    //{
    //    imageRectTransform = GetComponent<RectTransform>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Vector3 differenceVector = pointB - pointA;

    //    imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineWidth);
    //    imageRectTransform.pivot = new Vector2(0, 0.5f);
    //    imageRectTransform.position = pointA;
    //    float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
    //    imageRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    //}
    
    public void LightOn()
    {
        prevPathLine.color = Color.white;
        button.image.color = Color.white;
    }
    
    public void LightOff()
    {
        prevPathLine.color = Color.gray;
        button.image.color = Color.gray;
    }
}
