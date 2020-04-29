using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    #region Singleton
    public static InventoryUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject PanelGO;
    public InventorySlot InventorySlotPrefab;

    private List<InventorySlot> slots;

    void Start()
    {
        slots = new List<InventorySlot>();
        Inventory.Instance.OnContentsChanged += InventoryContentsChanged;
    }

    private void InventoryContentsChanged(InventoryEventArgs e)
    {
        if (e.Operation == InventoryOperation.Sort)
        {
            List<Item> itemOrder = Inventory.Instance.ItemOrder;
            foreach (InventorySlot _slot in slots)
                _slot.transform.SetSiblingIndex(itemOrder.IndexOf(_slot.Item));
            return;
        }

        InventorySlot slot = slots.SingleOrDefault(x => x.Item == e.Item);
        if (e.Operation == InventoryOperation.Add && slot == null)
        {
            slot = Instantiate(InventorySlotPrefab, PanelGO.transform);
            slot.Item = e.Item;
            slots.Add(slot);
        }
        else if (e.Operation == InventoryOperation.Remove && e.NewCount == 0)
        {
            Destroy(slot.gameObject);
            slots.Remove(slot);
            return;
        }
        slot.Count = e.NewCount;
    }

    public void Show() => PanelGO.SetActive(true);

    public void Hide() => PanelGO.SetActive(false); 

    public bool IsShowing => PanelGO.activeSelf;

}
