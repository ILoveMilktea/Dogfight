using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public static class JsonHelper
//{
//    public static T[] FromJson<T>(string json)
//    {
//        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
//        return wrapper.items;
//    }
//​
//    public static string ToJson<T>(T[] array)
//    {
//        Wrapper<T> wrapper = new Wrapper<T>();
//        wrapper.items = array;
//        return JsonUtility.ToJson(wrapper);
//    }
//​
//    public static string ToJson<T>(T[] array, bool prettyPrint)
//    {
//        Wrapper<T> wrapper = new Wrapper<T>();
//        wrapper.items = array;
//        return JsonUtility.ToJson(wrapper, prettyPrint);
//    }

//    private class Wrapper<T>
//    {
//        public T[] items;
//    }
//}

[Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    public List<TKey> keys;
    public List<TValue> values;

    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
    }
}
