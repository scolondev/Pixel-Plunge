using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Vector3 Offset;
    private Camera cam;

    public Transform target;

    public float engageRadius = 10f;
    public float smoothSpeed = 0.125f;

    public float zoomedSize = 3f;
    private float currentSize;
    private float originalSize;

    public void Start()
    {
        cam = Camera.main;
        originalSize = cam.orthographicSize;
        currentSize = cam.orthographicSize;
    }

    private void FixedUpdate()
    {
        //Go to player otherwise.
        if(target != null)
        {
            Vector3 desiredPosition = target.position + Offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, currentSize, smoothSpeed * Time.fixedDeltaTime);
        }

        if (EnemiesInRange())
        {
            Engage();
        } else
        {
            Disengage();
        }
    }

    private bool EnemiesInRange()
    {
        return Physics2D.OverlapCircle(target.position, engageRadius, LayerMask.GetMask("Enemy"));
    }

    public void Engage()
    {
        currentSize = zoomedSize;
    }

    public void Disengage()
    {
        currentSize = originalSize;
    }
}
