using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make sure to put this outside the class so every script can use it 
public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class Node : MonoBehaviour
{
    public Direction[] doors; //The doors this node will need. Ex a Left node needs a room with a Right Door
                              //Put the opposite of what you see

    //Destroy nodes if they collide with other nodes
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("node_center"))
        {
            Destroy(this.gameObject);
        }
    }
}
