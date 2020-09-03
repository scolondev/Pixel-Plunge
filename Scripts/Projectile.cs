using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Hitbox
{
    public Bullet bullet;
    public Vector3 target;

    private Rigidbody2D rb;
    private Vector3 dir = new Vector3();
    private float reverse = 1;
    public override void Start()
    {
        //Cache
        rb = GetComponent<Rigidbody2D>();

        //Find Target
        FindTarget();

        if (bullet.fireSound != null) { AudioManager.instance.PlaySound(bullet.fireSound); }

            Destroy(this.gameObject, duration);
    }

    public void SetReference(Vector3 pos)
    {
        target = pos;
    }

    public void Reverse()
    {
        reverse = -1;
    }

    public void FindTarget()
    {
        switch (bullet.seek)
        {
            case Bullet.Target.Player:
                //Will go towards player
                target = GameObject.FindGameObjectWithTag("player").transform.position;
            break;
            case Bullet.Target.Mouse:
                //Will go towards the mouse
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                break;
            case Bullet.Target.Other:
                //The target should be overrode, so do nothing.
               
            break;
        }

        dir = target - transform.position;
        dir.z = 0.0f;
        dir = dir.normalized;
    }

    //Use Fixed Update for physics, so it doesn't depend on the user's frame rate.
    public void FixedUpdate()
    {
        //Direction to travel
        if(bullet.homing)
        {
            FindTarget();
        }
       
        rb.velocity = dir * 1 * bullet.speed.value * reverse * Time.fixedDeltaTime;
    }

    public void OnDestroy()
    {
        if (bullet.impactSound != null) { AudioManager.instance.PlaySound(bullet.impactSound); } 
        GameObject explosion = Instantiate(bullet.deathEffect, transform.position, transform.rotation);
    }
}
