using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//TEST CLASS, NOT SURE IF THIS WILL BE IN THE FULL GAME
[CreateAssetMenu(fileName = "Room Codes", menuName = "Dungeon/Room Codes", order = 1)]
public class Room_Codes : ScriptableObject
{
	[System.Serializable]
   	public class TileCodes {
		public Color key;
		public TileBase tile;
	}
	
	[System.Serializable]
   	public class ObjectCodes {
		public Color key;
		public GameObject obj;
	}
	
	[System.Serializable]
   	public class NodeCodes {
		public Color key;
		public GameObject node;
	}
	
	public TileCodes[] tileCodes;
	public ObjectCodes[] objectCodes;
	public NodeCodes[] nodeCodes;
}
