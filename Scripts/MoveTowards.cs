using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public Transform anchor;
    [HideInInspector]
    public Transform target;

    public GameObject myTarget;

    public Vector3 offset;
    public float maxDistance;

    public bool glide = false;
    public bool targetPlayer = false;
    public bool targetMouse = false;
    public bool randomPoint;

    // Update is called once per frame
    public void UpdateTarget()
    {
        if (anchor == null)
        {
            Transform newAnchor = transform;
            anchor = newAnchor;
        }
        if (randomPoint)
        {
            Vector2 randomPoint = Random.insideUnitCircle * 2f;
            Vector3 newPoint = new Vector3(randomPoint.x, randomPoint.y, 0);
            offset = transform.position + newPoint;
        }

        if (targetPlayer)
        {
            target = GameObject.FindGameObjectWithTag("player").transform;
        } else
        {
            GameObject newTarget = new GameObject();
            newTarget.name = name + "'s Target";
            newTarget.transform.position = offset;

            myTarget = newTarget;
            target = newTarget.transform;
        }
    }

    public void Start()
    {
        if (randomPoint)
        {
            Destroy(this, 2f);
            Destroy(myTarget, 2f);
        }
        UpdateTarget();
    }

    public void FixedUpdate()
    {
        if (target == null) { return; }
        if (targetMouse)
        {
            if (anchor == null) { return; }
            transform.position = Vector2.MoveTowards(anchor.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), maxDistance);
            return;
        }
        if (glide)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, maxDistance);
            transform.position = smoothedPosition;

        } else
        {
            transform.position = Vector2.MoveTowards(anchor.position, target.position, maxDistance);
        }
    }

    public void OnDestroy()
    {
        if (myTarget)
        {
            Destroy(myTarget.gameObject);
        }
    }
}
