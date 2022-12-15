using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour

{
    public GameObject mascota;

    Mascota scriptMascota;

    public float tiempoParaHambre=20f; 

    public float tiempoParaAmor=30f;

    public string horadehambre;

    string hourHambreString;

    string ultimaHoraQueRestoPuntosPorHambre;

    [SerializeField]

    int pointsLove = 100;

    [SerializeField]

    float frecuenciaRestadoPuntosPorAlimentacion = 5f;



    // Start is called before the first frame update

    void Start()

    {
        scriptMascota = mascota.GetComponent<Mascota>();

        Debug.Log("Ahora mismo = " + DateTime.Now.ToString());

        if(PlayerPrefs.GetString("horadehambre")=="")
        {
            horadehambre=DateTime.Now.AddMinutes(tiempoParaHambre).ToString();
            PlayerPrefs.SetString("horadehambre", horadehambre);
        }
        else
        {
            horadehambre=PlayerPrefs.GetString("horadehambre");
        }

        /*
        //Para probar el funcionamiento lo primero que hago

        //es calcular cuando va a tener hambre para simplificarlo 

        //le digo que será en 15 segundos....

        DateTime cuandoTendraHambre = DateTime.Now.AddSeconds(15);



        //Almaceno en un string la hora de cuando tendrá hambre

        //esto lo hago para poder guardar como string en playerprefs

        //las diferentes fechas.

        hourHambreString = cuandoTendraHambre.ToString();

        Debug.Log("Tendra hambre a las " + hourHambreString);



        ultimaHoraQueRestoPuntosPorHambre = DateTime.Now.ToString();


*/
    }




    void Update()

    {

        //Carga desde un string (podría ser un string sacado desde player prefs...) la fecha (con hora mes y dias...)

        /*

        DateTime cuandoTendraHambre = DateTime.Parse(hourHambreString);



        //Comparo la fecha de cuando tendrá hambre con la actual.

        //En caso de haberse pasado la hora de comer, se mostrará el mensaje.

        if (IsHungry() && PuedePerderPuntos())

        {

            Debug.Log("Tiene Hambre");

            pointsLove = pointsLove - 1;

            ultimaHoraQueRestoPuntosPorHambre = DateTime.Now.ToString();

        }
        */
        //comprobarHambre();
    }



    public bool PuedePerderPuntos()

    {

        //Ultima vez que le resté puntos

        DateTime ultimaVezRestado = DateTime.Parse(ultimaHoraQueRestoPuntosPorHambre);

        return ultimaVezRestado.AddSeconds(frecuenciaRestadoPuntosPorAlimentacion) < DateTime.Now;

    }



    public bool IsHungry()

    {

        //Ultima vez que ha comido

        DateTime cuandoTendraHambre = DateTime.Parse(hourHambreString);

        return cuandoTendraHambre < DateTime.Now;

    }



    public void GiveFood()

    {

        if (IsHungry())

        {

            DateTime cuandoTendraHambre = DateTime.Now.AddSeconds(15);

            hourHambreString = cuandoTendraHambre.ToString();

            pointsLove += 10;

        }

        else

        {

            Debug.Log("Estoy engordando...");
            
        }

    }
   
    public void comprobarHambre()
    {
       if(scriptMascota.Hambre==true)
       {



       }

       else 
       {

            if (DateTime.Parse(DateTime.Now.ToString())>=DateTime.Parse(horadehambre))
            {
                scriptMascota.Hambre=true;


            }
        
       }
    } 
}