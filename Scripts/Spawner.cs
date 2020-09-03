using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject entity;
    public float timeBetweenSpawns = 3f;
    private float countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown = timeBetweenSpawns;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            Spawn(entity);
            countdown = timeBetweenSpawns;
        }
    }

    public void Spawn(GameObject obj)
    {
        Instantiate(obj, transform.position, transform.rotation);
    }
}
