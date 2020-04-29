using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Key: Item Preset; Value: Quantity in Inventory; 
    private Dictionary<Item, int> items;

    public event Action<InventoryEventArgs> OnContentsChanged;

    private void Start()
    {
        items = new Dictionary<Item, int>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Sort();
    }

    public void Sort()
    {
        items = items.OrderBy(x => x.Key.ID).ToDictionary(pair => pair.Key, pair => pair.Value);
        OnContentsChanged?.Invoke(new InventoryEventArgs(InventoryOperation.Sort));
    }

    public void Add(Item item, int count = 1)
    {
        if (items.ContainsKey(item))
            items[item] += count;
        else
            items.Add(item, count);
        OnContentsChanged?.Invoke(new InventoryEventArgs(InventoryOperation.Add, item, items[item]));
    }

    public void Remove(Item item, int count = 1)
    {
        if (items.ContainsKey(item))
        {
            if (items[item] > count)
                items[item] -= count;
            else
                items.Remove(item);
            OnContentsChanged?.Invoke(new InventoryEventArgs(InventoryOperation.Remove, item, items.ContainsKey(item) ? items[item] : 0));
        }
        else 
            Debug.LogWarning($"You cannot remove {item.Name} because you don't have it");
    }

    public int Count(Item item) => items[item];

    public List<Item> ItemOrder => items.Keys.ToList();


}

public class InventoryEventArgs : EventArgs
{
    public Item Item;
    public int NewCount;
    public InventoryOperation Operation;

    public InventoryEventArgs(InventoryOperation operation, Item item = null, int newCount = 0)
    {
        Operation = operation;
        Item = item;
        NewCount = newCount;
    }
}

public enum InventoryOperation
{
    Add, 
    Remove, 
    Sort
}
