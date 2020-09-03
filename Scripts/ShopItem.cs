using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string name = "New Shop Item";
    public string desc = "New Shop Item Description";
    public Sprite icon;
    public float price = 0;
    public bool purchased = false;
}
