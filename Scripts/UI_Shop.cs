using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{

    //Data we need for this to run
    private ShopHandler shopHandler;
    private PlayerHandler playerHandler;

    //Display of the current stock.
    //Three Items will be displayed at once.
    public List<UI_Shop_Item> stockObjects;

    //Current Stock
    public List<ShopItem> stock;

    public void Start(){
        //References
        shopHandler = ShopHandler.instance;
        playerHandler = PlayerHandler.instance;
        stock = new List<ShopItem>(shopHandler.shopData.stock);

        PurgeStock();
        DisplayStock();
    }

    //Remove all items from the stock that have already been purchased.
    public void PurgeStock(){
        for(int i = 0; i < stock.Count; i++){
            if(stock[i].purchased == true){
                stock.Remove(stock[i]);
            }
        }
    }

    //Display items currently for sale.
    public void DisplayStock(){
        for(int i = 0; i < stockObjects.Count; i++){
            stockObjects[i].shopItem = stock[i];
            stockObjects[i].UpdateDisplay();
        }
    }

    //If you have enough money to buy an item, buy it.
    public void PurchaseItem(int index){
        if(playerHandler.playerData.coinCount >= float.Parse(stockObjects[index].price.text)){
            playerHandler.playerData.coinCount -= float.Parse(stockObjects[index].price.text);
            playerHandler.SaveData();
            shopHandler.Purchase(stockObjects[index].shopItem);
        }
        else
        {
            Debug.Log("Not enough money");
        }
        PurgeStock();
        DisplayStock();
    }
}
