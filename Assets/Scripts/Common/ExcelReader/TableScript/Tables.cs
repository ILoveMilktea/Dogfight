using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusInfo
{
    public string m_name { get; private set; }
    public int m_hp { get; private set; }
    public int m_atk { get; private set; }
    public int m_dropParts { get; private set; }

    public void Setname(string name) { m_name = name; }
    public void Sethp(int hp) { m_hp = hp; }
    public void Setatk(int atk) { m_atk = atk; }
    public void SetdropParts(int dropParts) { m_dropParts = dropParts; }
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
            info.SetdropParts(binaryReader.ReadInt32());

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
public class StageEnemyInfo
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

public class StageEnemyTable
{
    public StageEnemyTable(int stageNumber)
    {
        ReadBinaryTable(stageNumber);
    }

    private Dictionary<int, StageEnemyInfo> Table = new Dictionary<int, StageEnemyInfo>();

    private void ReadBinaryTable(int stageNumber)
    {
        string resourceName = "Stage_" + stageNumber.ToString() + "_Enemy";
        TextAsset textAsset = Resources.Load("Tables/EnemyInfo/" + resourceName) as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tupleCount = binaryReader.ReadInt32();

        for( int i = 0; i < tupleCount; i++)
        {
            StageEnemyInfo info = new StageEnemyInfo();
            int key = binaryReader.ReadInt32();
            info.SetserialNumber(binaryReader.ReadInt32());
            info.Setlevel(binaryReader.ReadInt32());
            info.SetposX(binaryReader.ReadSingle());
            info.SetposY(binaryReader.ReadSingle());

            Table.Add(key, info);
        }
    }

    public Dictionary<int, StageEnemyInfo> GetTable()
    {
        return Table;
    }

    public StageEnemyInfo GetTuple(int key)
    {
        StageEnemyInfo value;

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

    public EnemyStatusTable EnemyStatus = null;
    public Dictionary<int, StageEnemyTable> StageEnemyTables = null;

    private void Start() 
    {
        EnemyStatus = new EnemyStatusTable();
        StageEnemyTables = new Dictionary<int, StageEnemyTable>();
        for(int i = 1; i <= 2; i++)
        {
            StageEnemyTable table = new StageEnemyTable(i);
            StageEnemyTables.Add(i, table);
        }
    }
}

