using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn
{
    public Transform location;
    public GameObject entity;
    public float waitTime = 0f;
    
}

[System.Serializable]
public class Wave
{
    public string name;
    public Spawn[] entities;
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Arena : MonoBehaviour
{
  

    //Progess in the Arena
    public int progress = 0;

    //What to spawn
    public GameObject spawnEffect;

    //Arena itself
    public Wave[] arena;

    //Doors to close
    public GameObject[] doors;

    //Enemies alive
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();

    //Has Started? Has Wave Finished?
    private bool started = false;
    private bool waveFinish = false;

    //How often it checks for enemies alive
    private float updateTime = 2f;
    public void ProgressArena()
    {
        //If the wave has finished spawning all enemise and we started this arena
        //AND all enemies are dead
        if(started && HasWaveEnded() && progress < arena.Length - 1)
        {
            progress += 1;
            StartCoroutine("SpawnWave");
        }

        //Arena is complete
        if(HasWaveEnded() && progress  >= arena.Length - 1)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
            Destroy(this.gameObject);
        }
    }

    public bool HasWaveEnded()
    {
        bool enemyAlive = false;
        
        if(enemies.Count > 0)
        {
            enemyAlive = true;
        }

        if(!enemyAlive && waveFinish)
        {
            return true;
        } else
        {
            return false;
        }
    }

    //Spawn it
    public void SpawnEntity(Spawn spawn)
    {
        AudioManager.instance.PlaySound("Enemy Spawn");
        //New Enemy
        GameObject newEnemy = Instantiate(spawn.entity, spawn.location.position, spawn.location.rotation);
        Instantiate(spawnEffect, spawn.location.position, spawn.location.rotation);
        newEnemy.transform.SetParent(transform);

        //New Spawn
        Arena_Spawn newSpawn = newEnemy.AddComponent<Arena_Spawn>();
        newSpawn.arena = this;

        //newEnemy.GetComponent<AI_Enemy>().difficulty = arcadeManager.difficulty;
        enemies.Add(newEnemy);
    }

    //Spawn a wave
    public IEnumerator SpawnWave()
    {
        waveFinish = false;
        for (int i = 0; i < arena[progress].entities.Length; i++)
        {
            yield return new WaitForSeconds(arena[progress].entities[i].waitTime);
            SpawnEntity(arena[progress].entities[i]);
        }
 
        waveFinish = true;
    }


    //Remove enemy from the arena
    public void RemoveEnemy(GameObject enemy)
    {
        enemies.RemoveAt(enemies.LastIndexOf(enemy));
    }


    //On Collision start the arena
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && !started)
        {
            //Spawn the Wave
            StartCoroutine("SpawnWave");
            InvokeRepeating("ProgressArena", updateTime, updateTime);

            //Close all the Doors
            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }

            started = true;
        }
    }
}
