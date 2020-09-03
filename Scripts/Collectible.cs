using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //Effect for picking it up.
    public float value = 1f;
    public GameObject get;
    
    //Collection sound.
    public string collectSound;

    protected Animator anim;

    //AudioManager reference forsounds, counter reference for increasing player stats.
    private AudioManager am;
    private UI_Counter counter;

    //Can it be collected, and has it been collected?
    private bool collectible = false;
    private bool collected = false;

    //Time before we can pick it up.
    private float wait = 0.3f;
    public void Start()
    {
        am = AudioManager.instance;
        wait = Time.time + wait;

        //Animator Reference for??? Will not remove it incase it's used for something important.
        anim = GetComponent<Animator>();

        float explosionRadius = 2f;
        Vector3 randomPosition = Random.insideUnitCircle * explosionRadius;
        transform.position = transform.position + randomPosition;
        transform.rotation = new Quaternion();
    }

    public void Update()
    {
        if (InRange() && !collected && collectible)
        {
            Collect();
        }

        if (Time.time >= wait)
        {
            collectible = true;
        }
    }
    //For powerups
    public virtual IEnumerator Buff(float strength)
    {

        yield return new WaitForSeconds(10f);

    }

    public virtual void Collect()
    {
        //Collection Effect
        Instantiate(get, transform.position, transform.rotation);
        
        //Play sound andset collected to true so it can't be collected again.
        am.PlaySound(collectSound);
        collected = true;
    }

    //Check if player in range.
    private bool InRange()
    {
        return Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Player"));
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
