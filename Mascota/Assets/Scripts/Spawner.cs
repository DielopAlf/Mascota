using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

   public GameObject [] Obstaculos;
   public GameObject [] Comida;
   public GameObject prefabtarget;
   public bool stopSpawning = false;
   public float SpawnTime;
   public float SpawnDelay;

    void Start()
    {
         InvokeRepeating("SpawnObject",SpawnTime,SpawnDelay);
    }

     public void SpawnObject()
    {
        Instantiate(prefabtarget,  gameObject.transform.position, Quaternion.identity);
        if (stopSpawning)
        {
         CancelInvoke("SpawnObject");

        }
    }
}
