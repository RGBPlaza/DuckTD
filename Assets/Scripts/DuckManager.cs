using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckManager : MonoBehaviour
{
    public static bool IsPlacingDuck { get; private set; } = false;
    public static bool IsDuckSelected { get => selectedDuck != null; }

    private static Duck selectedDuck;

    public void ToppingButtonPressed()
    {
        IsPlacingDuck = true;
    }

    public static void ToppingBuilt()
    {
        IsPlacingDuck = false;
    }

    public static void Select(Duck duck)
    {
        selectedDuck = duck;
    }

    public static void Deselect()
    {
        selectedDuck = null;
    }
}
