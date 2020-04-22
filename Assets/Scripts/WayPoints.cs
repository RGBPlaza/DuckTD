using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

    public static Transform[] Points;

    private void Awake()
    {
        int childCount = transform.childCount;
        Points = new Transform[childCount];
        for (int i = 0; i < childCount; i++) Points[i] = transform.GetChild(i);
    }
}
