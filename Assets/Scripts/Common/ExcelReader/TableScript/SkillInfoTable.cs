using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfoInfo
{
    public string m_prevPath { get; private set; }
    public int m_needParts { get; private set; }
    public string m_skillName { get; private set; }
    public string m_description { get; private set; }
    public int m_value { get; private set; }
    public string m_spriteName { get; private set; }

    public void SetprevPath(string prevPath) { m_prevPath = prevPath; }
    public void SetneedParts(int needParts) { m_needParts = needParts; }
    public void SetskillName(string skillName) { m_skillName = skillName; }
    public void Setdescription(string description) { m_description = description; }
    public void Setvalue(int value) { m_value = value; }
    public void SetspriteName(string spriteName) { m_spriteName = spriteName; }
}

public class SkillInfoTable : MonoSingleton<SkillInfoTable>
{
    private Dictionary<string, SkillInfoInfo> Table = new Dictionary<string, SkillInfoInfo>();

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }


    private void Start() 
    {
        ReadBinaryTable();
    }
    private void ReadBinaryTable()
    {
        string[] resourceNames = new string[] {"EnergySphereGun", "LinearGun", "ShotGun"};
        foreach(var name in resourceNames)
        {
            TextAsset textAsset = Resources.Load("Tables/SkillInfo/" + name) as TextAsset;
            MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            int tupleCount = binaryReader.ReadInt32();

            for( int i = 0; i < tupleCount; i++)
            {
                SkillInfoInfo info = new SkillInfoInfo();
                string key = binaryReader.ReadString();
                info.SetprevPath(binaryReader.ReadString());
                info.SetneedParts(binaryReader.ReadInt32());
                info.SetskillName(binaryReader.ReadString());
                info.Setdescription(binaryReader.ReadString());
                info.Setvalue(binaryReader.ReadInt32());
                info.SetspriteName(binaryReader.ReadString());

                Table.Add(key, info);
            }
        }
    }

    public Dictionary<string, SkillInfoInfo> GetTable()
    {
        return Table;
    }

    public SkillInfoInfo GetTuple(string key)
    {
        SkillInfoInfo value;

        if (Table.TryGetValue(key, out value))
            return value;

        return null;
    }

}

