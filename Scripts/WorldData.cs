using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public int[] nextWorld = new int[] {
        2000,
        1,
        1,
        1,
        1,
        1
    };
    public List<World> worlds = new List<World>();
    //public List<Level> levels = new List<Level>();
}
