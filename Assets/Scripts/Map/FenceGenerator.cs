using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGenerator : MonoBehaviour
{
    private Transform fencePrefab;
    private List<Transform> fencePrefabList;
    private string fencePrefabName;

    public void PrepareFence(Vector2 mapSize, int stage)
    {
        fencePrefabName = SetFencePrefabName(stage);
        fencePrefabList = new List<Transform>();

        List<GameObject> fencePrefabListFromObjectPool = ObjectPoolManager.Instance.ReadAllFromUnusedList(fencePrefabName);
        Vector3 fenceLocalScale = fencePrefabListFromObjectPool[0].transform.localScale;

        int count = 0;

        for (int y = 0; y < mapSize.y; ++y)
        {
            for (int x = 0; x < mapSize.x; ++x)
            {
                if (y >= 1 && y < mapSize.y - 1)
                {
                    if (x >= 1 && x < mapSize.x - 1)
                    {
                        continue;
                    }
                }                

                Vector3 fencePosition = new Vector3((-mapSize.x / 2 + 0.5f + x)*fenceLocalScale.x, 0, (mapSize.y / 2 - 0.5f - y)*fenceLocalScale.z);

                Transform prefabTmp = fencePrefabListFromObjectPool[count].transform;
                prefabTmp.position = fencePosition;
                prefabTmp.parent = transform;
                fencePrefabList.Add(prefabTmp);
                count++;
            }
        }
    }

    public void GenerateFence(Vector2 mapSize)
    {
        for(int i=0; i<fencePrefabList.Count; ++i)
        {
            fencePrefabList[i].gameObject.SetActive(true);
        }
    }

    public string SetFencePrefabName(int stage)
    {
        string fencePrefabName = null;
        switch (stage)
        {
            case 1:
                fencePrefabName = "Cube_Soil";
                break;
            //case 2:
            //    fencePrefabName = "Plane_Rock";
            //    break;
            //case 3:
            //    fencePrefabName = "Plane_Rock";
            //    break;
            //case 4:
            //    fencePrefabName = "Plane_Rock";
            //    break;
            //case 5:
            //    fencePrefabName = "Plane_Rock";
            //    break;
            //case 6:
            //    fencePrefabName = "Plane_Rock";
            //    break;
        }

        return fencePrefabName;
    }
}
