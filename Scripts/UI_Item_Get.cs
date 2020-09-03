using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item_Get : MonoBehaviour
{
    public Text itemName;
    public Text description;
    public Image icon;

    public Animator anim;

    public static UI_Item_Get instance;
    public void Awake()
    {
        instance = this;
    }

    public void UpdateDisplay(Item item)
    {
        itemName.text = item.name;
        description.text = item.description;
        icon.sprite = item.icon;

        anim.Play("get",-1,0);
    }
}
