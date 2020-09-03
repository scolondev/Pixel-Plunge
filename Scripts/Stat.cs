using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float value;
    public float maximum;
    public float minimum;

    public float wrapValue = 1;

    //Equate the value
    public void Equals(float amount)
    {
        value = amount;
    }

    //Increase the value linearly
    public void Increment(float amount = 1)
    {
        value += amount;
        BindValue();
    }


    //Multiply the value
    public void Multiply(float amount)
    {
        value *= amount;
        BindValue();

    }

    //Raise the value to a power
    public void Exponentiate(float amount)
    {
        value = (float)Math.Pow((double)value, ((double)amount));
        BindValue();
    }

    //Check if numberis out of a range and bind it.
    public void BindValue()
    {
        bool outOfBounds = minimum + value > maximum || maximum - value < minimum;
        if (outOfBounds)
        {
            value = RoundValue(wrapValue);
        }
    }

    //Rounding to the nearest limit
    public float RoundValue(float wrap = 1)
    {
        //Setting wrap to negative 1 will reverse the result
        float min = (minimum - value) * wrap;
        float max = (maximum + value) * wrap;

        float roundedValue = 0;

        if (max > min)
        {
            roundedValue = maximum;
        }
        else
        {
            roundedValue = minimum;
        }

        return roundedValue;
    }

    //Constructor
    public Stat(float amount, float max, float min, float wrap = 1)
    {
        this.value = amount;
        this.maximum = max;
        this.minimum = min;
        this.wrapValue = 1;
    }
}
