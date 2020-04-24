using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{

    private void OnMouseDown()
    {
        if(DuckManager.Instance.IsDuckSelected && Input.mousePosition.y > 126)
            DuckManager.Instance.Deselect();
    }
}
