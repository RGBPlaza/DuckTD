using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Environment : MonoBehaviour
{

    private void OnMouseDown()
    {
        if(DuckManager.Instance.IsDuckSelected && !EventSystem.current.IsPointerOverGameObject())
            DuckManager.Instance.Deselect();
    }
}
