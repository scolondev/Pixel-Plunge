using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//TEST CLASS, NOT SURE IF THIS WILL BE IN THE FULL GAME
[CreateAssetMenu(fileName = "Room Settings", menuName = "Dungeon/Room Settings", order = 1)]
public class Room_Settings : ScriptableObject
{
   	public string roomName;
	public Texture2D image;
	
	public Direction[] doors;
	public Vector2 size;
	
	public Room_Codes colorCodes;
}
