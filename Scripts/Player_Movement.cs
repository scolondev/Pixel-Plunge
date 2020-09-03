using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour
{
    //Hitbox
    public GameObject hitbox;
    public float deceleration = 0.3f;
    
    //Player unique references
    //We need a reference to the player for stats and rigidbody for movement
    private ActorPlayer player;
    private Rigidbody2D rb;

    //Ragdoll, when you hit an enemy you losecontrol of your player for a short time
    private bool ragdoll = false;
    private float ragdollTimer = 0.2f;
    private float ragdollCountdown;
    
    //Dashing
    private float dashTimer = 0.3f;
    private float dashCountdown;

    public void Start()
    {
        //Referencing
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<ActorPlayer>();    
    }

    //Is dashing, has swiped, and can move.
    [HideInInspector]
    public bool dashing = false;
    
    [HideInInspector]
    public bool swipe = false;
    
    [HideInInspector]
    public bool canMove = true;

    public void Update()
    {
        if (Time.time > ragdollCountdown)
        {
            ragdoll = false;
        }

        if (swipe || Input.GetMouseButtonDown(1) && Time.time > dashCountdown)
        {
            canMove = false;
            dashing = true;
            swipe = false;

            GameObject newHitbox = Instantiate(hitbox, transform.position, transform.rotation);
            newHitbox.transform.SetParent(transform);

            float damage = player.damageModifier.value;
            newHitbox.GetComponent<Hitbox>().damage.Equals(damage);

            dashCountdown = Time.time + dashTimer;
            AudioManager.instance.PlaySound("Player Dash");
         
        } else if (Input.GetMouseButton(0) && ragdoll == false)
        {
            canMove = true;
        } else
        {
            canMove = false;
        }
    }

    public void FixedUpdate()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var mouseDir = mouse - transform.position;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized;

        if (dashing)
        {
            rb.velocity = 3 * player.speed.value * mouseDir * Time.fixedDeltaTime;
            dashing = false;
        }

        if (canMove)
        {
            rb.velocity = player.speed.value * mouseDir * Time.fixedDeltaTime;
        }
        else if(!ragdoll)
        {
            rb.velocity = new Vector2(rb.velocity.x * deceleration, rb.velocity.y * deceleration);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            ragdoll = true;
            ragdollCountdown = Time.time + ragdollTimer;
        } 
    }
}
