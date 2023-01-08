using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject[] ComidaASpawnear;
    static List<GameObject> Comida = new List<GameObject>();
    static List<GameObject> ComidaSpawneada = new List<GameObject>();

    public GameObject mascota;
    Mascota scriptMascota;

    public float tiempoDeJuego = 30f;


    bool gameOver;

    public float maxPosDerecha;
    public float maxPosIzquierda;

    public float altura;

    public void Start()
    {
        scriptMascota = mascota.GetComponent<Mascota>();
    }

   
    public void SetComida()
    {
        Comida.Clear();
        ComidaSpawneada.Clear();

       
        for (int i = 0; i < ComidaASpawnear.Length; i++)
        {
            Comida.Add(ComidaASpawnear[i]);
        }
    }

    public void ComenzarAlimentacion()
    {
        SetComida();

        gameOver = false;

        StartCoroutine(TirarComida());
    }

    public IEnumerator TirarComida()
    {
        float tiempoEntreTrozos = tiempoDeJuego / ComidaASpawnear.Length;

        while (Comida.Count > 0 && gameOver == false)
        {

            yield return new WaitForSeconds(tiempoEntreTrozos);


            if (scriptMascota.muerta == false && gameOver == false)
            {

                int i = Random.Range(0, Comida.Count - 1);

                GameObject trozo = Instantiate(Comida[i], new Vector3(Random.Range(maxPosIzquierda, maxPosDerecha), altura, 0f), Quaternion.identity);

                Comida.RemoveAt(i);

                ComidaSpawneada.Add(trozo);
            }
            else
            {
                yield break;
            }
        }

        yield return new WaitForSeconds(5f);

        if (gameOver == false)
        {
            AcaboElJuego();
        }
    }


    public void AcaboElJuego()
    {
        gameOver = true;

        for (int i = 0; i < ComidaSpawneada.Count; i++)
        {
            if (ComidaSpawneada[i] != null)
            {
                Destroy(ComidaSpawneada[i]);
            }
        }

        Comida.Clear();

        ComidaSpawneada.Clear();

        scriptMascota.AcabarAlimentar();

        Interfaz.Singleton.DesactivarMenuAlimentar();
    }
   
}