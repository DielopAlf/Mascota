using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mascota : MonoBehaviour
{
    public bool Hambre;
    public int Amor;

    public GameObject [] Estados;
    public int EstadoActual =0;


   

    private void Start()
    {

        Amor =  PlayerPrefs.GetInt("Amor", 1);
        Hambre=false;
        /*
        if (PlayerPrefs.GetInt("Hambre", 0) == 0)
        {
        
            Hambre= false;

        }
        else 
        {
            Hambre= true; 
        
        }*/
        //Debug.Log(Amor);

        ComprobarEstado();
        CambiarEstado();


    }
    
   public void CambiarAmor(int DarAmor)
    {
        Amor= Amor+ DarAmor;
        PlayerPrefs.SetInt("Amor", Amor);
        CambiarEstado();
            

       // Debug.Log(Amor);
    }
    public void ComprobarEstado()
    {
        for (int i=0;i<Estados.Length;i=i+1) 
        {

            if (Estados[i].activeSelf==true)
            {
                EstadoActual= i;

            }

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
      //  Debug.Log(Amor);
    }
    public void Update()
    {
       // Debug.Log(Hambre);
    }
} 