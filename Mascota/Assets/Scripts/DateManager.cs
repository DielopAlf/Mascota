using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour

{
    public GameObject mascota;

    Mascota scriptMascota;

    [Header ("tiempo Para Hambre (en mins)") ]
    public float tiempoParaHambre=180f;
    public float tiempoparaperderpuntos=20f;
     string  str_horadehambre;
     string  str_pierdeamorporhambre;

     [Header ("tiempo Para Amor (en mins)") ]
     public float tiempoParaCaricia=1440f;
     string  str_horadeCaricia;
     string  str_pierdeamorporCaricia;
     public float tiempopierdeamorporCaricia=30f;
     

    void Start()

    {
        scriptMascota = mascota.GetComponent<Mascota>();

       // comprobarHambreStart();
        comprobarAmorStart();
    }


    void Update()

    {
       //comprobarHambreUpdate();
       comprobarAmorUpdate();

    }

    public void comprobarHambreStart()
    {
       if(PlayerPrefs.GetString("horaparacomer")=="")
       {
       
        str_horadehambre=DateTime.Now.AddMinutes(tiempoParaHambre).ToString();

        PlayerPrefs.SetString("horaparacomer",str_horadehambre);
        str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString(); 
        PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);
        scriptMascota.Hambre=false;
       }

       else 
       {

            str_horadehambre = PlayerPrefs.GetString ("horaparacomer");
            if (DateTime.Now>DateTime.Parse(str_horadehambre)) 
            {
                float tiempotranscurrido = (float)(DateTime.Now-DateTime.Parse(str_horadehambre)).TotalMinutes;
                
                int repeticiones = (int) Mathf.Ceil(tiempotranscurrido/tiempoparaperderpuntos); 
                for(int i = 0; i < repeticiones;i++)
                {
                    scriptMascota.CambiarAmor(-1); 
                }
                str_pierdeamorporhambre = DateTime.Now.AddMinutes(tiempoparaperderpuntos).ToString(); 
                PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);
                scriptMascota.Hambre=true;
            }
            else 
            {
                str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString(); 
                PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);
                scriptMascota.Hambre=true;

            }
       }
    }
    public void comprobarHambreUpdate()
    {
        if(scriptMascota.Hambre == true)
        { 
            if(DateTime.Now>DateTime.Parse(str_pierdeamorporhambre)) 
            {
                str_pierdeamorporhambre = DateTime.Now.AddMinutes(tiempoparaperderpuntos).ToString(); 
                PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);
                scriptMascota.CambiarAmor(-1);
            }
            Debug.Log("TieneHambre");

        }
        else 
        {
             if(DateTime.Now>DateTime.Parse(str_horadehambre)) 
             
             {
                scriptMascota.Hambre=true;
                
             }
             Debug.Log("NotIENEHAMBRE");

        }
    }



    public void comprobarAmorStart()
    {
       if(PlayerPrefs.GetString("horaparaCaricia")=="")
       {
       
        str_horadeCaricia=DateTime.Now.AddMinutes(tiempoParaCaricia).ToString();

        PlayerPrefs.SetString("horaparaCaricia",str_horadeCaricia);
        str_pierdeamorporCaricia= DateTime.Parse(str_horadeCaricia).AddMinutes(tiempopierdeamorporCaricia).ToString(); 
        PlayerPrefs.SetString("horapierdeamor",str_pierdeamorporCaricia);
       }

       else 
       {

            str_horadeCaricia = PlayerPrefs.GetString ("horaparaCaricia");
            if (DateTime.Now>DateTime.Parse(str_horadeCaricia)) 
            {
                float tiempotranscurrido = (float)(DateTime.Now-DateTime.Parse(str_horadeCaricia)).TotalMinutes;
                
                int repeticiones = (int) Mathf.Ceil(tiempotranscurrido/tiempopierdeamorporCaricia); 
                for(int i = 0; i < repeticiones;i++)
                {
                    scriptMascota.CambiarAmor(-1); 
                }
                str_pierdeamorporCaricia = DateTime.Now.AddMinutes(tiempopierdeamorporCaricia).ToString(); 
                PlayerPrefs.SetString("horapierdeamor",str_pierdeamorporCaricia);
               
            }
            else 
            {
                str_pierdeamorporCaricia = DateTime.Parse(str_horadeCaricia).AddMinutes(tiempopierdeamorporCaricia).ToString(); 
                PlayerPrefs.SetString("horapierdeamor",str_pierdeamorporCaricia);
               

            }
       }
    }
    public void comprobarAmorUpdate()
    {
        if(DateTime.Now>DateTime.Parse(str_horadeCaricia))
        { 
            if(DateTime.Now>DateTime.Parse(str_pierdeamorporCaricia)) 
            {
                str_pierdeamorporCaricia = DateTime.Now.AddMinutes(tiempopierdeamorporCaricia).ToString(); 
                PlayerPrefs.SetString("horapierdeamor",str_pierdeamorporCaricia);
                scriptMascota.CambiarAmor(-1);
            }
            Debug.Log("quierecaricia");
        }
        else 
        {
                    Debug.Log("noquierecaricia");

        }
    }

}