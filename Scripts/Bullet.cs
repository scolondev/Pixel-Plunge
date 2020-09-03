using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet", order = 1)]
public class Bullet : ScriptableObject
{
    public Stat damage = new Stat(1, 10, 1);
    public Stat speed = new Stat(150,150,1);

    public bool homing;

    public string fireSound;
    public string impactSound;

    public GameObject deathEffect;
    public enum Target
    {
        Player, //Fly towards the player.
        Mouse, //Fly towards the mouse.
        Other //Target will be something else and will be set by another script.
    }

    public Target seek; //What we are seeking
}
