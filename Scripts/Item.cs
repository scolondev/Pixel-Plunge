using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;

    public Sprite icon;
    public GameObject instance;

    public virtual void Use(GameObject player)
    {
        Debug.Log("Using a normal item...");
    }
}
