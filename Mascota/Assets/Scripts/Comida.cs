using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    [SerializeField] GameObject[] ComidaPrefabs;

    [SerializeField] GameObject[] BombasPrefabs;

    [SerializeField] float secondSpawn = 0.5f;

    [SerializeField] float minTras;

    [SerializeField] float maxTras;


    void Start()
    {
        StartCoroutine(ComidaSpawn());

    }

   IEnumerator ComidaSpawn()
    {
       while(true)
        {

            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(ComidaPrefabs[Random.Range(0, ComidaPrefabs.Length)], position,Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameObject, 30f);

        }

    }

}
