using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Editor_Camera : MonoBehaviour
{
    public Vector3 offset;

    public float dragSpeed = 2;
    public float zoomSpeed = 2;
    private Vector3 dragOrigin;

    public void Update()
    {

        float w = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize += w * zoomSpeed;

        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * -dragSpeed, pos.y * -dragSpeed, 0);

        transform.Translate(move, Space.World);
    }
}
