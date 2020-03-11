using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

    public new string name;
    public string description;
    public string itemType;
    public int buyPrice;
    public int sellPrice;

    public Sprite image;

    public Item()
    {

    }
}
