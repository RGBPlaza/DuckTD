using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Recipe")]
public class Recipe : ScriptableObject
{
    public int ID;
    public List<ItemQuantity> Ingredients;
    public ItemQuantity Product; 
    
    public bool CanPerform
    {
        get
        {
            foreach (ItemQuantity itemQuantity in Ingredients)
            {
                if (itemQuantity.Quantity > Inventory.Instance.Count(itemQuantity.Item))
                    return false;
            }
            return true;
        }
    }

    [Serializable]
    public class ItemQuantity
    {
        public Item Item;
        public int Quantity;
    }
}
