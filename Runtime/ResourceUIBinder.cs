using UnityEngine;
using TMPro;

public class ResourceUIBinder : MonoBehaviour
{
    public ResourceDefinition resource;
    public TextMeshProUGUI text;

    void Start()
    {
        UpdateUI(resource.id,
            ResourceManager.Instance.Get(resource));

        ResourceManager.Instance.Subscribe(UpdateUI);
    }

    void UpdateUI(string id, int value)
    {
        if (id != resource.id)
            return;

        text.text = value.ToString();
    }
}