using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //Map
    public Vector2 mapSize;

    //Floor 생성 관련
       public FloorController floorController;

    //Fence 생성 관련    
    public FenceGenerator fenceGenerator;

    private void Awake()
    {
        PrepareMap(1);
    }

    // Start is called before the first frame update
    private void Start()
    {
        GenerateMap();
    } 

    //게임 켜질때 Map생성 , 게임 상점단계에서 Map생성
    public void PrepareMap(int stage)
    {      
        floorController.PrepareFloor(mapSize, stage);      
        fenceGenerator.PrepareFence(mapSize,stage);
    }

    //FightScene 들어갈때 Map활성화
    public void GenerateMap()
    {       
        floorController.GenerateFloor(mapSize);       
        fenceGenerator.GenerateFence(mapSize);
    }

    


    
   
}
