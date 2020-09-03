using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Effect_Stat : MonoBehaviour
{
    [System.Serializable]
    public class StatChange
    {
        public enum StatToChange
        {
            health,
            damage,
            speed,
            luck
        }

        public float modifier;
        public Modify modify;
        public StatToChange stat;
    }

    public enum Modify
    {
        Increment,
        Multiply,
        Exponentiate,
        Set
    }

    public List<StatChange> effects = new List<StatChange>();
    public void Start()
    {
        BoostStats();
    }

    public void BoostStats()
    {
       ActorPlayer player = GetComponentInParent<ActorPlayer>();
       foreach(StatChange effect in effects)
       {
            switch (effect.stat)
            {
                case StatChange.StatToChange.health:
                    Apply(player.health, effect.modify, effect.modifier);
                    UI_Health_Display.instance.UpdateHealth(player.health.value);
                    break;
                case StatChange.StatToChange.damage:
                    Apply(player.damageModifier, effect.modify, effect.modifier);
                    break;
                case StatChange.StatToChange.speed:
                    Apply(player.speed, effect.modify, effect.modifier);
                    break;
                case StatChange.StatToChange.luck:
                    Apply(player.luck, effect.modify, effect.modifier);
                    break;
            }
       }
    }

    public void Apply(Stat stat, Modify mod, float change)
    {
        switch (mod)
        {
            case Modify.Increment:
                stat.Increment(change);
                break;
            case Modify.Exponentiate:
                stat.Exponentiate(change);
                break;
            case Modify.Multiply:
                stat.Multiply(change);
                break;
            case Modify.Set:
                stat.Equals(change);
                break;
        }
    }
}
