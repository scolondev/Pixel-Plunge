using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Item", menuName = "Active Item", order = 1)]
public class Item_Active : Item
{
    public float maxCharge;
    public float effectDuration = 2f;

    public override void Use(GameObject player)
    {
        ActorPlayer actor = player.GetComponent<ActorPlayer>();
        actor.EquipItem(this);
    }

    public void Activate(GameObject player)
    {
        GameObject effect = Instantiate(instance, player.transform.position, player.transform.rotation);
        Destroy(effect, effectDuration);
    }
}
