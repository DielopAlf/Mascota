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

    //AL EMPEZAR(START) se accede al script de la mascota para llamar sus funciones.
    public void Start()
    {
        scriptMascota = mascota.GetComponent<Mascota>();
    }

   //Aqui almacenaremos  los objetos que serán tirados.
    public void SetComida()
    {
        //Primero se limpian las listas donde se guardan por si no es la primera vez que
        //se juega el minijuego y tambien por si habia comida guardada antes.
        Comida.Clear();
        ComidaSpawneada.Clear();

       //Hacemos un bucle que pasara por todos los objetos de comida puesto en el inspector, y los guradaos en la lista en la que serán tirados
       //Usamos listas para que se puedan añadir y quitar objetos durante el juego.
       //Con Los array no se puede hacer esto. 
        for (int i = 0; i < ComidaASpawnear.Length; i++)
        {
            Comida.Add(ComidaASpawnear[i]);
        }
    }
    //Funcion comienza el minijuego
    public void ComenzarAlimentacion()
    {
        //Se establece la comida que caerá
        SetComida();
        //El juego no se acaba (Empieza).
        gameOver = false;
        //Se usa una corrutina para que a diferencia de un contador
        //se pueda controlar la duracion y ritmo de esta.
        StartCoroutine(TirarComida());
    }

    //Hacemos una funcion  temporizador que controla cuando cae un trozo de comida.
    public IEnumerator TirarComida()
    {
        //Se Calcula el tiempo que habrá entre trozos para para que dure lo marcado.
        float tiempoEntreTrozos = tiempoDeJuego / ComidaASpawnear.Length;
        // Mientras no se acaben de caerse los objetos y el juego no termine la comida continurará cayendo
        while (Comida.Count > 0 && gameOver == false)
        {
            //esto se usará para que esperar un tiempo en la caida de cada trozo.
            yield return new WaitForSeconds(tiempoEntreTrozos);

            //Si la mascota no muere y y el juego no termina.
            if (scriptMascota.muerta == false && gameOver == false)
            {
                //Caera de la lista un objeto de comida random.
                int i = Random.Range(0, Comida.Count - 1);

                //Con el instantiate aparecerá la comida puesta en la lista [i] en un punto random del eje X (derecha izquierda) siendo el limite el mismo que el del movimiento , a la vez que una altura y rotacion.
                GameObject trozo = Instantiate(Comida[i], new Vector3(Random.Range(maxPosIzquierda, maxPosDerecha), altura, 0f), Quaternion.identity);
                
                //Una vez que aparezca un trozo lo haremos desaparecer de la lista para que no vuelva a aparecer y que queden menos para la prox ejecucion ademas de que aparezca otro.
                Comida.RemoveAt(i);

                ComidaSpawneada.Add(trozo);
            }
            // Si la mascota muere o se acaba el juego.
            else
            {//se corta la courutine finalizando el juego.
                yield break;
            }
        }
        //esto hará que cuando no queden mas trozos por caer se espera 5 segundo para finalizar.
        yield return new WaitForSeconds(5f);

        //si no se para manualmente(dandole a volver) lo terminaremos nosotros
        if (gameOver == false)
        {
            AcaboElJuego();
        }
    }

    //Funcion para parar o acabar el juego
    public void AcaboElJuego()
    {
        //hacemos que el juego termine (para que deje de caer la comidaS)
        gameOver = true;
        //se hace otro bucle para que afecte a toda la lista.
        for (int i = 0; i < ComidaSpawneada.Count; i++)
        {
            //si este trozo aun existe (no se ha eliminado de cualquier manera)
            if (ComidaSpawneada[i] != null)
            {
                //hacemos que se destruya
                Destroy(ComidaSpawneada[i]);
            }
        }
        //se limpian las lista de la comida
        Comida.Clear();
        ComidaSpawneada.Clear();
        //Le decimos que ha terminado de comer para que detecte que no tiene hambre 
        scriptMascota.AcabarAlimentar();
        //Esto irá para la interfaz para indicarle que vuelva al menu principal por que termino el juego
        Interfaz.Singleton.DesactivarMenuAlimentar();
    }
   
}