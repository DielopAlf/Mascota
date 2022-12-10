using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

   
   public GameObject prefabtarget;
   public bool stopSpawning = false;
   public float SpawnTime;
   public float SpawnDelay;

    void Start()
    {
         InvokeRepeating("SpawnObject",spawnTime,SpawnDelay);
    }

     public void SpawnObject()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
        if (stopSpawning)
        {
         CancelInvoke("SpawnObject");

        }
    }
}
