using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Item : MonoBehaviour
{

    public Item item;

    private bool collectible = false;
    private bool collected = false;
    private float wait = 1f;

    private Animator anim;
    private UI_Item_Get itemGet;
    public void Start()
    {
       // this.name = item.name + " pickup";
        anim = GetComponent<Animator>();
        itemGet = UI_Item_Get.instance;
      
        wait = Time.time + wait;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time > wait)
        {
            collectible = true;
        }

        if (collision.CompareTag("player") && collectible && !collected)
        {
            //Play the animation linked with this
            anim.Play("collect");

            GameObject player = GameObject.FindGameObjectWithTag("player");
            item.Use(player);
           
            itemGet.UpdateDisplay(item);
            collected = true;
        }
    }
}
