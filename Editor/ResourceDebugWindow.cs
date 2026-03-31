
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ResourceDebugWindow : EditorWindow
{
    ResourceDefinition resource;
    int amount = 10;

    [MenuItem("Tools/Resource Debug Window")]
    public static void ShowWindow()
    {
        GetWindow<ResourceDebugWindow>("Resource Debug");
    }

    void OnGUI()
    {
        GUILayout.Space(10);

        GUILayout.Label("Resource Debug Tool", EditorStyles.boldLabel);

        resource = (ResourceDefinition)EditorGUILayout.ObjectField(
            "Resource",
            resource,
            typeof(ResourceDefinition),
            false
        );

        amount = EditorGUILayout.IntField("Amount", amount);

        GUILayout.Space(10);

        GUI.enabled = resource != null && Application.isPlaying;

        if (GUILayout.Button("Add Resource"))
        {
            ResourceManager.Instance.Add(resource, amount);
        }

        if (GUILayout.Button("Spend Resource"))
        {
            ResourceManager.Instance.Spend(resource, amount);
        }

        if (GUILayout.Button("Set Resource"))
        {
            int current = ResourceManager.Instance.Get(resource);
            int delta = amount - current;

            if (delta > 0)
                ResourceManager.Instance.Add(resource, delta);
            else
                ResourceManager.Instance.Spend(resource, -delta);
        }

        GUI.enabled = true;

        GUILayout.Space(20);

        if (Application.isPlaying && resource != null)
        {
            int current = ResourceManager.Instance.Get(resource);
            GUILayout.Label("Current Amount: " + current);
        }
        else
        {
            GUILayout.Label("Enter Play Mode to test.");
        }
    }
}
#endif
