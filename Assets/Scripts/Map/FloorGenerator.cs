using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    private string floorPrefabName;
        
    public void PrepareFloor(Vector2 mapSize, int stage, out List<List<Transform>> floorPrefabList)
    {
       
        floorPrefabName = SetFloorPrefabName(stage);

        floorPrefabList = new List<List<Transform>>();
       
        //int currentPrefabIndex = 0;
        //int floorPrefabMaxLength = floorPrefab.Length;
        //int firstPrefabIndex = 0;

        //나중에 Read String Stage이름으로 바꿔야함
        
        List<GameObject> floorPrefabListFromObjectPool = ObjectPoolManager.Instance.ReadAllFromUnusedList(floorPrefabName);
       
        Vector3 floorLocalScale = floorPrefabListFromObjectPool[0].transform.localScale;

        int count = 0;        

        for (int y = 0; y < mapSize.y; ++y)
        {            
            for (int x = 0; x < mapSize.x; ++x)
            {
               
                floorPrefabList.Add(new List<Transform>());

                Vector3 floorPosition = new Vector3((-mapSize.x / 2 + 0.5f + x)*floorLocalScale.x, 0, (mapSize.y/2 - 0.5f - y)*floorLocalScale.z);

                Transform prefabTmp = floorPrefabListFromObjectPool[count].transform;
                prefabTmp.position = floorPosition;
                prefabTmp.parent = transform;
                floorPrefabList[y].Add(prefabTmp);                             
                count++;                         
                
                

                //currentPrefabIndex++;
                //if (currentPrefabIndex == floorPrefabMaxLength)
                //{
                //    currentPrefabIndex = 0;
                //}
            }
            //currentPrefabIndex = firstPrefabIndex + 1;
            //firstPrefabIndex++;
            //if(currentPrefabIndex==floorPrefabMaxLength)
            //{
            //    currentPrefabIndex = 0;
            //    firstPrefabIndex = 0;
            //}
        }

    }

    public void GenerateFloor(Vector2 mapSize, ref List<List<Transform>> floorPrefabList)
    {
        for (int y = 0; y < mapSize.y; ++y)
        {
            for (int x = 0; x < mapSize.x; ++x)
            {
                floorPrefabList[y][x].gameObject.SetActive(true);
            }
        }

    }

    public string SetFloorPrefabName(int stage)
    {
        string floorPrefabName = null;
        switch (stage)
        {
            case 1:
                floorPrefabName = "Plane_Grass";
                break;
            case 2:
                floorPrefabName = "Plane_Rock";
                break;
            case 3:
                floorPrefabName = "Plane_Rock";
                break;
            case 4:
                floorPrefabName = "Plane_Rock";
                break;
            case 5:
                floorPrefabName = "Plane_Rock";
                break;
            case 6:
                floorPrefabName = "Plane_Rock";
                break;
        }

        return floorPrefabName;
    }

}
