using System;
using System.Collections.Generic;

[Serializable]
public class ResourceWallet
{
    private Dictionary<string, int> resources = new Dictionary<string, int>();

    public event Action<string, int> OnResourceChanged;

    public int Get(string id)
    {
        if (resources.TryGetValue(id, out int value))
            return value;

        return 0;
    }

    public void Add(string id, int amount)
    {
        if (!resources.ContainsKey(id))
            resources[id] = 0;

        resources[id] += amount;

        OnResourceChanged?.Invoke(id, resources[id]);
    }

    public bool Spend(string id, int amount)
    {
        if (Get(id) < amount)
            return false;

        resources[id] -= amount;

        OnResourceChanged?.Invoke(id, resources[id]);

        return true;
    }

    public Dictionary<string, int> GetAll()
    {
        return resources;
    }

    public void Load(Dictionary<string, int> data)
    {
        resources = data;
    }
}