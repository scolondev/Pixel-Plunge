using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_Spawn : MonoBehaviour
{
    public Arena arena;
    public void OnDestroy()
    {
        arena.RemoveEnemy(this.gameObject);
    }
}
