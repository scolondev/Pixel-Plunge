using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Dungeon/Level", order = 1)]
public class Level : ScriptableObject
{
    public string _name = "New Level";
    public string description = "New Level Description";

    public GameObject prefab; //The level itself

    public float weight; //Chance for it to spawn
    public string setpiece; //The set of levels it fits in with
    public int setpiece_index; //The order at which it appears

    public Sprite icon; //Icon of the level
    public Sprite backing; //Backing Sprite
    public Color backing_color = new Color(0, 0, 0, 1f); //Color of Backing
}
