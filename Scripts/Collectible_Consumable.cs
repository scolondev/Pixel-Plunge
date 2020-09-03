using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Consumable : Collectible
{
    public enum Modify
    {
        Essence,
        Coins,
        Health
    }

    public Modify modify;
    public override void Collect()
    {
        base.Collect();

        Player player = GameObject.FindGameObjectWithTag("player_parent").GetComponent<Player>();

        switch (modify)
        {
            case Modify.Essence:
                player.essence += (int)value;
            break;
            case Modify.Coins:
                player.coins += (int)value;
            break;
            case Modify.Health:
                ActorPlayer actor = GameObject.FindGameObjectWithTag("player").GetComponent<ActorPlayer>();
                actor.health.Increment(value);
                actor.healthDisplay.UpdateHealth(actor.health.value);
                break;
        }

        player.UpdateUI();
        anim.Play("collect");
    }
}
