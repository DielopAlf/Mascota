using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mascota : MonoBehaviour
{
    public bool Hambre;
    public int Amor;
    public GameObject mascota;

    int amorAlimentando;
    public int maxAmorAlAlimentar = 12;

    public GameObject particulas;

    public GameObject [] Estados;
    public int EstadoActual =0;

    public bool muerta;

    public Vector3 posInicial;
   
    private void Awake()
    {
        Amor = PlayerPrefs.GetInt("Amor", 1);
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


    public void Start()
    {
        ComprobarMuerte();

        Interfaz.Singleton.sliderAmor.value = Amor;
        Interfaz.Singleton.toggleHambre.isOn = Hambre;
    }


    public void CambiarAmor(int DarAmor)
    {
        if(EstadoActual != 0)
        {
            Amor = Amor + DarAmor;
            PlayerPrefs.SetInt("Amor", Amor);

            CambiarEstado();
            Debug.Log(Amor);

            if (Amor < 0)
            {
                Amor = -1;
            }
            else if (Amor > 100)
            {
                Amor = 100;
            }

            Interfaz.Singleton.sliderAmor.value = Amor;

            ComprobarMuerte();
        }
    }
    

    public void DarDeComer(int amor)
    {
        if(Hambre == true)
        {
            if(amor > 0)
            {
                amorAlimentando = +amor;

                if (amorAlimentando <= maxAmorAlAlimentar)
                {
                    CambiarAmor(amor);
                    StartCoroutine(MostrarCorazones());
                }
                
            }
          

            else
            {
                CambiarAmor(amor);
            }

        }
        

        else
        {
            if(amor < 0)
            {   
                CambiarAmor(amor);
            }
            else
            {

                mascota.transform.localScale = mascota.transform.localScale * PlayerPrefs.GetFloat("gordura", 1.5f);
            }
            /*if  (Gordura > 1.5f)
            {
                Gordura = 1.5f;
            }*/
        }
       
    }
   
    IEnumerator MostrarCorazones()
    {
        particulas.SetActive(true);

        yield return new WaitForSeconds(1f);

        particulas.SetActive(false);

    }


    public void AcabarAlimentar()
    {
        if(amorAlimentando > 0)
        {
            Hambre = false;

            DateManager.Singleton.HaComido();

            Interfaz.Singleton.toggleHambre.isOn = Hambre;
        }
      
        amorAlimentando = 0;

        transform.position = posInicial;
    }


    public void ComprobarMuerte()
    {
        if(Amor <= 0)
        {
            muerta = true;
            Hambre = false;

            Interfaz.Singleton.MascotaMuerta();
            Interfaz.Singleton.sliderAmor.value = Amor;
            Interfaz.Singleton.toggleHambre.isOn = Hambre;

            transform.position = posInicial;
        }
        else
        {
            muerta = false;
        }
    }

    public void CambiarEstado()
    {
        for(int i = 0; i < Estados.Length; i++)
        {
            Estados[i].SetActive(false);
        }



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