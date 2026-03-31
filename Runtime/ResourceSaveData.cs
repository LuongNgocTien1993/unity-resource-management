using System;
using System.Collections.Generic;

[Serializable]
public class ResourceSaveData
{
    public List<Entry> list = new List<Entry>();

    [Serializable]
    public class Entry
    {
        public string id;
        public int amount;
    }

    public ResourceSaveData(Dictionary<string,int> dic)
    {
        foreach (var pair in dic)
        {
            list.Add(new Entry()
            {
                id = pair.Key,
                amount = pair.Value
            });
        }
    }

    public Dictionary<string,int> ToDictionary()
    {
        Dictionary<string,int> dic = new Dictionary<string,int>();

        foreach (var e in list)
            dic[e.id] = e.amount;

        return dic;
    }
}