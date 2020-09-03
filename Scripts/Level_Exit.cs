using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Exit : MonoBehaviour
{
    private GameManager gm;
    private float activateTime = 5f;
    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        activateTime = Time.time + activateTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && activated == false && Time.time > activateTime)
        {
            gm.CompleteLevel();
            activated = true;
        }
    }
}
