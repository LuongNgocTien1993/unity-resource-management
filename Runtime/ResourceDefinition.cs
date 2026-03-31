using UnityEngine;

[CreateAssetMenu(menuName = "Game/Resource")]
public class ResourceDefinition : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
}