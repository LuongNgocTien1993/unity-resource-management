using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    ResourceWallet wallet = new ResourceWallet();

    const string SAVE_KEY = "RESOURCE_DATA";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Load();
    }

    public int Get(ResourceDefinition res)
    {
        return wallet.Get(res.id);
    }

    public void Add(ResourceDefinition res, int amount)
    {
        wallet.Add(res.id, amount);
        Save();
    }

    public bool Spend(ResourceDefinition res, int amount)
    {
        bool ok = wallet.Spend(res.id, amount);

        if (ok)
            Save();

        return ok;
    }

    public void Subscribe(System.Action<string,int> action)
    {
        wallet.OnResourceChanged += action;
    }

    void Save()
    {
        var data = new ResourceSaveData(wallet.GetAll());

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    void Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);

        var data = JsonUtility.FromJson<ResourceSaveData>(json);

        wallet.Load(data.ToDictionary());
    }
}