using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Room Tile", menuName = "Dungeon/Room Tile", order = 1)]
public class Room_Editor_Tile : ScriptableObject
{
	//The Tile
    [System.Serializable]
    public class TileSet
    {
        public string name;
        public List<TileBase> tiles;
    }

   	public List<TileSet> tileSet;

    public TileSet GetTileSet(string name)
    {
        return Array.Find(tileSet.ToArray(), set => set.name == name);
    }
}
