using System;
using DefaultNamespace;
using UnityEngine;

public class Resource : Entity
{
    [SerializeField] public ResourceValues resourceValues;


    public ItemType getDrop()
    {
        return resourceValues.ItemDrop;
    }

    [Serializable]
    public class ResourceValues
    {
        public ItemType ItemDrop;
    }
}