using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public GameObject InventoryPanelGO;
    public GameObject InventorySlotPrefab;

    private Dictionary<int, int> items;
    private List<InventorySlot> slots;

    private void Start()
    {
        items = new Dictionary<int, int>();
        slots = new List<InventorySlot>();
    }

    public void AddItem(int itemID)
    {
        InventorySlot slot;
        if (items.ContainsKey(itemID))
        {
            items[itemID]++;
            slot = slots.Single(x => x.ItemID == itemID);
            slot.Count++;
        }
        else
        {
            items.Add(itemID, 1);
            slot = Instantiate(InventorySlotPrefab, InventoryPanelGO.transform).GetComponent<InventorySlot>();
            slot.ItemID = itemID;
            slot.Count = 1;
            slots.Add(slot);
        }
    }

    public void Show()
    {
        InventoryPanelGO.SetActive(true);
    }

    public void Hide()
    {
        InventoryPanelGO.SetActive(false);
    }

    public bool IsShowing { get => InventoryPanelGO.activeSelf; }

}
