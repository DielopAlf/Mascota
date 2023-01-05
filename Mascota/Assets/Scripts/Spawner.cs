using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject[] ComidaASpawnear;
    static List<GameObject> Comida = new List<GameObject>();
    static List<GameObject> ComidaSpawneada = new List<GameObject>();

    public GameObject mascota;
    Mascota scriptMascota;

    public float tiempoDeJuego = 30f;

    public TextMeshProUGUI timer;

    bool gameOver;

    public float maxPosDerecha;
    public float maxPosIzquierda;

    public float altura;

    //Al empezar, accedemos al script de la masctoa para poder llamar sus funciones
    public void Start()
    {
        scriptMascota = mascota.GetComponent<Mascota>();
    }

    //Funcion que almacena los objetos de comida que van a ser tirados durante el minijuego
    public void SetComida()
    {
        //Primero limpiamos las listas donde guardamos estos objetos (por si no es el primer minijuego que se juega y habia alguna comida guardada aun)
        Comida.Clear();
        ComidaSpawneada.Clear();

        //Hacemos un bucle que pasa por todos los objetos de comida que pusimos en el inspector como opciones, y guardamos cada uno en una lsita de los que van a ser tirados durante este minijuego
        //(Usamos listas y no arrays porque en las listas podemos eliminar y añadir objectos durante el juego, en los arrays no)
        for (int i = 0; i < ComidaASpawnear.Length; i++)
        {
            Comida.Add(ComidaASpawnear[i]);
        }
    }

    //Funcion que se llama cuando debe iniciar el minijuego
    public void ComenzarAlimentacion()
    {
        //Establecemos que comida se va a tirar
        SetComida();
        //Indicamos que el minijuego no ha acabado
        gameOver = false;
        //Comenzamos la funcion que controla el ritmo de tirar la comida
        StartCoroutine(TirarComida());
    }

    //Funcion con temporizador que controla cada cuanto se tira un trozo de comida
    public IEnumerator TirarComida()
    {
        //Calculamos cada cuanto debe tirarse un trozo de comida para que el minijuego dure lo que queremos (30 seguindos/7 trozos por ejemplo, nos da a 4 con algo entre cada trozo)
        float tiempoEntreTrozos = tiempoDeJuego / ComidaASpawnear.Length;

        //Mientras queden trozos de comida en la lista para tirar y aun no se haya acabado el minijuego, tiramos comida en bucle
        while (Comida.Count > 0 && gameOver == false)
        {
            //Esperamos lo que calculamos entre cada trozo
            yield return new WaitForSeconds(tiempoEntreTrozos);

            //Si la mascota no ha muerto y no se ha acabado el minijuego
            if (scriptMascota.muerta == false && gameOver == false)
            {
                //Cogemos un objeto de comida random (puede ser del 0 que es el primero de la lista, al ultimo de la lista)
                int i = Random.Range(0, Comida.Count - 1);

                //Hacemos aparecer este trozo (el i) de la lista Comida , en un punto random de x (de izquierda a derecha) entre unos limites, a la altura determinada y con rotacion base)
                GameObject trozo = Instantiate(Comida[i], new Vector3(Random.Range(maxPosIzquierda, maxPosDerecha), altura, 0f), Quaternion.identity);

                //Despues de aparecer este trozo (el i) de la lista de comida, lo eliminamos de la lista para que ya no aparezca este mas y en la proxima ejecuccion del mientras ya este no aparezca y queden cada vez menos
                Comida.RemoveAt(i);

                //Añadimos a otra lista nueva este trozo (esto es simplemente para que si le damos a acabar el minijuego mientras cae algun trozo, podamos eliminar este mientras cae)
                ComidaSpawneada.Add(trozo);
            }
            //Si la mascota a muerto o el minijuego ha acabdo mientras se tiraba la comida
            else
            {
                //Se para la funcion de tirar comida (esta)
                yield break;
            }
        }

        //Cuando ya no quedan trozos de comida en la lista o se ha acabado el minijuego (gameover = true), esperamos 5 segundo
        yield return new WaitForSeconds(5f);

        //Si el minijuego no se habia parado manualmente, lo paramos nosotros (lo acabamos)
        if (gameOver == false)
        {
            AcaboElJuego();
        }
    }


    //Funcion para parar/acabar el minijuego
    public void AcaboElJuego()
    {
        //Decimos que el minijuego ha acabado (por si aun se esta tirando comida, que deje de hacerlo)
        gameOver = true;

        //Hacemos un bucle que pasa por toda la lsita donde guardabamos cada trozo que tirabamos
        for (int i = 0; i < ComidaSpawneada.Count; i++)
        {
            //Si este trozo aun existe (no ha sido eliminado por comerselo la mascota o llegar al limite por abajo del mapa)
            if (ComidaSpawneada[i] != null)
            {
                //Lo eliminamos a mano para que deje de caer
                Destroy(ComidaSpawneada[i]);
            }
        }

        //Limipamos las listas de comida
        Comida.Clear();
        ComidaSpawneada.Clear();

        //Indicamos a la mascota que ya hemos acabado de alimentarla para que detecte si ya no tiene hambre
        scriptMascota.AcabarAlimentar();

        //Indicamos a la interfaz que ya paro el minijuego para que vuelva al menu principal
        Interfaz.Singleton.DesactivarMenuAlimentar();
    }
   
}