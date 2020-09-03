using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Room_Editor_Data
{
    public List<string> roomKeys;

    public List<GameObject> objects;
    public List<Vector3> objectPositions;

    [System.Serializable]
    public class TilePositions
    {
        public List<Vector3Int> groundPositions;
        public List<Vector3Int> backgroundPositions;
    }

    public List<TilePositions> tilePositions;
}
