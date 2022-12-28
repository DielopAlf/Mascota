using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour

{
    public GameObject mascota;

    Mascota scriptMascota;

    public static DateManager Singleton;


    [Header("tiempo Para Hambre (en mins)")]
    public float tiempoParaHambre = 180f;
    public float tiempoparaperderpuntos = 20f;
    string str_horadehambre;
    string str_pierdeamorporhambre;

    [Header("tiempo Para Amor (en mins)")]
    public float tiempoParaCaricia = 1440f;
    string str_horadeCaricia;
    string str_pierdeamorporCaricia;
    public float tiempopierdeamorporCaricia = 30f;


    //Declara el singleton para poder acceder a este manager del tiempo desde cualquier otro codigo
    public void Awake()
    {
        //Si ya hay algun otro DateManager creado (no deberia) destruye este para que solo haya 1
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        //Si no, este es el unnico, y este debe ser el singleton
        else
        {
            Singleton = this;
        }
    }



    //En el start almacena al codigo de la masctoa para poder acceder a sus funciones, como quitarle amor, comprobar si tiene hambre, etc
    //Ademas, ejecuta funciones que comprueban si la mascota ya tenia tiempos para pasar hambre o necesidad de caricias almacenados, y si los tenia, le descuenta la canitdad de amor que corresponda
    void Start()
    {
        scriptMascota = mascota.GetComponent<Mascota>();

        comprobarHambreStart();
        comprobarAmorStart();
    }



    //Cada frame, se comprueba si ha pasado el tiempo necesario mientras juegas para que empiece a pasar hambre, y si es el caso, el tiempo necesario para restarle amor
    //Exactamente lo mismo con las caricias
    void Update()
    {
        comprobarHambreUpdate();
        comprobarAmorUpdate();

    }





    //Funcion que comprueba si, al iniciar el juego, existia una hora de pasar hambre, y de hacerlo, cuanto ha pasado y cuanto amor se debe descontar
    public void comprobarHambreStart()
    {
        //Si no se habia guardado ninguna hora para pasar hambre (primera vez que se ejecuta), entonces se ejecuta la misma funcion que si justo hubiera comido, para que se guarde la siguiente hora de hambre y se establezca que no tiene hambre
        if (PlayerPrefs.GetString("horaparacomer")=="")
        {
            HaComido();
        }


        //En caso de que si hubiera alguna hora de hambre guardada, se realizan comprobaciones para saber si ya deberia haber perdido puntos
        else
        {
            //Recuperamos la hora de hambre almacenada
            str_horadehambre = PlayerPrefs.GetString("horaparacomer");

            //Si la hora actual es mas tarde de la hora a la que tenia hambre, se considera que tiene hambre y se calcula cuanto ha pasado del tiempo necesario para que teniendo hambre, pierda 1 punto de amor
            if (DateTime.Now>DateTime.Parse(str_horadehambre))
            {
                //Cuantos minutos han pasado desde la hora que tenia hambre hasta la hora actual
                float tiempotranscurrido = (float)(DateTime.Now-DateTime.Parse(str_horadehambre)).TotalMinutes;

                //Se calcula cuentas veces ha debido perder 1 punto de amor en base a cuanto ha transucrrido y cada cuanto considerabamos que perida 1 punto de amor teniendo hambre
                int repeticiones = (int)Mathf.Ceil(tiempotranscurrido/tiempoparaperderpuntos);

                //Se ejecuta un ciclo ese numero de veces para quitarle la cantidad correspondiente de amor
                for (int i = 0; i < repeticiones; i++)
                {
                    scriptMascota.CambiarAmor(-1);
                }


                //Se establece la siguiente hora a la que vuelve a perder un punto de amor por tener hambre
                str_pierdeamorporhambre = DateTime.Now.AddMinutes(tiempoparaperderpuntos).ToString();
                PlayerPrefs.SetString("horapierdepuntos", str_pierdeamorporhambre);

                //Se establece que tiene hambre
                scriptMascota.Hambre=true;
            }
            //Si la hora actual no es mas tarde de la hora a la que tiene hambre
            else
            {
                //Calculamos cuando perdera puntos de amor en abse a la hora a la que va a tener hambre
                str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString();
                PlayerPrefs.SetString("horapierdepuntos", str_pierdeamorporhambre);

                //Establecemos que aun no tiene hambre
                scriptMascota.Hambre=false;

            }
        }
    }



    //Funcion que se encarga de comprobar mientras se ejecuta el juego (cada frame), si la mascota tiene hambre, y de no tenerla, si ha pasado la hora de tenerla
    public void comprobarHambreUpdate()
    {

        //Si la mascota tiene hambre
        if (scriptMascota.Hambre == true)
        {
            //Comprobamos si la hora actual es mas tarde de la que deberia haber perdido 1 punto de amor por tener hambre
            if (DateTime.Now>DateTime.Parse(str_pierdeamorporhambre))
            {
                //Si lo es, establecemos la proxima hora a la que perdera otro punto por tener hambre, y le quitamos ese punto de amor
                str_pierdeamorporhambre = DateTime.Now.AddMinutes(tiempoparaperderpuntos).ToString();
                PlayerPrefs.SetString("horapierdepuntos", str_pierdeamorporhambre);
                scriptMascota.CambiarAmor(-1);
            }
            Debug.Log("TieneHambre");

        }
        //Si la mascota no tiene hambre
        else
        {
            //Comprobamos si la hora actual es mas tarde de la que deberia para tener hambre
            if (DateTime.Now>DateTime.Parse(str_horadehambre))
            {
                //Si si lo es, establecemos que ya tiene hambre
                scriptMascota.Hambre=true;

            }
            Debug.Log("NotIENEHAMBRE");

        }
    }



    public void comprobarAmorStart()
    {
        if (PlayerPrefs.GetString("horaparaCaricia")=="")
        {
            HaSidoAcariciada();
        }

        else
        {

            str_horadeCaricia = PlayerPrefs.GetString("horaparaCaricia");
            if (DateTime.Now > DateTime.Parse(str_horadeCaricia))
            {
                float tiempotranscurrido = (float)(DateTime.Now-DateTime.Parse(str_horadeCaricia)).TotalMinutes;

                int repeticiones = (int)Mathf.Ceil(tiempotranscurrido/tiempopierdeamorporCaricia);
                for (int i = 0; i < repeticiones; i++)
                {
                    scriptMascota.CambiarAmor(-1);
                }
                str_pierdeamorporCaricia = DateTime.Now.AddMinutes(tiempopierdeamorporCaricia).ToString();
                PlayerPrefs.SetString("horapierdeamor", str_pierdeamorporCaricia);

            }
            else
            {
                str_pierdeamorporCaricia = DateTime.Parse(str_horadeCaricia).AddMinutes(tiempopierdeamorporCaricia).ToString();
                PlayerPrefs.SetString("horapierdeamor", str_pierdeamorporCaricia);

            }
        }
    }
    public void comprobarAmorUpdate()
    {
        if (DateTime.Now>DateTime.Parse(str_horadeCaricia))
        {
            if (DateTime.Now>DateTime.Parse(str_pierdeamorporCaricia))
            {
                str_pierdeamorporCaricia = DateTime.Now.AddMinutes(tiempopierdeamorporCaricia).ToString();
                PlayerPrefs.SetString("horapierdeamor", str_pierdeamorporCaricia);
                scriptMascota.CambiarAmor(-1);
            }
            Debug.Log("quierecaricia");
        }
        else
        {
            Debug.Log("noquierecaricia");

        }
    }



    public void HaComido()
    {
        str_horadehambre = DateTime.Now.AddMinutes(tiempoParaHambre).ToString();

        PlayerPrefs.SetString("horaparacomer", str_horadehambre);
        str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString();
        PlayerPrefs.SetString("horapierdepuntos", str_pierdeamorporhambre);
        scriptMascota.Hambre = false;
    }

    public void HaSidoAcariciada()
    {
        str_horadeCaricia = DateTime.Now.AddMinutes(tiempoParaCaricia).ToString();

        PlayerPrefs.SetString("horaparaCaricia", str_horadeCaricia);
        str_pierdeamorporCaricia = DateTime.Parse(str_horadeCaricia).AddMinutes(tiempopierdeamorporCaricia).ToString();
        PlayerPrefs.SetString("horapierdeamor", str_pierdeamorporCaricia);
    }
}