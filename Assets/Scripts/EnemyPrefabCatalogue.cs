using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabCatalogue : MonoBehaviour
{
    private static GameObject[] enemyPrefabs;
    public static GameObject GetForID(int id) => enemyPrefabs[id];

    public GameObject[] EnemyPrefabs;
    private void Awake()
    {
        enemyPrefabs = EnemyPrefabs;
    }
}
