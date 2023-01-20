using System;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class TownHall : Entity
{
    
    [SerializeField] private TownHallValues _townHallValues;
    
    private Dictionary<ItemType, int> _storage = new Dictionary<ItemType, int>();

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Item item))
        {
            AddToStorage(item);
            Destroy(other.gameObject);
        }
    }

    private void AddToStorage(Item item)
    {
        ItemType type = item.getItemType();
        int quantity = _storage.ContainsKey(type) ? _storage[type] : 0;
        _storage[type] = quantity + 1;

        DisplayStorage();
    }

    private void DisplayStorage()
    {
        string text = "";
        
        foreach(ItemType type in _storage.Keys)
        {
            int quantity = _storage[type];

            text += $"{quantity}x {type}\n";
        }

        _townHallValues.ui.storageText.text = text;
    }

    [Serializable]
    internal class TownHallValues
    {
        [SerializeField] public UI ui;

        [Serializable]
        internal class UI 
        {
            public TextMeshProUGUI storageText;
        }
    } 
    
}
