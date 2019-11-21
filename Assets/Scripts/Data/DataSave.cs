using System.Reflection;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave
{
    public void SaveUserData(string directoryPath)
    {
        SavePlayInfo(directoryPath + Const_Path.playInfoPath);
        SavePlayerStatusInfo(directoryPath + Const_Path.playerStatusInfoPath);
        SaveWeaponInfo(directoryPath + Const_Path.WeaponInfoPath);
    }

    private void SavePlayInfo(string dataPath)
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

        PlayInfo info = DataManager.Instance.GetPlayInfo;
        PropertyInfo[] properties = info.GetType().GetProperties();

        foreach (PropertyInfo item in properties)
        {
            binaryWriter = FindTypeAndWrite(binaryWriter, item.GetValue(info));
        }

        // 생성된 Tuple List를 각 Name.bytes 파일로 저장
        SaveToStorage(dataPath, memoryStream.ToArray());

        binaryWriter.Close();
        memoryStream.Close();
    }
    private void SavePlayerStatusInfo(string dataPath)
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

        PlayerStatusInfo info = DataManager.Instance.GetPlayerStatus;
        PropertyInfo[] properties = info.GetType().GetProperties();

        foreach (PropertyInfo item in properties)
        {
            binaryWriter = FindTypeAndWrite(binaryWriter, item.GetValue(info));
        }

        // 생성된 Tuple List를 각 Name.bytes 파일로 저장
        SaveToStorage(dataPath, memoryStream.ToArray());

        binaryWriter.Close();
        memoryStream.Close();
    }
    private void SaveWeaponInfo(string dataPath)
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

        Dictionary<WeaponType, WeaponInfo> weapons = DataManager.Instance.GetWeapons;

        foreach(var weapon in weapons)
        {
            binaryWriter = SaveWeapon(binaryWriter, weapon.Value);
        }

        // 생성된 Tuple List를 각 Name.bytes 파일로 저장
        SaveToStorage(dataPath, memoryStream.ToArray());

        binaryWriter.Close();
        memoryStream.Close();
    }
    private BinaryWriter SaveWeapon(BinaryWriter note, WeaponInfo weapon)
    {
        PropertyInfo[] properties = weapon.GetType().GetProperties();

        foreach (PropertyInfo item in properties)
        {
            if (item.Equals(typeof(Dictionary<int, WeaponSkill>)))
            {
                Dictionary<int, WeaponSkill> skillList = (Dictionary<int, WeaponSkill>)item.GetValue(weapon);
                
                foreach(var skill in skillList)
                {
                    note.Write(skill.Key);
                    SaveSkillList(note, skill.Value);
                }
            }
            else
            {
                note = FindTypeAndWrite(note, item.GetValue(weapon));
            }

        }
        return note;
    }
    private BinaryWriter SaveSkillList(BinaryWriter note, WeaponSkill skillList)
    {
        PropertyInfo[] properties = skillList.GetType().GetProperties();

        foreach (PropertyInfo item in properties)
        {
            FindTypeAndWrite(note, item.GetValue(skillList));
        }

        return note;
    }

    private BinaryWriter FindTypeAndWrite(BinaryWriter note, object item)
    {
        if (item.Equals(typeof(int)))
        {
            note.Write((int)item);
        }
        else if (item.Equals(typeof(float)))
        {
            note.Write((float)item);
        }
        else if (item.Equals(typeof(bool)))
        {
            note.Write((bool)item);
        }
        else if (item.Equals(typeof(string)))
        {
            note.Write((string)item);
        }
        else if (item.Equals(typeof(byte)))
        {
            note.Write((byte)item);
        }
        else
        {
            Debug.Log("Cannot find type");
        }

        return note;
    }


    private void SaveToStorage(string path, byte[] binaryFile)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        FileStream fileStream = new FileStream(path, FileMode.Create);
        fileStream.Write(binaryFile, 0, binaryFile.Length);
        fileStream.Close();
    }
}