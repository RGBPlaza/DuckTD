using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatalogue : MonoBehaviour
{
    private static Item[] items;
    public static Item GetForID(int id) => items[id];

    public Item[] Items;
    private void Awake()
    {
        items = Items;
    }
}
