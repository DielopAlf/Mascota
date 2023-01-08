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


    public void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }

        spawner = Minijuego.GetComponent<Spawner>();
    }

    void Start()
    {
        MenuInicial.SetActive(true);

        MenuAlimentar.SetActive(false);
    }


    public void DesactivarMenuAlimentar()
    {
        MenuInicial.SetActive(true);
        MenuAlimentar.SetActive(false);
    }

    public void DarleAParar()
    {
        spawner.AcaboElJuego();
    }



    public void ActivarAlimentar()
    {
        MenuInicial.SetActive(false);
        spawner.ComenzarAlimentacion();
        MenuAlimentar.SetActive(true);
    }

    public void MascotaMuerta()
    {
       
        DesactivarMenuAlimentar();
        botonAlimentar.SetActive(false);
        botonResetear.SetActive(true);
    }



   
    public void ResetearPuntos()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}