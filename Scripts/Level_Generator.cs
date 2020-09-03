using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level_Generator : MonoBehaviour
{
   

    public int maxSize = 12; //Maximum amount of body rooms in a dungeon
    public int minSize = 7; //Minimum amount of body rooms in a dungeon
    public int dungeonSize; //Amount of body rooms in a dungeon

    public float waitTime = 0.5f; //Time between dungeon generations

    public LayerMask dungeon; //Layermask for checking collisions

    public GameObject start; //Starting Room
    public GameObject exit; //Exit Point

    public Entry[] spawn_table; //Body Rooms
    public Entry[] treasure_table; //Treasure Rooms
    public Entry[] exit_table; //Dead-End Rooms

    private Entropy entropy;
    private GameObject startRoom;
    private RandomHandler handler;
    private GameManager gm;

    private int currentRoom;
    private int lastRoom;

    private List<GameObject> rooms = new List<GameObject>();
    private List<GameObject> roomOrder = new List<GameObject>();
    public void Start()
    {
        gm = GameManager.instance;
        entropy = Entropy.instance;
        handler = entropy.GetHandler("dungeon");

        Debug.Log(handler.rand_seed);

        //Randomize Dungeon Size
        dungeonSize = entropy.GetHandler("dungeon").randomValue(minSize, maxSize);

        handler.OverridePool(exit_table);

        startRoom = Instantiate(handler.Randomize(), transform.position, transform.rotation);
        startRoom.transform.SetParent(transform);

        //Make the dungeon
        StartCoroutine("CreateDungeon");
    }

    //Can a room spawn in this area, checks for any collisions so it doesn't overlap.
    public bool CanSpawn(Vector2 point, Vector2 size, float angle, LayerMask mask)
    {
        return Physics2D.OverlapBox(point, size, angle, mask);
    }

    public bool HasDoor(Room room, Direction[] directions)
    {
        //Iterate through all the directions
        //Warning Nested Loop, may cause lag.  We don't want long loading screens
        //This will at most run 16 times. Since there are 4 Doors and 4 Directions
        for (int i = 0; i < directions.Length; i++)
        {
            //Iterate through all the doors
            for (int j = 0; j < room.doors.Length; j++)
            {
                //If any direction matches a direction of the node, it does have a necessary door to spawn here
                if (directions[i] == room.doors[j])
                {
                    return true;
                }
            }
        }
        //Debug.Log(room.name + " No door matches up with this");
        return false;
    }

    public GameObject[] Find(string tag)
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag(tag);
        return nodes;
    }

    private int roomsGenerated = 0;
    private GameObject[] nodes;
    private GameObject[] nodes_center;
    //Create the Dungeon
    public IEnumerator CreateDungeon()
    {
        yield return new WaitForSeconds(2f);

        nodes = Find("node_spawn");
        nodes_center = Find("node_center");

        int startIndex = handler.randomValue(0, dungeonSize/2);
   

        handler.OverridePool(spawn_table);

        while (nodes.Length > 0)
        {
            //After reaching the max body rooms, add in the treasure room, then close the dungeon.
            if(nodes_center.Length >= dungeonSize && !GameObject.FindGameObjectWithTag("node_treasure")){
                handler.OverridePool(treasure_table);
            } else if(nodes_center.Length >= dungeonSize)
            {
                handler.OverridePool(exit_table);
            }

            int newNode = handler.randomValue(0, nodes.Length);
            Node node = nodes[newNode].GetComponent<Node>(); //Grab a random node
 
            GameObject newRoom = handler.Randomize(); //Grab a random room
            Room room = newRoom.GetComponent<Room>();  //Grab the room info

            //Spawn the room! In order for it to spawn, must be no collisions and must match up doors.
            if (!CanSpawn(nodes[newNode].transform.position, room.size, 0, dungeon) && HasDoor(room, node.doors))
            {
                if(roomsGenerated == startIndex)
                {
                    SpawnRoom(start, nodes[newNode]);
                } else
                {
                    SpawnRoom(newRoom, nodes[newNode]);
                }

                roomsGenerated++;
            }
           
            //We have a wait time between generations to allow for rooms to detect collisions and destroy nodes.   
            yield return new WaitForSeconds(waitTime);

            //Find the nodes that still exist.
            nodes = Find("node_spawn");
            nodes_center = Find("node_center");

            if(nodes.Length < 1 && roomsGenerated < dungeonSize)
            {
                yield return StartCoroutine("RevertStep");
              //  yield return new WaitForSeconds(3f);
            }
        }

        //Possible places to put an exit door
        GameObject[] possibleExits = GameObject.FindGameObjectsWithTag("node_exit");
        int newExit = handler.randomValue(0, possibleExits.Length);

        //Instantiate the Goal Point
        GameObject dungeonExit = Instantiate(exit, possibleExits[newExit].transform.position, exit.transform.rotation);
        dungeonExit.transform.SetParent(transform);

        Debug.Log("Dungeon Complete!");
        gm.levelStart();
    }

    public void SpawnRoom(GameObject newRoom, GameObject node)
    {
        //Actually spawn the room
        GameObject myRoom = Instantiate(newRoom, node.transform.position, node.transform.rotation);
        myRoom.transform.SetParent(startRoom.transform);

        //Keeping track of all the rooms we created increase we build into ourselves.
        rooms.Add(myRoom);
        roomOrder.Add(newRoom);

        currentRoom = rooms.Count - 1;
        lastRoom = rooms.Count - 2;
    }

    public IEnumerator RevertStep()
    {
        Debug.Log("Reverting");
        //Grab the room before the current one
        GameObject revert = roomOrder[lastRoom];
        Vector3 lastRoomPosition = rooms[lastRoom].transform.position;

        //Remove the room we got stuck on
        Destroy(rooms[currentRoom]);
        rooms.RemoveAt(currentRoom);
        roomsGenerated--;

        //Remove the last room
        Destroy(rooms[lastRoom]);
        rooms.RemoveAt(lastRoom);

        roomOrder.RemoveAt(currentRoom);
        roomOrder.RemoveAt(lastRoom);
    

        //Actually spawn the room
        GameObject myRoom = Instantiate(revert, lastRoomPosition, revert.transform.rotation);
        myRoom.transform.SetParent(startRoom.transform);

        Tilemap[] maps = myRoom.GetComponentsInChildren<Tilemap>(); 
        foreach(Tilemap tilemap in maps)
        {
            tilemap.enabled = false;
            yield return new WaitForSeconds(waitTime);
            tilemap.enabled = true;
            tilemap.RefreshAllTiles();
        }

        //Keeping track of all the rooms we created increase we build into ourselves.
        rooms.Add(myRoom);
        roomOrder.Add(revert);

        currentRoom = rooms.Count - 1;
        lastRoom = rooms.Count - 2;


        //Reget the nodes
        yield return new WaitForSeconds(waitTime);

        nodes = Find("node_spawn");
        nodes_center = Find("node_center");
    }
}
