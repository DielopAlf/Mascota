using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
   public GameObject [] ComidaaSpawnear;
   static List<GameObject> comida=new List<GameObject>();
   public float ALTURA;
   public float MaxTiempoentrecomidas=3f;
   public float MinTiempoentrecomidas=0.5f;
   public float MaxDERECHA;
   public float MaxIZQUIERDA;
    public void comenzarjuego()
    {
        setcomida();
        StartCoroutine(ciclodejuego());
    }
   

    public IEnumerator ciclodejuego()

    {
        while(comida.Count> 0)
        {
            yield return new WaitForSeconds(Random.Range(MinTiempoentrecomidas, MaxTiempoentrecomidas));
            int i = Random.Range(0, comida.Count-1);
            Instantiate(comida[i], new Vector3(Random.Range(MaxIZQUIERDA, MaxDERECHA), ALTURA, 0f), Quaternion.identity);
            comida.RemoveAt(i);
        }

    }
    public void setcomida()
    {
        for (int i = 0; i< ComidaaSpawnear.Length; i++)
        {
            comida.Add(ComidaaSpawnear[i]);
        }
    }
}