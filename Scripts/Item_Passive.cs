using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Item", menuName = "Passive Item", order = 1)]
public class Item_Passive : Item
{
    public override void Use(GameObject player)
    {
        GameObject instanceObj = Instantiate(instance, player.transform.position, player.transform.rotation);
        instanceObj.transform.SetParent(player.transform);
    }
}
