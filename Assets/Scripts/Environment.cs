using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private void OnMouseDown()
    {
        DuckManager.Deselect();
    }
}
