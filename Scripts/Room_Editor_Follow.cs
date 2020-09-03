using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Editor_Follow : MonoBehaviour
{
    public Grid grid;

    public void Update()
    {
        //Mouse to Cell Position
        if (grid == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3Int position = grid.WorldToCell(mousePos);

        transform.position = new Vector3(position.x + 0.5f,position.y + 0.5f);
    }
}
