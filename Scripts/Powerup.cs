using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Collectible
{
    public enum Type
    {
        Strength,
        Speed,
        Radius
    }

    public Type statToBuff;
    public float power;
    public float duration;
    
    public override IEnumerator Buff(float strength)
    {
        Stat myStat = null;
         
        switch (statToBuff)
        {
            case Type.Strength:
                myStat = GameObject.FindGameObjectWithTag("player").GetComponent<ActorPlayer>().damageModifier;
            break;
            case Type.Speed:
                myStat = GameObject.FindGameObjectWithTag("player").GetComponent<ActorPlayer>().speed;
            break;
            case Type.Radius:
               // myStat = GameObject.FindGameObjectWithTag("player_hitbox").GetComponent<StatHolder>().radius;
            break;
        }

        myStat.Increment(power);
        
        yield return new WaitForSeconds(duration);

        myStat.Increment(-power); 
    } 
}
