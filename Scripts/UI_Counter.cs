using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Counter : MonoBehaviour
{
    public Text count;

    public void UpdateCount(float newValue)
    {
        count.text = newValue.ToString();
    }
}
