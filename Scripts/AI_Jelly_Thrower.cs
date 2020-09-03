using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Jelly_Thrower : Enemy
{

    public float attackDelay;
    public GameObject bullet;
    public GameObject fireEffect;

    private float attackCountdown;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attackCountdown = Time.time + attackDelay;
    }

    // Update is called once per frame
    public void FireProjectile()
    {
        GameObject newProjectile = Instantiate(bullet, transform.position, bullet.transform.rotation);
        Instantiate(fireEffect, transform.position, transform.rotation);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Time.time > attackCountdown)
        {
            anim.Play("attack");
            attackCountdown = Time.time + attackDelay;
        }
    }
}
