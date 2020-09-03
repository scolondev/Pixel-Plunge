using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Gate_Switch : MonoBehaviour
{
    public Level_Gate gate;
    bool collided = false;

    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player") && !collided)
        {
            gate.UnlockRequirement();
            collided = true;
            anim.Play("collect");
        }
    }
}
