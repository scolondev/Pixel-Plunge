using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Entries
[System.Serializable]
public class Entry
{
    public string _name; //Name, we use this to get a specific entry

    public GameObject result; //What this entry will result in
    public float weight; //Increase the weight for better odds

    //Override the weight, dynamic randomness.  This will allow for luck increasing items, and a luck stat.
    public void OverrideWeight(float newWeight)
    {
        weight = newWeight;
    }

    //Increase the weight, dynamic randomness.
    public void ModifyWeight(float newWeight)
    {
        weight += newWeight;
    }
}

//Handler, we make a new bit of randomness for each independent event
[System.Serializable]
public class RandomHandler
{
    public string _name;

    [HideInInspector]
    public System.Random random;

    //Pool of Events
    public Entry[] pool;

    public int rand_seed;

    //Update the pool, so we can use the same handler for multiple pools
    //EX: Different Dungeon Pools
    public void OverridePool(Entry[] newPool)
    {
        pool = newPool;
    }
    //Override the Seed, done at runtime
    public void OverrideSeed(int newSeed)
    {
        rand_seed = newSeed;
        random = new System.Random(rand_seed);
    }
    public void NewSeed()
    {
        rand_seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        random = new System.Random(rand_seed);
    }

    //Get an Entry
    public Entry GetEntry(string name)
    {
        return Array.Find(pool, entry => entry._name == name);
    }

    //Get the total of all the weights
    public float Sum()
    {
        float sum = 0;
        for (int i = 0; i < pool.Length; i++)
        {
            sum += pool[i].weight;
        }
        return sum;
    }

    public int randomValue(int minimum, int maximum)
    {
        return random.Next(minimum, maximum);
    }

    //Weighted Randomness
    public GameObject Randomize()
    {
        float top = 0; //Use this to iterate through weights
        float total = Sum(); //Sum of the weights

        //Random Number between 0 and total
        float rand = random.Next(0, (int)total);

        
        for (int i = 0; i < pool.Length; i++)
        {
            top += pool[i].weight;
            if (top >= rand)
            {
                //If it matches up, this is what you rolled
                return pool[i].result; 
            }
        }

        Debug.LogError(_name + " randomize failed, null value returned.");
        return null;
    }
}

public class Entropy : MonoBehaviour
{

    public static Entropy instance; //We only want 1 instance of this script, it can be referenced anytime.
    public int seed = 0; //The overarching seed

    //Our Handlers
    public RandomHandler[] randomHandlers;

    //We only want 1 instance of this script
    public void Awake()
    {
        instance = this;
        if (seed == 0)
        {
            seed = UnityEngine.Random.Range(0, Int32.MaxValue);
        }
        
        foreach(RandomHandler handler in randomHandlers){
            handler.OverrideSeed(seed);
        }
    }



    //Get a specific handler by name
    public RandomHandler GetHandler(string handler_name)
    {
        return Array.Find(randomHandlers, handler => handler._name == handler_name);
    }

    //Calculate Odds of a particular event
    public float CalculateOdds(string handler_name, string entry_name)
    {
        RandomHandler handler = GetHandler(handler_name);
        Entry entry = handler.GetEntry(entry_name);

        //The Chance
        float chance = (entry.weight / handler.Sum()) * 100f;
        return chance;
    }

    
}
