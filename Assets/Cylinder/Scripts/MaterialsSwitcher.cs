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

public class MaterialsSwitcher : MonoBehaviour
{
    public SerializableList2D<Material> materials;
    private readonly List<Renderer> _renderers = new();

    public void SetMaterials(int setIdx)
    {
        foreach (var r in _renderers)
        {
            r.SetMaterials(materials[setIdx]);
        }
    }

    public void AddRenderer(Renderer r)
    {
        _renderers.Add(r);
    }
}