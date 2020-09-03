using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Jelly_Mite : Enemy
{
    public bool isFlyer;
    public float waitToFly = 2f;

    private bool thrown = false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        waitToFly = Time.time + waitToFly;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!thrown && isFlyer && Time.time > waitToFly)
        {
            moveMethod = MoveMethod.Impulse;
            thrown = true;
        }
    }
}
