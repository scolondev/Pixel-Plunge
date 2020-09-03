using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Room_Editor : MonoBehaviour {

    /*
	
	Creating our own room editor tool to make room creation quicker;
	Instead of having a rooms folder with multiple prefabs, we will be 
	saving everything inside of a JSON that's loaded in at runtime.
	
	To be safe, there will be many backups of this JSON.
	This will also allow for level creation tools for the players later
	down the line.
	

     Looks like it turned out great, we still need to do object editing and saving though.
	*/

    //Assign these in the inspector

    public GameObject room;

    public Grid grid;
    public Tilemap groundTiles;
    public Tilemap backgroundTiles;

    public Room_Editor_Tile pallette;

    public float eraseRange;

    public Dropdown objects;
    public Dropdown tilemap;
    public InputField roomName;

    private string action = "DrawTile";
    private List<Vector3Int> tiles = new List<Vector3Int>();
    private List<Vector3Int> backTiles = new List<Vector3Int>();

    private List<TileBase> tileGround = new List<TileBase>();
    private List<TileBase> tileBackGround = new List<TileBase>();

    private GameObject selectedObject;
    private Tilemap selectedTiles;

    public void Start()
    {
        selectedTiles = groundTiles;

        for (int n = groundTiles.cellBounds.xMin; n < groundTiles.cellBounds.xMax; n++)
        {
            for (int p = groundTiles.cellBounds.yMin; p < groundTiles.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)groundTiles.transform.position.y));
                if (groundTiles.HasTile(localPlace))
                {
                    //Tile at "place"
                    tiles.Add(localPlace);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }

        for (int n = backgroundTiles.cellBounds.xMin; n < backgroundTiles.cellBounds.xMax; n++)
        {
            for (int p = backgroundTiles.cellBounds.yMin; p < backgroundTiles.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)backgroundTiles.transform.position.y));
                if (backgroundTiles.HasTile(localPlace))
                {
                    //Tile at "place"
                    backTiles.Add(localPlace);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }

    public void Update() {
        EventSystem system = EventSystem.current;
        if (system.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(0)) {
            Draw();
        }

        if (Input.GetMouseButton(1)) {
            Erase();
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            UpdateTiles();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            selectedTiles.ClearAllTiles();
            tiles.Clear();
        }
    }

    public void Draw() {
        if (action == "DrawTile") {
            DrawTile(GetCell());
        }
        if (action == "DrawObject") {
            DrawObject();
        }
    }

    public void Erase() {
        EraseTile();
        EraseObject();
    }

    public Vector3Int GetCell() {

        //Mouse to Cell Position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3Int position = grid.WorldToCell(mousePos);

        return position;
    }

    public Vector2 GetPos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }

 
    //Draw Tile
    public void DrawTile(Vector3Int pos) {
        if (pallette == null)
        {
            return;
        }

        //Drawing the tile!!!
        int index = UnityEngine.Random.Range(0, pallette.tileSet[0].tiles.Count);
        TileBase newTile = pallette.tileSet[0].tiles[index];

       // if (selectedTiles == null) return;

        if (!selectedTiles.GetTile(pos))
        {
            if (selectedTiles == groundTiles)
            {
                tiles.Add(pos);
                selectedTiles.SetTile(pos, newTile);
            } else
            {
                backTiles.Add(pos);
                selectedTiles.SetTile(pos, newTile);
            }
        }
    //    UpdateTiles();
    }

    //Erase Tile
    public void EraseTile()
    {
        selectedTiles.SetTile(GetCell(), null);
        if (selectedTiles == groundTiles)
        {
            tiles.Remove(GetCell());
        }
        else
        {
            backTiles.Remove(GetCell());
        }
        // UpdateTiles();
    }

    public void UpdateTiles()
    {
        if (selectedTiles == groundTiles)
        {
            foreach (Vector3Int pos in tiles)
            {
                int dir = CheckSides(pos);

                switch (dir)
                {
                    case 10000:
                        DrawUpdatedTile(pos, "Tunnel Vertical Closed");
                        break;
                    //Bottom
                    case 11000:
                        DrawUpdatedTile(pos, "Tunnel Vertical Closed");
                        break;
                    //Top
                    case 10100:
                        DrawUpdatedTile(pos, "Tunnel Vertical Closed");
                        break;
                    //Right
                    case 10010:
                        DrawUpdatedTile(pos, "Tunnel Horizontal Closed");
                        break;
                    //Left
                    case 10001:
                        DrawUpdatedTile(pos, "Tunnel Horizontal Closed");
                        break;
                    //Bottom Top
                    case 11100:
                        DrawUpdatedTile(pos, "Tunnel Vertical");
                        break;
                    //Bottom Right
                    case 11010:
                        DrawUpdatedTile(pos, "TL Corner");
                        break;
                    //Bottom Left
                    case 11001:
                        DrawUpdatedTile(pos, "TR Corner");
                        break;
                    //Top Right
                    case 10110:
                        DrawUpdatedTile(pos, "BL Corner");
                        break;
                    //Right Left
                    case 10011:
                        DrawUpdatedTile(pos, "Tunnel Horizontal");
                        break;
                    //Top Left
                    case 10101:
                        DrawUpdatedTile(pos, "BR Corner");
                        break;
                    //Top Bottom Right
                    case 11110:
                        DrawUpdatedTile(pos, "Left Ground");
                        break;
                    //Top Left Right
                    case 10111:
                        DrawUpdatedTile(pos, "Bottom Ground");
                        break;
                    //Bottom Left Right
                    case 11011:
                        DrawUpdatedTile(pos, "Top Ground");
                        break;
                    //Bottom Top Left
                    case 11101:
                        DrawUpdatedTile(pos, "Right Ground");
                        break;
                    //Everywhere
                    case 11111:
                        DrawUpdatedTile(pos, "Fill");
                        break;
                }
            }
        }
        if (selectedTiles == backgroundTiles)
        {
            foreach (Vector3Int pos in backTiles)
            {
                DrawUpdatedTile(pos, "Background");
            }
        }
    }

    public void DrawUpdatedTile(Vector3Int pos, string setString)
    {
        Room_Editor_Tile.TileSet set = pallette.GetTileSet(setString);
        int index = UnityEngine.Random.Range(0, set.tiles.Count);

        selectedTiles.SetTile(pos, set.tiles[index]);
    }

    public int CheckSides(Vector3Int pos)
    {
        Vector3Int leftPos = new Vector3Int(pos.x - 1, pos.y, 0);
        Vector3Int rightPos = new Vector3Int(pos.x + 1, pos.y, 0);
        Vector3Int topPos = new Vector3Int(pos.x, pos.y + 1, 0);
        Vector3Int bottomPos = new Vector3Int(pos.x, pos.y - 1, 0);

        //No collisions
        int dir = 10000;

        if (selectedTiles.GetTile(leftPos))
        {
            dir += 1;
        }
        if (selectedTiles.GetTile(rightPos))
        {
            dir += 10;
        }
        if (selectedTiles.GetTile(topPos))
        {
            dir += 100;
        }
        if (selectedTiles.GetTile(bottomPos))
        {
            dir += 1000;
        }

        //     Debug.Log(pos + " " + dir);
        return dir;
    }

   


    //Draw Object
    public void DrawObject() {
        Vector3 pos = new Vector3(GetCell().x + 0.5f , GetCell().y + 0.5f , 0);
        GameObject newObject = Instantiate(selectedObject, pos, selectedObject.transform.rotation);
    }

    //EraseObject
    public void EraseObject() {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(GetPos(), eraseRange, LayerMask.GetMask("Object"))) {
            Destroy(collider.gameObject);
        }
    }

    //Select Tilemap
	public void SelectTilemap(){
        switch (tilemap.value)
        {
            case 0:
                selectedTiles = groundTiles;
            break;
            case 1:
                selectedTiles = backgroundTiles;
            break;
        }	
	}
	
	//Select thy Tile
	public void SelectTile(Room_Editor_Tile newSelection){
		pallette = newSelection;
        action = "DrawTile";
	}

    //Select thy Object
    public List<GameObject> objectInstances = new List<GameObject>();
	public void SelectObject(){

        selectedObject = objectInstances[objects.value];
        action = "DrawObject";
	}

    public void SaveRoom()
    {
        Room_Editor_Saver saveData = Room_Editor_Saver.instance;
        saveData.ReadData();
        //Save Data
        if (saveData.editorData.roomKeys.Contains(roomName.text)){
            //Replace the room at that point
            int index = saveData.editorData.roomKeys.IndexOf(roomName.text);

            Room_Editor_Data.TilePositions tileData = new Room_Editor_Data.TilePositions();
            tileData.groundPositions = tiles;
            tileData.backgroundPositions = backTiles;

            saveData.editorData.tilePositions[index] = tileData;
        } else
        {
            //Doesn't exist in our savedata so we add a new one.
            Room_Editor_Data.TilePositions tileData = new Room_Editor_Data.TilePositions();
            tileData.groundPositions = tiles;
            tileData.backgroundPositions = backTiles;

            saveData.editorData.roomKeys.Add(roomName.text);
            saveData.editorData.objects.Add(null);
            saveData.editorData.objectPositions.Add(new Vector3Int(0,0,0));
            saveData.editorData.tilePositions.Add(tileData);
        }

        saveData.SaveData();
    }

    public void LoadRoom()
    {
        //Loading in a room
        Room_Editor_Saver saveData = Room_Editor_Saver.instance;
        saveData.ReadData();

        Tilemap previousSelection = selectedTiles;

        int index = saveData.editorData.roomKeys.IndexOf(roomName.text);

        groundTiles.ClearAllTiles();
        backgroundTiles.ClearAllTiles();
        tiles.Clear();
        backTiles.Clear();

        //All the ground tiles
        foreach (Vector3Int pos in saveData.editorData.tilePositions[index].groundPositions)
        {
            selectedTiles = groundTiles;
            DrawTile(pos);
          
        }

        UpdateTiles();

        //All the background tiles
        foreach (Vector3Int pos in saveData.editorData.tilePositions[index].backgroundPositions)
        {
            selectedTiles = backgroundTiles;
            DrawTile(pos);
        }

        UpdateTiles();

        selectedTiles = previousSelection;
    }

    public void LoadObject(Vector3 pos, GameObject obj)
    {
        GameObject load = Instantiate(obj, pos, obj.transform.rotation);
        load.transform.SetParent(room.transform);
    }
}
