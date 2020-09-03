using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bomber : Enemy
{
    public float attackRadius = 1f;
    public GameObject hitbox;

    private bool detonated;
    public void Update()
    {
        if (InRange(attackRadius) && !detonated)
        {
            Detonate();
            detonated = true;
        }
    }

    public void Detonate()
    {
        TakeDamage(100000);
    }

    public void OnDestroy()
    {
        Instantiate(hitbox, transform.position, transform.rotation);
    }

}
