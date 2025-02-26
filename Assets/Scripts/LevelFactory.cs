using UnityEngine;

public class LevelFactory
{
    private const string ParentName = "Level";
        
    private GameObject parent;

    public T CreateLevel<T>(T prefabLevel) where T : Object
    {
        parent ??= CreateParent();
        return Object.Instantiate(prefabLevel, parent.transform);
    }

    private GameObject CreateParent()
    {
        parent = new GameObject();
        parent.name = ParentName;
        parent.transform.position = Vector3.zero;
        return parent;
    }
}