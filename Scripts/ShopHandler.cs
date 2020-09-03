using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
 
    public static ShopHandler instance;
    string filename = "shop.json";
    string path;

   //All stock in the game
    public ShopData shopData;
    
    public void Awake()
    {
        instance = this;
        path = Application.persistentDataPath + "/" + filename;
        ReadData();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            shopData = new ShopData();
            SaveData();
        }
    }

    public void Purchase(ShopItem purchase){
        int index = shopData.stock.IndexOf(purchase);
        shopData.stock[index].purchased = true;
        SaveData();
    }

    public void SaveData()
    {
        ShopWrapper wrapper = new ShopWrapper();
        wrapper.shopData = shopData;

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(path, contents);
    }

    public void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {

                string contents = System.IO.File.ReadAllText(path);
                ShopWrapper wrapper = JsonUtility.FromJson<ShopWrapper>(contents);
                if(wrapper.shopData.stock.Count < 1)
                {
                    SaveData();
                } else
                {
                    shopData = wrapper.shopData;
                }
            }
            else
            {
                Debug.Log("Unable to read the save data, file does not exist... creating new data");
                
                //If there's no data then we need to create some new data.
                //If there's no stock already premade, we create some empty data.
                //This should not happen as we can set up the default instance of stock in the inspector.

          
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
