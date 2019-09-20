using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerMake
{
    public void MakeNewFile(string dataPath)
    {
        if (!File.Exists(dataPath))
        {
            File.Create(dataPath);
        }
    }

    public void MakeNewDirectory(string dataPath)
    {
        if(!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
    }
    /// <summary>
    /// SaveData.bytes 파일이 없으면 빈걸로 새로 만들자.
    /// </summary>
    public void SaveData()
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

        //List<DataSlot> dataSlots = UIManager.Get.m_DataWindowPrefab.GetComponentsInChildren<DataSlot>().ToList();

        //binaryWriter.Write(dataSlots.Count);
        //foreach (var dataSlot in dataSlots)
        //{
        //    binaryWriter.Write(dataSlot.gameObject.name);
        //    binaryWriter.Write(false);
        //}

        //// 생성된 Tuple List를 각 Name.bytes 파일로 저장
        //byte[] binaryFile = memoryStream.ToArray();

        //FileStream fileStream = new FileStream(m_DataPath, FileMode.Create);
        //fileStream.Write(binaryFile, 0, binaryFile.Length);

        //fileStream.Close();
        //binaryWriter.Close();

        //LoadAllFromStorage();
    }

    public void SaveDirectory()
    {

    }
}
