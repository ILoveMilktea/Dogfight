using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListInfo
{
    public string m_name { get; private set; }
    public int m_hp { get; private set; }
    public int m_atk { get; private set; }

    public void Setname(string name) { m_name = name; }
    public void Sethp(int hp) { m_hp = hp; }
    public void Setatk(int atk) { m_atk = atk; }
}

public class EnemyListTable
{
    public EnemyListTable()
    {
        ReadBinaryTable();
    }

    private static Dictionary<int, EnemyListInfo> Table = new Dictionary<int, EnemyListInfo>();

    private void ReadBinaryTable()
    {
        TextAsset textAsset = Resources.Load("Tables/testdb/EnemyList") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tupleCount = binaryReader.ReadInt32();

        for( int i = 0; i < tupleCount; i++)
        {
            EnemyListInfo info = new EnemyListInfo();
            int key = binaryReader.ReadInt32();
            info.Setname(binaryReader.ReadString());
            info.Sethp(binaryReader.ReadInt32());
            info.Setatk(binaryReader.ReadInt32());

            Table.Add(key, info);
        }
    }

    public static Dictionary<int, EnemyListInfo> GetTable()
    {
        return Table;
    }

    public static EnemyListInfo GetTuple(int key)
    {
        EnemyListInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

}
public class EnemyStatusInfo
{
    public string m_name { get; private set; }
    public int m_hp { get; private set; }
    public int m_atk { get; private set; }

    public void Setname(string name) { m_name = name; }
    public void Sethp(int hp) { m_hp = hp; }
    public void Setatk(int atk) { m_atk = atk; }
}

public class EnemyStatusTable
{
    public EnemyStatusTable()
    {
        ReadBinaryTable();
    }

    private static Dictionary<int, EnemyStatusInfo> Table = new Dictionary<int, EnemyStatusInfo>();

    private void ReadBinaryTable()
    {
        TextAsset textAsset = Resources.Load("Tables/EnemyInfo/EnemyStatus") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tupleCount = binaryReader.ReadInt32();

        for( int i = 0; i < tupleCount; i++)
        {
            EnemyStatusInfo info = new EnemyStatusInfo();
            int key = binaryReader.ReadInt32();
            info.Setname(binaryReader.ReadString());
            info.Sethp(binaryReader.ReadInt32());
            info.Setatk(binaryReader.ReadInt32());

            Table.Add(key, info);
        }
    }

    public static Dictionary<int, EnemyStatusInfo> GetTable()
    {
        return Table;
    }

    public static EnemyStatusInfo GetTuple(int key)
    {
        EnemyStatusInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

}
public class Stage1Info
{
    public int m_serialNumber { get; private set; }
    public int m_level { get; private set; }
    public float m_posX { get; private set; }
    public float m_posY { get; private set; }

    public void SetserialNumber(int serialNumber) { m_serialNumber = serialNumber; }
    public void Setlevel(int level) { m_level = level; }
    public void SetposX(float posX) { m_posX = posX; }
    public void SetposY(float posY) { m_posY = posY; }
}

public class Stage1Table
{
    public Stage1Table()
    {
        ReadBinaryTable();
    }

    private static Dictionary<string, Stage1Info> Table = new Dictionary<string, Stage1Info>();

    private void ReadBinaryTable()
    {
        TextAsset textAsset = Resources.Load("Tables/EnemyInfo/Stage1") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tupleCount = binaryReader.ReadInt32();

        for( int i = 0; i < tupleCount; i++)
        {
            Stage1Info info = new Stage1Info();
            string key = binaryReader.ReadString();
            info.SetserialNumber(binaryReader.ReadInt32());
            info.Setlevel(binaryReader.ReadInt32());
            info.SetposX(binaryReader.ReadSingle());
            info.SetposY(binaryReader.ReadSingle());

            Table.Add(key, info);
        }
    }

    public static Dictionary<string, Stage1Info> GetTable()
    {
        return Table;
    }

    public static Stage1Info GetTuple(string key)
    {
        Stage1Info value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

}
public class Stage2Info
{
    public int m_serialNumber { get; private set; }
    public int m_level { get; private set; }
    public float m_posX { get; private set; }
    public float m_posY { get; private set; }

    public void SetserialNumber(int serialNumber) { m_serialNumber = serialNumber; }
    public void Setlevel(int level) { m_level = level; }
    public void SetposX(float posX) { m_posX = posX; }
    public void SetposY(float posY) { m_posY = posY; }
}

public class Stage2Table
{
    public Stage2Table()
    {
        ReadBinaryTable();
    }

    private static Dictionary<string, Stage2Info> Table = new Dictionary<string, Stage2Info>();

    private void ReadBinaryTable()
    {
        TextAsset textAsset = Resources.Load("Tables/EnemyInfo/Stage2") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tupleCount = binaryReader.ReadInt32();

        for( int i = 0; i < tupleCount; i++)
        {
            Stage2Info info = new Stage2Info();
            string key = binaryReader.ReadString();
            info.SetserialNumber(binaryReader.ReadInt32());
            info.Setlevel(binaryReader.ReadInt32());
            info.SetposX(binaryReader.ReadSingle());
            info.SetposY(binaryReader.ReadSingle());

            Table.Add(key, info);
        }
    }

    public static Dictionary<string, Stage2Info> GetTable()
    {
        return Table;
    }

    public static Stage2Info GetTuple(string key)
    {
        Stage2Info value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

}

public class Tables : MonoSingleton<Tables>
{
    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    public EnemyListTable EnemyList = null;
    public EnemyStatusTable EnemyStatus = null;
    public Stage1Table Stage1 = null;
    public Stage2Table Stage2 = null;

    private void Start() 
    {
        EnemyList = new EnemyListTable();
        EnemyStatus = new EnemyStatusTable();
        Stage1 = new Stage1Table();
        Stage2 = new Stage2Table();
    }
}

