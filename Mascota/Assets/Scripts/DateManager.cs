using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    public GameObject mascota;

    Mascota scriptMascota;

    public static DateManager Singleton;


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
     

    public void Awake()
    {
        if(Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }
    }



    
    void Start()
    {
        scriptMascota = mascota.GetComponent<Mascota>();


        if(scriptMascota.muerta != true)
        {
            comprobarHambreStart();
            comprobarAmorStart();
        }
    }



    
    void Update()
    {
        if(scriptMascota.muerta != true)
        {
            comprobarHambreUpdate();
            comprobarAmorUpdate();
        }
    }





    public void comprobarHambreStart()
    {
       if(PlayerPrefs.GetString("horaparacomer")=="")
       {
            HaComido();
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

                Interfaz.Singleton.toggleHambre.isOn = scriptMascota.Hambre;
            }
            else 
            {
                str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString(); 
                PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);

                scriptMascota.Hambre=false;

                Interfaz.Singleton.toggleHambre.isOn = scriptMascota.Hambre;

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
            Debug.Log( "TieneHambre --- perdera puntosamor a las:" + str_pierdeamorporhambre);

        }
        else 
        {
             if(DateTime.Now>DateTime.Parse(str_horadehambre)) 
             {
                scriptMascota.Hambre=true;

                Interfaz.Singleton.toggleHambre.isOn = scriptMascota.Hambre;

            }
            Debug.Log("NotIENEHAMBRE--- la tendra a las:"+   str_horadehambre);
        }
    }


    public void comprobarAmorStart()
    {

       if(PlayerPrefs.GetString("horaparaCaricia")=="")
       {
            HaSidoAcariciada();
       }

       else 
       {
            str_horadeCaricia = PlayerPrefs.GetString ("horaparaCaricia");

            if (DateTime.Now > DateTime.Parse(str_horadeCaricia)) 
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
            Debug.Log( "quierecaricia --- perdera amor a las : " + str_pierdeamorporCaricia);
        }
        else 
        {
            Debug.Log("noquierecaricia --- querrá a las : "+str_horadeCaricia);

        }
    }



    public void HaComido()
    {
        str_horadehambre = DateTime.Now.AddMinutes(tiempoParaHambre).ToString();
        PlayerPrefs.SetString("horaparacomer", str_horadehambre);

        str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString();
        PlayerPrefs.SetString("horapierdepuntos", str_pierdeamorporhambre);
    }


    public void HaSidoAcariciada()
    {
        str_horadeCaricia = DateTime.Now.AddMinutes(tiempoParaCaricia).ToString();
        PlayerPrefs.SetString("horaparaCaricia", str_horadeCaricia);

        str_pierdeamorporCaricia = DateTime.Parse(str_horadeCaricia).AddMinutes(tiempopierdeamorporCaricia).ToString();
        PlayerPrefs.SetString("horapierdeamor", str_pierdeamorporCaricia);
    }
}