using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mascota : MonoBehaviour
{
    public bool Hambre;
    public int Amor;
    int AmorPorAlimentacion;
    public int MaxAmoralAlimentar=12;

    public GameObject [] Estados;
    public int EstadoActual =0;


   

    private void Awake()
    {
     
        Amor = 50; //PlayerPrefs.GetInt("Amor", 1);
        EstadoActual =  PlayerPrefs.GetInt("EstadoActual", 1);

        if (PlayerPrefs.GetInt("Hambre", 0) == 0)
        {
        
            Hambre= false;

        }
        else 
        {
            Hambre= true; 
        
        }

        CambiarEstado();


    }
    
   public void CambiarAmor(int DarAmor)
    {
        Amor= Amor+ DarAmor;
        PlayerPrefs.SetInt("Amor", Amor);
        CambiarEstado();
            Debug.Log(Amor);

    }
    public void DardeComer(int amor)
    {
        if (Hambre == true )
        {
            AmorPorAlimentacion += amor;
            if (AmorPorAlimentacion <= MaxAmoralAlimentar)
            {
                CambiarAmor(amor);
            }


        }
    }
    public void AcabardeAlimentar()
    {

        if (AmorPorAlimentacion > 0)
        {
            Hambre= false;
            AmorPorAlimentacion= 0;
        }



    }
    


    public void CambiarEstado()
    {
        Estados[EstadoActual].SetActive(false);

        if (Amor<= 0)
        {
            
            Estados[0].SetActive(true);
            EstadoActual=0;
        }
        else if(Amor>0 && Amor<=5)
        {
            Estados[1].SetActive(true);
            EstadoActual=1;
        }
        else if (Amor>5 && Amor<=20)
        {
            Estados[2].SetActive(true);
            EstadoActual=2;
        }
        else if (Amor>20 && Amor<=60)
        {
            Estados[3].SetActive(true);
            EstadoActual=3;
        }
        else if (Amor>60)
        {
            Estados[4].SetActive(true);
            EstadoActual=4;
        }
        PlayerPrefs.SetInt("EstadoActual",EstadoActual);
    }
    
} 