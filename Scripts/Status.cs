using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Status", menuName = "Dungeon/Status", order = 1)]
public class Status : ScriptableObject
{
    public enum Effect {
        Fire,
        Freeze,
        Poison,
        Dazed
    }

    public string _name = "New Status";

    public Effect effect; //The Status
    public GameObject prefab; //Status Visuals
    
    public int maxTicks = 3; //Maximum amount of times it can tick
    public float tickSpeed = 2f; //How fast it'll tick
    
    public float duration = 6f; //How long it lasts
    public float strength = 1f; //How powerful it is

    public float chance;

    public bool Proc(){
        int roll = Random.Range(0, 100);
        if(chance >= roll){
            return true;
        } else {
            return false;
        }
    }
  
}
