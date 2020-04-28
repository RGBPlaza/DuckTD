using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Item : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public Sprite IconSprite;
}