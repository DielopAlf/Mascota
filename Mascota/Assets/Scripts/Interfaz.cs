using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaz : MonoBehaviour
{
    public GameObject MenuInicial;
    public GameObject MenuJugar;
    public GameObject MenuAlimentar;

    void Start()
    {
     MenuInicial.SetActive(true); 
     MenuJugar.SetActive(false);
     MenuAlimentar.SetActive(false);
    }

    
    public void ActivarJugar()
    {
        MenuInicial.SetActive(false);
        MenuJugar.SetActive(true);
    }
    public void DesactivarJugar()
    {
        MenuInicial.SetActive(true);
        MenuJugar.SetActive(false);
    }

    public void ActivarAlimentar()
    {
        MenuInicial.SetActive(false);
        MenuAlimentar.SetActive(true);
    }
    public void DesactivarAlimentar()
    {
        MenuInicial.SetActive(true);
        MenuAlimentar.SetActive(false);
    }

}
