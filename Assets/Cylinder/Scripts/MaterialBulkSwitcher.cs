using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableList<T>
{
    public List<T> list;
}

[Serializable]
public class SerializableList2D<T>
{
    public List<SerializableList<T>> lists;

    public List<T> this[int row]
    {
        get => lists[row].list;
        set => lists[row].list = new List<T>(value);
    }
}

public class MaterialBulkSwitcher : MonoBehaviour
{
    public SerializableList2D<Material> materials;
    public Renderer[] renderers;

    public void SetMaterials(int setIdx)
    {
        foreach (var r in renderers)
        {
            r.SetMaterials(materials[setIdx]);
        }
    }
}