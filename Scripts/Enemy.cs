using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity

{
	//Different ways the enemy can move
	public enum MoveMethod{
		Idle,
		Impulse,
		Force,
		ForceQuick
	}
	
	//What the enemy is currently doing
    public enum State{
        Idle,
        Seeking,
        Wandering,
        Attacking
    }
	
	//Drop Table
	public DropTable table;
	
	//Search Radius

    public float searchRadius;
    private string targeting = "player";

    //How often enemy changes direction whilst wandering.
    public float wanderRate = 6f;
	private float wanderCountdown;

	//MoveMethod and current State
	public MoveMethod moveMethod = MoveMethod.Idle;
    public State state = State.Idle;

	//Player Layermask for detecting if in range
    [HideInInspector]
	public LayerMask playerMask;
	
	//Last position of the target
	private Vector3 target;
	
   	public override void Start(){

        base.Start();
        anim = GetComponent<Animator>();
        playerMask = LayerMask.GetMask("Player");
		wanderCountdown = wanderRate;
        ChangeTarget("player");
    }

    public virtual void FixedUpdate(){
		if(InRange(searchRadius)){
			state = State.Seeking;
		} else {
			state = State.Wandering;
			wanderCountdown -= Time.deltaTime;
			if(wanderCountdown <= 0){
				wanderCountdown = wanderRate;
                Vector2 circle = Random.insideUnitCircle * searchRadius;
                target = transform.position + new Vector3(circle.x,circle.y,0);
			}
		}
		
		CheckState();
		
    }

	public void ChangeTarget(string tag){
		targeting = tag;
	}
	public void FindTarget(string tag){
		target = GameObject.FindGameObjectWithTag(tag).transform.position;
	}
	
	public void CheckState(){
		switch(state){
			case State.Idle:
			
			break;
			case State.Seeking:
				FindTarget(targeting);
				Move();
			break;
			case State.Wandering:				
				Move();
			break;
			case State.Attacking:
			
			break;
		}
	}
	
	
	//Actually moving
	public void Move(){
		var dir = target - transform.position;
		dir.z = 0.0f;
		dir = dir.normalized;
		switch(moveMethod){
			case MoveMethod.Idle:
				
			break;
			case MoveMethod.Impulse:
				rb.AddForce(dir * speed.value * Time.fixedDeltaTime, ForceMode2D.Impulse);
			break;
			case MoveMethod.Force:
				rb.AddForce(dir * speed.value * Time.fixedDeltaTime, ForceMode2D.Force);
			break;
			case MoveMethod.ForceQuick:
				rb.velocity = dir * speed.value * Time.fixedDeltaTime;
			break;
		}
	}
	
	//Check if player in range.
    protected bool InRange(float radius){
        return Physics2D.OverlapCircle(transform.position, radius, playerMask);
    }
	
	//Drop all the loot before dying.
    public override void Die()
    {   
        RandomHandler handler = Entropy.instance.GetHandler("enemy");
        handler.OverridePool(table.entries);

        int amount = handler.randomValue(table.rolls[0], table.rolls[1]);
        for (int i = 0; i < amount; i++)
        {
            GameObject result = handler.Randomize();
            Instantiate(result, transform.position, transform.rotation);
        }

        //Charge the player's active item
        GameObject player = GameObject.FindGameObjectWithTag("player");
        player.SendMessage("GainCharge" , 0.2f , SendMessageOptions.DontRequireReceiver);

        base.Die();
    }

    public virtual void StartAttack()
    {
        anim.SetBool("attack", true);
    }

    public virtual void EndAttack()
    {
        anim.SetBool("attack", false);
    }
}
