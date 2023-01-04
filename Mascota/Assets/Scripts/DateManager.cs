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
     

    //Declara el singleton para poder acceder a este manager del tiempo desde cualquier otro codigo
    public void Awake()
    {
        //Si ya hay algun otro DateManager creado (no deberia) destruye este para que solo haya 1
        if(Singleton != null && Singleton != this)
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


        //Siempre y cuando la mascota no haya muerto
        if(scriptMascota.muerta != true)
        {
            comprobarHambreStart();
            comprobarAmorStart();
        }
    }



    //Cada frame, se comprueba si ha pasado el tiempo necesario mientras juegas para que empiece a pasar hambre, y si es el caso, el tiempo necesario para restarle amor
    //Exactamente lo mismo con las caricias
    void Update()
    {
        //Siempre y cuando la mascota no haya muerto
        if(scriptMascota.muerta != true)
        {
            comprobarHambreUpdate();
            comprobarAmorUpdate();
        }
    }





    //Funcion que comprueba si, al iniciar el juego, existia una hora de pasar hambre, y de hacerlo, cuanto ha pasado y cuanto amor se debe descontar
    public void comprobarHambreStart()
    {
        //Si no se habia guardado ninguna hora para pasar hambre (primera vez que se ejecuta), entonces se ejecuta la misma funcion que si justo hubiera comido, para que se guarde la siguiente hora de hambre y se establezca que no tiene hambre
       if(PlayerPrefs.GetString("horaparacomer")=="")
       {
            HaComido();
       }


       //En caso de que si hubiera alguna hora de hambre guardada, se realizan comprobaciones para saber si ya deberia haber perdido puntos
       else 
       {
            //Recuperamos la hora de hambre almacenada
            str_horadehambre = PlayerPrefs.GetString ("horaparacomer");

            //Si la hora actual es mas tarde de la hora a la que tenia hambre, se considera que tiene hambre y se calcula cuanto ha pasado del tiempo necesario para que teniendo hambre, pierda 1 punto de amor
            if (DateTime.Now>DateTime.Parse(str_horadehambre)) 
            {
                //Cuantos minutos han pasado desde la hora que tenia hambre hasta la hora actual
                float tiempotranscurrido = (float)(DateTime.Now-DateTime.Parse(str_horadehambre)).TotalMinutes;
                
                //Se calcula cuentas veces ha debido perder 1 punto de amor en base a cuanto ha transucrrido y cada cuanto considerabamos que perida 1 punto de amor teniendo hambre
                int repeticiones = (int) Mathf.Ceil(tiempotranscurrido/tiempoparaperderpuntos); 

                //Se ejecuta un ciclo ese numero de veces para quitarle la cantidad correspondiente de amor
                for(int i = 0; i < repeticiones;i++)
                {
                    scriptMascota.CambiarAmor(-1); 
                }


                //Se establece la siguiente hora a la que vuelve a perder un punto de amor por tener hambre
                str_pierdeamorporhambre = DateTime.Now.AddMinutes(tiempoparaperderpuntos).ToString(); 
                PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);

                //Se establece que tiene hambre
                scriptMascota.Hambre=true;

                //Se llama al singleton de la interfaz para cambiar el icono de hambre
                Interfaz.Singleton.toggleHambre.isOn = scriptMascota.Hambre;
            }
            //Si la hora actual no es mas tarde de la hora a la que tiene hambre
            else 
            {
                //Calculamos cuando perdera puntos de amor en abse a la hora a la que va a tener hambre
                str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString(); 
                PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);

                //Establecemos que aun no tiene hambre
                scriptMascota.Hambre=false;

                //Se llama al singleton de la interfaz para cambiar el icono del hambre
                Interfaz.Singleton.toggleHambre.isOn = scriptMascota.Hambre;

            }
       }
    }



    //Funcion que se encarga de comprobar mientras se ejecuta el juego (cada frame), si la mascota tiene hambre, y de no tenerla, si ha pasado la hora de tenerla
    public void comprobarHambreUpdate()
    {

        //Si la mascota tiene hambre
        if(scriptMascota.Hambre == true)
        { 
            //Comprobamos si la hora actual es mas tarde de la que deberia haber perdido 1 punto de amor por tener hambre
            if(DateTime.Now>DateTime.Parse(str_pierdeamorporhambre)) 
            {
                //Si lo es, establecemos la proxima hora a la que perdera otro punto por tener hambre, y le quitamos ese punto de amor
                str_pierdeamorporhambre = DateTime.Now.AddMinutes(tiempoparaperderpuntos).ToString(); 
                PlayerPrefs.SetString("horapierdepuntos",str_pierdeamorporhambre);
                scriptMascota.CambiarAmor(-1);
            }
            Debug.Log("TieneHambre");

        }
        //Si la mascota no tiene hambre
        else 
        {
            //Comprobamos si la hora actual es mas tarde de la que deberia para tener hambre
             if(DateTime.Now>DateTime.Parse(str_horadehambre)) 
             {
                //Si si lo es, establecemos que ya tiene hambre
                scriptMascota.Hambre=true;

                //Llamamos al singleton de la interfaz para cambiar el icono de hambre
                Interfaz.Singleton.toggleHambre.isOn = scriptMascota.Hambre;

            }
             Debug.Log("NotIENEHAMBRE");

        }
    }


    //Al inicial la partida se comprueba si no hay ninguna hora almacenada en los PlayerPrefs de la hora a la que quiere caricias (primera vez que se ejcuta) o si hay alguna almacenada
    public void comprobarAmorStart()
    {

        //Si no hay ninguna hora para caricica almacenada (esta vacia, o ""), se ejecuta la misma logica que cuando se acaricia, para calcular la proxima hora a la que querra caricia
       if(PlayerPrefs.GetString("horaparaCaricia")=="")
       {
            HaSidoAcariciada();
       }

       //En cambio, si existe alguna hora para caricia guardada en los playersPrefs
       else 
       {
            //La recuperamos de los player prefs en forma de cadena
            str_horadeCaricia = PlayerPrefs.GetString ("horaparaCaricia");

            //Comprobamos si la hora actual es despues de la hora que estaba guardada para querer una caricia
            if (DateTime.Now > DateTime.Parse(str_horadeCaricia)) 
            {
                //Si es asi, calculamos cuanto tiempo ha pasado desde que queria la caricia hasta ahora (en minutos)
                float tiempotranscurrido = (float)(DateTime.Now-DateTime.Parse(str_horadeCaricia)).TotalMinutes;
                
                //Sabiendo este tiempo, y cada cuando pierde amor si quiere una caricia, calculamos cuantas veces deberia haber perdido amor
                int repeticiones = (int) Mathf.Ceil(tiempotranscurrido/tiempopierdeamorporCaricia); 

                //Hacemos un bucle que se repetira tantas veces como veces deberia haber perdido amor (el int repeticiones que calculamos antes)
                for(int i = 0; i < repeticiones;i++)
                {
                    //Le quitamos 1 de amor por cada una de estas veces que deberia haber perdido amor
                    scriptMascota.CambiarAmor(-1); 
                }

                //Despues calculamos contando desde la hora actual, cual sera la proxima hora a la que pierda amor por querer caricia, y lo guardamos en el player prefs
                str_pierdeamorporCaricia = DateTime.Now.AddMinutes(tiempopierdeamorporCaricia).ToString(); 
                PlayerPrefs.SetString("horapierdeamor",str_pierdeamorporCaricia);
               
            }
            //Si la hora actual no es despues de la hora a la que quiere una caricia
            else 
            {
                //Entonces calculamos cuando perdera amor por querer caricia, pero no en base a la hora actual, sino a la hora a la que va a querer una caricia
                str_pierdeamorporCaricia = DateTime.Parse(str_horadeCaricia).AddMinutes(tiempopierdeamorporCaricia).ToString(); 
                PlayerPrefs.SetString("horapierdeamor",str_pierdeamorporCaricia);

            }
       }
    }

    //Mientras el juego se ejecuta, comprobamos cada frame si ha pasado la hora para querer una caricia, y si lo ha hecho, si ha pasado la hora para perder amor por querer caricias
    public void comprobarAmorUpdate()
    {
        //Si la hora actual es despues de la hora a la que queria una caricia
        if(DateTime.Now>DateTime.Parse(str_horadeCaricia))
        { 
            //Comprobamos si la hora actual tambien es despues a la hora a la que pierde amor por querer caricia
            if(DateTime.Now>DateTime.Parse(str_pierdeamorporCaricia)) 
            {
                //Si es asi, le quitamos un punto de amor, y calculamos la siguiente hora a la que perdera un punto de amor por querer una caricia
                str_pierdeamorporCaricia = DateTime.Now.AddMinutes(tiempopierdeamorporCaricia).ToString(); 
                PlayerPrefs.SetString("horapierdeamor",str_pierdeamorporCaricia);
                scriptMascota.CambiarAmor(-1);
            }
            Debug.Log("quierecaricia");
        }
        //Si la hora actual no es despues de la hora a la que queria una caricia, no hacemos nada
        else 
        {
            Debug.Log("noquierecaricia");

        }
    }



    //Cuando la mascota reciba comida llamaremos a esta funcion, para que la hora a la que pase hambre y pierda puntos por pasar hambre se actualice y sea mas tarde respecto a la hora actual
    public void HaComido()
    {
        //La proxima hora para querer comida sera la hora actual mas los minutos que hayamos decidido
        str_horadehambre = DateTime.Now.AddMinutes(tiempoParaHambre).ToString();
        PlayerPrefs.SetString("horaparacomer", str_horadehambre);

        //La proxima hora para perder amor por pasar hambre sera la hora a la que le dara hambre mas los minutos que hayamos decidido para que perida amor por tener hambre
        str_pierdeamorporhambre = DateTime.Parse(str_horadehambre).AddMinutes(tiempoparaperderpuntos).ToString();
        PlayerPrefs.SetString("horapierdepuntos", str_pierdeamorporhambre);
    }


    //Cuando la mascota reciba una caricia, llamaremos a esta funcion para que la hora a la que querra caricia y perdera punto si no la recibe se actualice y sea mas tarde respecto a la hora actual
    public void HaSidoAcariciada()
    {
        //La proxima hora para querer caricia sera la hora actual mas los minutos que hayamos decidido
        str_horadeCaricia = DateTime.Now.AddMinutes(tiempoParaCaricia).ToString();
        PlayerPrefs.SetString("horaparaCaricia", str_horadeCaricia);

        //La proxima hora para perder amor por no recibir caricia sera la hora a la que quiere caricia, mas los minutos que deben pasar para que pierda amor por querer caricia
        str_pierdeamorporCaricia = DateTime.Parse(str_horadeCaricia).AddMinutes(tiempopierdeamorporCaricia).ToString();
        PlayerPrefs.SetString("horapierdeamor", str_pierdeamorporCaricia);
    }
}