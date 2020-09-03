using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Turret : Enemy
{
    public float attackDelay;
    public GameObject bullet;
    public GameObject fireEffect;
    public Transform firePoint;
    public bool backfire = false;

    private float attackCountdown;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attackCountdown = Time.time + attackDelay;
        if(attackDelay < 1)
        {
            anim.speed = 2/attackDelay;
        }
    
    }

    public void FireProjectile()
    {
        GameObject newProjectile = Instantiate(bullet, firePoint.position, bullet.transform.rotation);
        newProjectile.BroadcastMessage("SetReference", transform.position,SendMessageOptions.DontRequireReceiver);
        if (backfire)
        {
            newProjectile.BroadcastMessage("Reverse", transform.position, SendMessageOptions.DontRequireReceiver);
        }
        Instantiate(fireEffect, transform.position, transform.rotation);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.time > attackCountdown)
        {
            StartAttack();
            attackCountdown = Time.time + attackDelay;
        }
    }
}
