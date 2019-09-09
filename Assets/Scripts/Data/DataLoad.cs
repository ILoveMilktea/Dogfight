using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoad
{

    /// <summary>
    /// dataPath상 storage에 저장된 binary 파일을 읽는 함수
    /// </summary>
    /// <param name="dataPath"></param>
    public void LoadUserData(string dataPath)
    {
        byte[] saveFile = File.ReadAllBytes(dataPath);
        MemoryStream memoryStream = new MemoryStream(saveFile);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        //info.SetIsDataExist(binaryReader.ReadBoolean());
        //if (info.m_IsDataExist)
        //{

        //    info.SetChapter(binaryReader.ReadString());
        //    info.SetDate(binaryReader.ReadInt64());
        //    info.SetCharacterType(binaryReader.ReadString());
        //    info.SetLevel(binaryReader.ReadInt32());
        //    info.SetPlayTime(binaryReader.ReadSingle());
        //    info.SetArea(binaryReader.ReadInt32());
        //    Vector3 pos = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        //    info.SetPlayerPosition(pos);

        //    int InventoryItemsCount = binaryReader.ReadInt32();
        //    Dictionary<string, InventoryItemInfo> inventoryItems = new Dictionary<string, InventoryItemInfo>();
        //    for (int j = 0; j < InventoryItemsCount; j++)
        //    {
        //        InventoryItemInfo item = new InventoryItemInfo();

        //        string name = binaryReader.ReadString();
        //        item.SetIcon(name);
        //        item.SetItemInfo(name);
        //        item.SetCount(binaryReader.ReadInt32());
        //        item.SetIsEquip(binaryReader.ReadBoolean());

        //        inventoryItems.Add(name, item);
        //    }
        //    info.SetInventoryItems(inventoryItems);

        //}

        //dataSlots.Add(info.m_SlotName, info);

    }
    
}
