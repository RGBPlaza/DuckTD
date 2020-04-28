using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public Text CounterText;

    private Item item;
    private int count;

    public int ItemID { get => item.ID; set { item = ItemCatalogue.GetForID(value); Icon.sprite = item.IconSprite; } }
    public int Count { get => count; set { count = value; CounterText.text = value.ToString(); } }

}
