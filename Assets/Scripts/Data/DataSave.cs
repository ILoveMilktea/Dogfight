//using System.Reflection;
//using System.IO;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DataSave
//{
//    public void SaveUserData(string dataPath, UserData userData)
//    {
//    }

//    /// <summary>
//    /// SaveData.bytes 파일이 없으면 빈걸로 새로 만들자.
//    /// </summary>
//    private void MakeNewStorage()
//    {
//        MemoryStream memoryStream = new MemoryStream();
//        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
        
//        // 생성된 Tuple List를 각 Name.bytes 파일로 저장
//        byte[] binaryFile = memoryStream.ToArray();

//        FileStream fileStream = new FileStream(m_DataPath, FileMode.Create);
//        fileStream.Write(binaryFile, 0, binaryFile.Length);

//        fileStream.Close();
//        binaryWriter.Close();

//        LoadAllFromStorage();
//    }
    

//    /// <summary>
//    /// dataPath상 storage에 저장된 binary 파일을 읽는 함수
//    /// </summary>
//    /// <param name="dataPath"></param>
//    private void LoadAllFromStorage()
//    {
//        byte[] saveFile = File.ReadAllBytes(m_DataPath);
//        MemoryStream memoryStream = new MemoryStream(saveFile);
//        BinaryReader binaryReader = new BinaryReader(memoryStream);

//        Dictionary<string, SaveDataInfo> dataSlots = new Dictionary<string, SaveDataInfo>();
//        int slotCount = binaryReader.ReadInt32();

//        for (int i = 0; i < slotCount; i++)
//        {
//            SaveDataInfo info = new SaveDataInfo();
//            info.SetSlotName(binaryReader.ReadString());
//            info.SetIsDataExist(binaryReader.ReadBoolean());
//            if (info.m_IsDataExist)
//            {

//                info.SetChapter(binaryReader.ReadString());
//                info.SetDate(binaryReader.ReadInt64());
//                info.SetCharacterType(binaryReader.ReadString());
//                info.SetLevel(binaryReader.ReadInt32());
//                info.SetPlayTime(binaryReader.ReadSingle());
//                info.SetArea(binaryReader.ReadInt32());
//                Vector3 pos = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
//                info.SetPlayerPosition(pos);

//                int InventoryItemsCount = binaryReader.ReadInt32();
//                Dictionary<string, InventoryItemInfo> inventoryItems = new Dictionary<string, InventoryItemInfo>();
//                for (int j = 0; j < InventoryItemsCount; j++)
//                {
//                    InventoryItemInfo item = new InventoryItemInfo();

//                    string name = binaryReader.ReadString();
//                    item.SetIcon(name);
//                    item.SetItemInfo(name);
//                    item.SetCount(binaryReader.ReadInt32());
//                    item.SetIsEquip(binaryReader.ReadBoolean());

//                    inventoryItems.Add(name, item);
//                }
//                info.SetInventoryItems(inventoryItems);

//            }

//            dataSlots.Add(info.m_SlotName, info);
//        }

//        m_DataSlots = dataSlots;
//    }

//    /// <summary>
//    /// 게임 실행중에 다른 파일을 읽을때 호출합니다.
//    /// </summary>
//    /// <param name="slotName"></param>
//    public void Load(string slotName)
//    {
//        SaveDataInfo info = GetSlotInfo(slotName);

//        if (info.m_IsDataExist)
//        {
//            // 저장하지 않은 내용은 사라집니다, 이 데이터를 읽겠습니까 팝업?
//            m_Chapter = info.m_Chapter;
//            m_CharacterType = info.m_CharacterType;
//            m_Level = info.m_Level;
//            m_PlayTime = info.m_PlayTime;
//            m_AreaNumber = info.m_AreaNumber;
//            m_PlayerPosition = info.m_PlayerPosition;

//            m_CurrentInventoryItems = info.m_InventoryItems;
//            SceneManager.LoadScene(AreaTable.GetTuple(m_AreaNumber).m_SceneName);
//        }
//    }

