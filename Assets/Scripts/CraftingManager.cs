using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    private Recipe[] recipes;
    private void Awake()
    {
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(Recipe)}", new string[] { "Assets/Items/Recipes" });
        recipes = new Recipe[guids.Length];
        for (int i = 0; i < guids.Length; i++)
            recipes[i] = AssetDatabase.LoadAssetAtPath<Recipe>(AssetDatabase.GUIDToAssetPath(guids[i]));
        recipes = recipes.OrderBy(x => x.ID).ToArray();

        Instance = this;
    }

}
