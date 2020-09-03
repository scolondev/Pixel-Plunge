using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drop Table", menuName = "Dungeon/DropTable", order = 1)]
public class DropTable : ScriptableObject
{
    public int[] rolls = new int[2];
    public Entry[] entries;
}
