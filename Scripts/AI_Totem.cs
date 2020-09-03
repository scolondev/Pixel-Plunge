using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Totem : Enemy
{
    public float effectRadius;
    public float strength;
    public float downTime;

    public GameObject aura;
    private float downCountdown;

    private Rigidbody2D playerRb;
    private GameObject player;
    private bool recharging = false;
    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("player");
        playerRb = player.GetComponent<Rigidbody2D>();
        downCountdown = downTime;
        aura.SetActive(false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        downCountdown -= Time.deltaTime;

        if(downCountdown <= 0)
        {
            if (!recharging)
            {
                StartCoroutine("Recharge");
            }

            AreaAttack();
        }
    }

    public void AreaAttack()
    {
        if (InRange(effectRadius))
        {
            //Add Status Effect;
            player.SendMessage("TakeDamage", 1);
        }
    }

    public IEnumerator Recharge()
    {
        recharging = true;
        aura.SetActive(true);
        StartAttack();

        yield return new WaitForSeconds(5f);

        EndAttack();
        downCountdown = downTime;
        recharging = false;
        aura.SetActive(false);
    }
}