//    /// <summary>
//    /// 플레이 타임 체크, 무한루프 돌다가 세이브할때 멈춰줘야함
//    /// </summary>
//    /// <returns></returns>
//    IEnumerator CheckPlayTime()
//    {
//        while (true)
//        {
//            m_PlayTime += Time.deltaTime;
//            yield return null;
//        }
//    }

//    public void QuickSave()
//    {
//        // 퀵세이브를 어느 상황에서 할 것인가가 좀 중요할듯
//        Save("QuickSlot");
//    }

//    /// <summary>
//    /// 게임 실행중에 현재 파일을 저장할 때에 호출합니다.
//    /// </summary>
//    /// <param name="slotName"></param>
//    public void Save(string slotName)
//    {
//        SaveDataInfo info = new SaveDataInfo();

//        info.SetSlotName(slotName);
//        info.SetIsDataExist(true);
//        info.SetChapter(m_Chapter);
//        info.SetDate(DateTime.Now.ToBinary());
//        info.SetCharacterType(m_CharacterType.ToString());
//        info.SetLevel(m_Level);
//        StopCoroutine(CheckPlayTime());
//        info.SetPlayTime(m_PlayTime);
//        info.SetArea(m_AreaNumber);
//        info.SetPlayerPosition(m_PlayerPosition);

//        info.SetInventoryItems(m_CurrentInventoryItems);

//        m_DataSlots[slotName] = info;
//    }

//    public SaveDataInfo GetSlotInfo(string slotName)
//    {
//        return m_DataSlots[slotName];
//    }

//    private BinaryWriter MakeNewSaveFile()
//    {

//    }

//    // 1. User Data
//    // 2. Skill Data
//    // 3. Item Data

//    /// <summary>
//    /// 게임 종료할때 저장 정보들을 storage에 저장합시다.
//    /// </summary>
//    /// <param name="dataPath"></param>
//    public void SaveAllToStorage()
//    {
//        MemoryStream memoryStream = new MemoryStream();
//        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

//        SaveDataInfo info = dataSlot.Value;
//        // slotname(string)
//        binaryWriter.Write(info.m_SlotName);
//        // check empty(bool)
//        binaryWriter.Write(info.m_IsDataExist);

//        if (info.m_IsDataExist)
//        {
//            // chapter(string)
//            binaryWriter.Write(info.m_Chapter);
//            // date(long)
//            binaryWriter.Write(info.m_Date.ToBinary());
//            // charactertype(string)
//            binaryWriter.Write(info.m_CharacterType.ToString());
//            // level(int)
//            binaryWriter.Write(info.m_Level);
//            // playtime(float)
//            binaryWriter.Write(info.m_PlayTime);
//            // scenename(string)
//            binaryWriter.Write(info.m_AreaNumber);
//            // pos(float,float,flaot)
//            binaryWriter.Write(info.m_PlayerPosition.x);
//            binaryWriter.Write(info.m_PlayerPosition.y);
//            binaryWriter.Write(info.m_PlayerPosition.z);
//            // inventoryitemscount(int)
//            binaryWriter.Write(info.m_InventoryItems.Count);
//            foreach (var item in info.m_InventoryItems)
//            {
//                //    itemname(string)
//                binaryWriter.Write(item.Value.m_ItemInfo.m_Name);
//                //    count(int)
//                binaryWriter.Write(item.Value.m_Count);
//                //    isequip(boolean)
//                binaryWriter.Write(item.Value.m_IsEquip);
//            }
//        }
//        // 생성된 Tuple List를 각 Name.bytes 파일로 저장
//        SaveToStorage("path", memoryStream.ToArray());

//        fileStream.Close();
//        binaryWriter.Close();
//    }


//    private void SaveToStorage(string path, byte[] binaryFile)
//    {
//        if (File.Exists(path))
//        {
//            File.Delete(path);
//        }
//        FileStream fileStream = new FileStream(path, FileMode.Create);
//        fileStream.Write(binaryFile, 0, binaryFile.Length);
//    }
//}
