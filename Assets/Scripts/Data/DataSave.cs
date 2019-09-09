using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave
{

    /// <summary>
    /// 게임 실행중에 현재 파일을 저장할 때에 호출합니다.
    /// </summary>
    /// <param name="slotName"></param>
    public void SaveUserData(string dataPath, UserData userData)
    {
        string toJsonData = JsonUtility.ToJson(userData);
    }
    
    /// <summary>
    /// 게임 종료할때 저장 정보들을 storage에 저장합시다.
    /// </summary>
    /// <param name="dataPath"></param>
    public void SaveToStorage()
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

        // save 순서
        //binaryWriter.Write(m_DataSlots.Count);
        //foreach (var dataSlot in m_DataSlots)
        //{
        //    SaveDataInfo info = dataSlot.Value;
        //    // slotname(string)
        //    binaryWriter.Write(info.m_SlotName);
        //    // check empty(bool)
        //    binaryWriter.Write(info.m_IsDataExist);

        //    if (info.m_IsDataExist)
        //    {
        //        // chapter(string)
        //        binaryWriter.Write(info.m_Chapter);
        //        // date(long)
        //        binaryWriter.Write(info.m_Date.ToBinary());
        //        // charactertype(string)
        //        binaryWriter.Write(info.m_CharacterType.ToString());
        //        // level(int)
        //        binaryWriter.Write(info.m_Level);
        //        // playtime(float)
        //        binaryWriter.Write(info.m_PlayTime);
        //        // scenename(string)
        //        binaryWriter.Write(info.m_AreaNumber);
        //        // pos(float,float,flaot)
        //        binaryWriter.Write(info.m_PlayerPosition.x);
        //        binaryWriter.Write(info.m_PlayerPosition.y);
        //        binaryWriter.Write(info.m_PlayerPosition.z);
        //        // inventoryitemscount(int)
        //        binaryWriter.Write(info.m_InventoryItems.Count);
        //        foreach (var item in info.m_InventoryItems)
        //        {
        //            //    itemname(string)
        //            binaryWriter.Write(item.Value.m_ItemInfo.m_Name);
        //            //    count(int)
        //            binaryWriter.Write(item.Value.m_Count);
        //            //    isequip(boolean)
        //            binaryWriter.Write(item.Value.m_IsEquip);
        //        }
        //    }

        //}

        //// 생성된 Tuple List를 각 Name.bytes 파일로 저장
        //byte[] binaryFile = memoryStream.ToArray();

        //if (File.Exists(m_DataPath))
        //{
        //    File.Delete(m_DataPath);
        //}
        //FileStream fileStream = new FileStream(m_DataPath, FileMode.Create);
        //fileStream.Write(binaryFile, 0, binaryFile.Length);

        //fileStream.Close();
        //binaryWriter.Close();
    }


}
