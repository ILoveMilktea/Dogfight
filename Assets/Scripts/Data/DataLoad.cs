using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoad
{
    public void LoadUserData(string directoryPath)
    {
        LoadPlayInfo(directoryPath + Const_Path.playInfoPath);
        LoadPlayerStatusInfo(directoryPath + Const_Path.playerStatusInfoPath);
        LoadWeaponInfo(directoryPath + Const_Path.WeaponInfoPath);
    }


    private void LoadPlayInfo(string dataPath)
    {
        byte[] saveFile = File.ReadAllBytes(dataPath);
        MemoryStream memoryStream = new MemoryStream(saveFile);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        PlayInfo info = DataManager.Instance.GetPlayInfo;

        SerializeReadData(binaryReader, info);

        binaryReader.Close();
        memoryStream.Close();
    }
    private void LoadPlayerStatusInfo(string dataPath)
    {
        byte[] saveFile = File.ReadAllBytes(dataPath);
        MemoryStream memoryStream = new MemoryStream(saveFile);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        PlayerStatusInfo info = DataManager.Instance.GetPlayerStatus;

        SerializeReadData(binaryReader, info);

        binaryReader.Close();
        memoryStream.Close();
    }
    private void LoadWeaponInfo(string dataPath)
    {
        byte[] saveFile = File.ReadAllBytes(dataPath);
        MemoryStream memoryStream = new MemoryStream(saveFile);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        Dictionary<WeaponType,WeaponInfo> weapons = DataManager.Instance.GetWeapons;

        foreach(var weapon in weapons)
        {
            LoadWeapon(binaryReader, weapon.Value);
        }

        binaryReader.Close();
        memoryStream.Close();
    }
    private void LoadWeapon(BinaryReader note, WeaponInfo weapon)
    {

    }
    private BinaryWriter LoadSkillList(BinaryReader note, WeaponSkill skillList)
    {
        return null;
    }

    private void SerializeReadData(BinaryReader note, object info)
    {
        PropertyInfo[] properties = info.GetType().GetProperties();

        foreach (PropertyInfo item in properties)
        {
            MethodInfo SetMethod = info.GetType().GetMethod("Set" + item.Name, new Type[] { item.PropertyType });

            SetMethod.Invoke(info, FindTypeAndRead(note, item));
            ucj
                .3.3
            if (item.Equals(typeof(int)))
            {
                SetMethod.Invoke(info, new object[] { note.ReadInt32() });
            }
            else if (item.Equals(typeof(float)))
            {
                SetMethod.Invoke(info, new object[] { note.ReadSingle() });
            }
            else if (item.Equals(typeof(bool)))
            {
                SetMethod.Invoke(info, new object[] { note.ReadBoolean() });
            }
            else if (item.Equals(typeof(string)))
            {
                SetMethod.Invoke(info, new object[] { note.ReadString() });
            }
            else if (item.Equals(typeof(byte)))
            {
                SetMethod.Invoke(info, new object[] { note.ReadByte() });
            }
            else
            {
                Debug.Log("Cannot find type");
            }
        }
    }

    private object[] FindTypeAndRead(BinaryReader note, object item)
    {

        if (item.Equals(typeof(int)))
        {
            return new object[] { note.ReadInt32() };
        }
        else if (item.Equals(typeof(float)))
        {
            return new object[] { note.ReadSingle() };
        }
        else if (item.Equals(typeof(bool)))
        {
            return new object[] { note.ReadBoolean() };
        }
        else if (item.Equals(typeof(string)))
        {
            return new object[] { note.ReadString() };
        }
        else if (item.Equals(typeof(byte)))
        {
            return new object[] { note.ReadByte() };
        }
        else
        {
            Debug.Log("Cannot find type");
            return null;
        }
    }


    /// <summary>
    /// dataPath상 storage에 저장된 binary 파일을 읽는 함수
    /// </summary>
    /// <param name="dataPath"></param>
    private void LoadAllFromStorage()
    {
        byte[] saveFile = File.ReadAllBytes(m_DataPath);
        MemoryStream memoryStream = new MemoryStream(saveFile);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        Dictionary<string, SaveDataInfo> dataSlots = new Dictionary<string, SaveDataInfo>();
        int slotCount = binaryReader.ReadInt32();

        for (int i = 0; i < slotCount; i++)
        {
            SaveDataInfo info = new SaveDataInfo();
            info.SetSlotName(binaryReader.ReadString());
            info.SetIsDataExist(binaryReader.ReadBoolean());
            if (info.m_IsDataExist)
            {

                info.SetChapter(binaryReader.ReadString());
                info.SetDate(binaryReader.ReadInt64());
                info.SetCharacterType(binaryReader.ReadString());
                info.SetLevel(binaryReader.ReadInt32());
                info.SetPlayTime(binaryReader.ReadSingle());
                info.SetArea(binaryReader.ReadInt32());
                Vector3 pos = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                info.SetPlayerPosition(pos);

                int InventoryItemsCount = binaryReader.ReadInt32();
                Dictionary<string, InventoryItemInfo> inventoryItems = new Dictionary<string, InventoryItemInfo>();
                for (int j = 0; j < InventoryItemsCount; j++)
                {
                    InventoryItemInfo item = new InventoryItemInfo();

                    string name = binaryReader.ReadString();
                    item.SetIcon(name);
                    item.SetItemInfo(name);
                    item.SetCount(binaryReader.ReadInt32());
                    item.SetIsEquip(binaryReader.ReadBoolean());

                    inventoryItems.Add(name, item);
                }
                info.SetInventoryItems(inventoryItems);

            }

            dataSlots.Add(info.m_SlotName, info);
        }

        m_DataSlots = dataSlots;
    }

    /// <summary>
    /// 게임 실행중에 다른 파일을 읽을때 호출합니다.
    /// </summary>
    /// <param name="slotName"></param>
    public void Load(string slotName)
    {
        SaveDataInfo info = GetSlotInfo(slotName);

        if (info.m_IsDataExist)
        {
            // 저장하지 않은 내용은 사라집니다, 이 데이터를 읽겠습니까 팝업?
            m_Chapter = info.m_Chapter;
            m_CharacterType = info.m_CharacterType;
            m_Level = info.m_Level;
            m_PlayTime = info.m_PlayTime;
            m_AreaNumber = info.m_AreaNumber;
            m_PlayerPosition = info.m_PlayerPosition;

            m_CurrentInventoryItems = info.m_InventoryItems;
            SceneManager.LoadScene(AreaTable.GetTuple(m_AreaNumber).m_SceneName);
        }
    }


}
