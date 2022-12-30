using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interfaz : MonoBehaviour
{
    public GameObject MenuInicial;
    public GameObject MenuAlimentar;

    public GameObject Minijuego;
    Spawner spawner;


    public Slider sliderAmor;
    public Toggle toggleHambre;

    public GameObject botonAlimentar;
    public GameObject botonResetear;


    public static Interfaz Singleton;


    //Declara el singleton para poder acceder a este manager de interfaces desde cualquier otro codigo
    public void Awake()
    {
        //Si ya hay algun otro Interfaz creado (no deberia) destruye este para que solo haya 1
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        //Si no, este es el unico, y este debe ser el singleton
        else
        {
            Singleton = this;
        }

        //Almacena el script de spawner para poder acceder a sus funciones mas adelante
        spawner = Minijuego.GetComponent<Spawner>();
    }

    void Start()
    {
        //Activa el menu principal y desactiva el del modo alimentar
        MenuInicial.SetActive(true);

        MenuAlimentar.SetActive(false);
    }


    public void DesactivarMenuAlimentar()
    {
        //Desactiva el menu de alimentar y activa el menu inicial
        MenuInicial.SetActive(true);
        MenuAlimentar.SetActive(false);
    }

    public void DarleAParar()
    {
        //Cuando estas en el minijuego de alimentar, si le das a parar, se ejcuta la logica de acabar el juego que esta en el script del spawner
        spawner.AcaboElJuego();
    }



    public void ActivarAlimentar()
    {
        //Cuando le damos al boton de alimentar, quitamos el menu incial y ponemos el del modo alimentar, e iniciamos la logica del minijuego del script de spawner
        MenuInicial.SetActive(false);
        spawner.ComenzarAlimentacion();
        MenuAlimentar.SetActive(true);
    }

    public void MascotaMuerta()
    {
        //Cuando la mascota muere, se desactiva el menu de alimentar y se activa el inicial (por si estaba en el minijuego)
        //y ademas, se descactiva la opcion de darle alimentacion y se activa la de reiniciar a la mascota
        DesactivarMenuAlimentar();
        botonAlimentar.SetActive(false);
        botonResetear.SetActive(true);
    }



    //Cuando la mascota muere aparece la ocpion de resetear la mascota.
    //Borra todos los datos guardados de su amor y hambre y resetea el juego
    public void ResetearPuntos()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
