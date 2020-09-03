using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPlayer : Entity
{
    //Damage Modifier
    public Stat damageModifier = new Stat(1, float.MaxValue, float.MinValue);

    //Luck
    public Stat luck = new Stat(0, float.MaxValue, float.MinValue);

    //Health Display
    [HideInInspector]
    public UI_Health_Display healthDisplay;

    //Active item
    public Item_Active activeItem;
    public float activeCharge;
    private float maxCharge;

    private float iframes = 0.8f;
    private bool invincible = false;

    public override void Start()
    {
        base.Start();
        anim.SetFloat("health", health.value);
        healthDisplay = UI_Health_Display.instance;
    }

    public override void TakeDamage(float damage)
    {
        if (!invincible)
        {
            base.TakeDamage(damage);

            healthDisplay.UpdateHealth(health.value);
            healthDisplay.BreakHeart();

            StartCoroutine("IFrames");
        }
    }

    //Taking damage
    public IEnumerator IFrames()
    {
        invincible = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        yield return new WaitForSeconds(iframes / 5);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);
        yield return new WaitForSeconds(iframes / 5);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        yield return new WaitForSeconds(iframes / 5);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);
        yield return new WaitForSeconds(iframes / 5);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        yield return new WaitForSeconds(iframes / 5);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);

        invincible = false;
    }

    //Using your active item
    public void UseActive()
    {
        if (activeItem != null)
        if (activeCharge >= maxCharge)

        activeItem.Activate(this.gameObject);
        activeCharge = 0;
    }

    //Equipping Active Item
    public void EquipItem(Item_Active newActive)
    {
        if(activeItem != null)
        {
            Instantiate(activeItem.instance, transform.position, transform.rotation);
        }

        activeItem = newActive;
        maxCharge = newActive.maxCharge;
        activeCharge = 0;
    }

    //Gainning Charge, you'll gain it everytime an enemy dies.
    public void GainCharge(float amount)
    {
        if(activeItem != null)

        activeCharge += amount;
        if(activeCharge >= maxCharge)
        {
            activeCharge = maxCharge;
        }
    }
}
