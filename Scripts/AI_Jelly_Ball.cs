using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Jelly_Ball : Enemy
{
    public float attackDuration = 1f;

    //Counting down to the attack;
    public GameObject attackEffect;
    public float attackSpeed = 5f;

    private float attackCountdown;
    // Start is called before the first frame update
    public override void Start()
    {
        //References
        base.Start();

        //Attack Speed
        attackCountdown = Time.time + attackSpeed;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.time > attackCountdown)
        {
            StartCoroutine("Dash");
            //Attack!
            //Jump towards the player
          
        }
    }

    public IEnumerator Dash()
    {
        GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
        effect.transform.SetParent(transform);



        rb.velocity = new Vector2(0, 0);
        attackCountdown = Time.time + attackSpeed;
        speed.Multiply(2);
        moveMethod = MoveMethod.Impulse;

        yield return new WaitForSeconds(0.2f);

        speed.Multiply(0.5f);
        moveMethod = MoveMethod.Force;

     
    }
 
}
