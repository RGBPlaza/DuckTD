using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Environment : MonoBehaviour
{

    public bool deselectDuck = true;
    public bool hideInventory = true;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (DuckManager.Instance.IsDuckSelected && deselectDuck)
                DuckManager.Instance.Deselect();
            if (Inventory.Instance.IsShowing && hideInventory)
                Inventory.Instance.Hide();
        }
    }
}
