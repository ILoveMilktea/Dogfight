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

    private Dictionary<int, EnemyStatusInfo> Table = new Dictionary<int, EnemyStatusInfo>();

    private void ReadBinaryTable()
    {
        TextAsset textAsset = Resources.Load("Tables/PrefabInfo/EnemyStatus") as TextAsset;
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

    public Dictionary<int, EnemyStatusInfo> GetTable()
    {
        return Table;
    }

    public EnemyStatusInfo GetTuple(int key)
    {
        EnemyStatusInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

}
public class WallStatusInfo
{
    public string m_name { get; private set; }

    public void Setname(string name) { m_name = name; }
}

public class WallStatusTable
{
    public WallStatusTable()
    {
        ReadBinaryTable();
    }

    private Dictionary<int, WallStatusInfo> Table = new Dictionary<int, WallStatusInfo>();

    private void ReadBinaryTable()
    {
        TextAsset textAsset = Resources.Load("Tables/PrefabInfo/WallStatus") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tupleCount = binaryReader.ReadInt32();

        for( int i = 0; i < tupleCount; i++)
        {
            WallStatusInfo info = new WallStatusInfo();
            int key = binaryReader.ReadInt32();
            info.Setname(binaryReader.ReadString());

            Table.Add(key, info);
        }
    }

    public Dictionary<int, WallStatusInfo> GetTable()
    {
        return Table;
    }

    public WallStatusInfo GetTuple(int key)
    {
        WallStatusInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

}

public class Tables : MonoSingleton<Tables>
{
    protected override void Init() 
    {
        DontDestroyOnLoad(gameObject);
    }

    public EnemyStatusTable EnemyStatus = null;
    public WallStatusTable WallStatus = null;

    private void Start() 
    {
        EnemyStatus = new EnemyStatusTable();
        WallStatus = new WallStatusTable();
    }
}

