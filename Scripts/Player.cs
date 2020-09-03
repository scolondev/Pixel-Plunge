using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHandler playerHandler;
    public Menu menu;

    public SpriteRenderer playerSprite;

    public int essence;
    public int coins;

    public UI_Counter essenceCounter;
    public UI_Counter coinCounter;

    public void Start()
    {
        playerHandler.ReadData();
        playerSprite.color = playerHandler.playerData.personalColor;
    }

    public void UpdateUI()
    {
        essenceCounter.count.text = essence.ToString();
        coinCounter.count.text = coins.ToString();
    }
}
