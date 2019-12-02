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
public class GunDefaultStatusInfo
{
    public string m_fireMode { get; private set; }
    public string m_skillMode { get; private set; }
    public float m_msBetweenShots { get; private set; }
    public float m_muzzleVelocity { get; private set; }
    public float m_maxRange { get; private set; }
    public int m_damage { get; private set; }
    public float m_knonkBackForce { get; private set; }
    public int m_directionNumber { get; private set; }
    public float m_projectileMaxAngle { get; private set; }

    public void SetfireMode(string fireMode) { m_fireMode = fireMode; }
    public void SetskillMode(string skillMode) { m_skillMode = skillMode; }
    public void SetmsBetweenShots(float msBetweenShots) { m_msBetweenShots = msBetweenShots; }
    public void SetmuzzleVelocity(float muzzleVelocity) { m_muzzleVelocity = muzzleVelocity; }
    public void SetmaxRange(float maxRange) { m_maxRange = maxRange; }
    public void Setdamage(int damage) { m_damage = damage; }
    public void SetknonkBackForce(float knonkBackForce) { m_knonkBackForce = knonkBackForce; }
    public void SetdirectionNumber(int directionNumber) { m_directionNumber = directionNumber; }
    public void SetprojectileMaxAngle(float projectileMaxAngle) { m_projectileMaxAngle = projectileMaxAngle; }
}

public class GunDefaultStatusTable
{
    public GunDefaultStatusTable()
    {
        ReadBinaryTable();
    }

    private Dictionary<string, GunDefaultStatusInfo> Table = new Dictionary<string, GunDefaultStatusInfo>();

    private void ReadBinaryTable()
    {
        TextAsset textAsset = Resources.Load("Tables/PrefabInfo/GunDefaultStatus") as TextAsset;
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryReader binaryReader = new BinaryReader(memoryStream);

        int tupleCount = binaryReader.ReadInt32();

        for( int i = 0; i < tupleCount; i++)
        {
            GunDefaultStatusInfo info = new GunDefaultStatusInfo();
            string key = binaryReader.ReadString();
            info.SetfireMode(binaryReader.ReadString());
            info.SetskillMode(binaryReader.ReadString());
            info.SetmsBetweenShots(binaryReader.ReadSingle());
            info.SetmuzzleVelocity(binaryReader.ReadSingle());
            info.SetmaxRange(binaryReader.ReadSingle());
            info.Setdamage(binaryReader.ReadInt32());
            info.SetknonkBackForce(binaryReader.ReadSingle());
            info.SetdirectionNumber(binaryReader.ReadInt32());
            info.SetprojectileMaxAngle(binaryReader.ReadSingle());

            Table.Add(key, info);
        }
    }

    public Dictionary<string, GunDefaultStatusInfo> GetTable()
    {
        return Table;
    }

    public GunDefaultStatusInfo GetTuple(string key)
    {
        GunDefaultStatusInfo value;

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
    public GunDefaultStatusTable GunDefaultStatus = null;
    public WallStatusTable WallStatus = null;

    private void Start() 
    {
        EnemyStatus = new EnemyStatusTable();
        GunDefaultStatus = new GunDefaultStatusTable();
        WallStatus = new WallStatusTable();
    }
}

