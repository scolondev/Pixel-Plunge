using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Entity : MonoBehaviour
{
    //All Entities must have these stats;

    public Stat health = new Stat(10, 10, 0);
    public Stat speed = new Stat(10, 10, 0);

    public GameObject hitEffect;
    public GameObject deathEffect;

    public string hitSound;
    public string deathSound;

    protected Rigidbody2D rb;
    protected Animator anim;

    private AudioManager am;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        am = AudioManager.instance;
    }

    //Taking Damage.
    public virtual void TakeDamage(float damage)
    {
        am.PlaySound(hitSound);
  
        health.Increment(-damage);
        Instantiate(hitEffect, transform.position, transform.rotation);

        //Dying is handled by the animator.
        anim.SetFloat("health", health.value);
    }

    public virtual void Die()
    {
        am.PlaySound(deathSound);

        Instantiate(deathEffect, transform.position, transform.rotation);
      //  anim.playableGraph.Destroy();

        Destroy(this.gameObject);
    }
}
