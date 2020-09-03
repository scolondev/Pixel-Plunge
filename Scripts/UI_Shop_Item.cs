using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop_Item : MonoBehaviour
{
    //FOr displaying all information in a particular item;
    public ShopItem shopItem;

    public Text _name;
    public Text desc;
    public Text price;
    public Image icon;

    public void UpdateDisplay(){
        _name.text = shopItem.name;
        desc.text = shopItem.desc;
        price.text = shopItem.price.ToString();
        icon.sprite = shopItem.icon;
    }
}
