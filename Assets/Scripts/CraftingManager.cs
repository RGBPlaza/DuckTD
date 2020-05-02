using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    private Recipe[] recipes;
    private void Awake()
    {
        recipes = Resources.LoadAll<Recipe>("Recipes").OrderBy(x => x.ID).ToArray();
        Instance = this;
    }

}
