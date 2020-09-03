using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 100;

    public bool targetPlayer;

    private SpriteRenderer sr;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if(targetPlayer) { target = GameObject.FindGameObjectWithTag("player").transform; }
    }
    public void FixedUpdate()
    {
        if(target == null) { return;  }

       
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 180;

        if (angle > 90 && angle < 270)   { sr.flipY = true;  } else  {  sr.flipY = false; }

        float smoothedAngle = Mathf.Lerp(transform.rotation.z, angle, rotateSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.AngleAxis(smoothedAngle, Vector3.forward);
    }
}
