using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//TEST CLASS, NOT SURE IF THIS WILL BE IN THE FULL GAME
public class Room_Generator : MonoBehaviour
{
    //Task, take an image and read each pixel color of it
	//Generate a room from that image based on colors
	//Each color will correspond to a different block.
	
	//As for arenas, this will only work for 1 wave arenas.
	
	//Keeping a list of all the rooms created, for other scripts to reference.
	public List<GameObject> rooms = new List<GameObject>();
	
	//Loading the Rooms
	public List<Room_Settings> roomSettings = new List<Room_Settings>();
	public void Awake(){
		foreach(Room_Settings setting in roomSettings){
			LoadRoom(setting);
		}
	}
	
	//For other scripts to reference
	public void LoadRoom(Room_Settings room){
		CreateRoom(room);
	}
	
	private void CreateRoom(Room_Settings room){
		//Create the room
		GameObject newRoom = new GameObject();
		newRoom.name = room.roomName;
		
		//Create the level, this holds the tilemap stuffs
		GameObject level = new GameObject();
		level.name = "Ground";
		level.transform.SetParent(newRoom.transform);
		
		//Grid so that the properly come to gether
		newRoom.AddComponent<Grid>();
		
		//The level itself.
		Tilemap tilemap = level.AddComponent<Tilemap>();
		level.AddComponent<TilemapRenderer>();
		level.AddComponent<TilemapCollider2D>();
	
		//Draw the room.
		ParseRoom(tilemap, room, newRoom);

		//Add it to our dict.
		rooms.Add(newRoom);
	}
	
	private void ParseRoom(Tilemap tilemap, Room_Settings room, GameObject instance)
	{
		Debug.Log("Parsing Room: " + instance.name);

        //Load image
        Texture2D image = room.image;

		Room newRoom = instance.AddComponent<Room>();
		newRoom.size = new Vector2(image.width,image.height);
		
    	//Iterate through it's pixels
		//Normally this would be expensive, but rooms are small so it should be quick- 
    	for (int x = 0; x < image.width; x++) {
        	for (int y = 0; y < image.height; y++) { 
            	Color pixel = image.GetPixel(x, y);
				
				//Skip all transparent pixels, this should save a lot on loadtime.
				if(pixel.a == 0){
					continue;
				}
				
				for(int i = 0; i < room.colorCodes.tileCodes.Length; i++){
				
					if(pixel == room.colorCodes.tileCodes[i].key){
					
						//Setting Tile
						tilemap.SetTile(new Vector3Int(x,y,0), room.colorCodes.tileCodes[i].tile);
					}
				}
				
				for(int i = 0; i < room.colorCodes.objectCodes.Length; i++){
					if(pixel == room.colorCodes.objectCodes[i].key){
					
						//Instantiate Object
						GameObject newObject = Instantiate(room.colorCodes.objectCodes[i].obj,new Vector3(x,y,0),room.colorCodes.objectCodes[i].obj.transform.rotation);
						
						newObject.transform.SetParent(instance.transform);
					}
				}
				
				for(int i = 0; i < room.colorCodes.nodeCodes.Length; i++){
					if(pixel == room.colorCodes.nodeCodes[i].key){
						
						//Instantiate Node
						GameObject newNode = Instantiate(room.colorCodes.nodeCodes[i].node,new Vector3(x,y,0),room.colorCodes.nodeCodes[i].node.transform.rotation);
						
						newNode.transform.SetParent(instance.transform);
					}
				}
        	}
    	}
		
		Debug.Log("Parse Complete.");
	}
}
