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
   
    //Al empezar
    private void Awake()
    {
        //Recuperamos el amor que habia guardado previeamente(si no hay se le pone 1 por defecto)
        Amor = PlayerPrefs.GetInt("Amor", 1);
        //Lo mismo se hace con los estados actuales (forma)
        EstadoActual =  PlayerPrefs.GetInt("EstadoActual", 1);
        
        //Si los playerprefs guardaron un 0 hambre (o no habia ninguno) significa que no se guardo con hambre
        if (PlayerPrefs.GetInt("Hambre", 0) == 0) 
        {
            Hambre= false;

        }
        //Si se guardo con otro numero (1) significa que si tenia
        else 
        {
            Hambre= true;  
        }
        //Comprobamos que dependiendo del nivel de amor tendrá  una determinada forma.
        CambiarEstado();
    }


    public void Start()
    {
        //Comprobamos nada mas empezar la partida, si la mascota esta viva o muerta.
        ComprobarMuerte();
        //Y hacemos que se vea en los sliders y togles de la interfaz los niveles de amor y si tiene hambre o no
        Interfaz.Singleton.sliderAmor.value = Amor;
        Interfaz.Singleton.toggleHambre.isOn = Hambre;
    }

    //En esta Funcion  sirve para que reciba o pierda amor (se pone entre parentesis en el nombre de la funcion )
    public void CambiarAmor(int DarAmor)
    {
        //Si la mmascota no esta en la forma 0 (muerta) podrá recibir o perder amor
        if(EstadoActual != 0) 
        {
            //Será igual al que ya tiene mas lo que  se le sume o reste
            Amor = Amor + DarAmor;
            //Este nuevo amor se guardará en los playerprefs
            PlayerPrefs.SetInt("Amor", Amor);

            //Comprobamos si debe cambiar de forma en base al amor 
            CambiarEstado();
            Debug.Log(Amor);

            //si tiene menos que 0
            if (Amor < 0)
            {
                //entonces se pone que sea siempre -1`para que no baje mas
                Amor = -1;
            }
            //si tiene mas de 100

            else if (Amor > 100)
            {
                // se pone 100 para que no suba mas
                Amor = 100;
            }
            //se cambiara en la interfaz el valor del amor en el slider
            Interfaz.Singleton.sliderAmor.value = Amor;
            //se comprueba si la mascota esta muerta o viva
            ComprobarMuerte();
        }
    }
    
    //esta funcion controlará cuanto podrá comer en lo que duré el minijuego (es parecida a la de amor)
    public void DarDeComer(int amor)
    {   //si tiene hambre subirá el amor si come o bajará si se come un obstaculo
        if(Hambre == true)
        {   //Si el amor que le damos es mayor a 0 (no es un obstaculo)
            if (amor > 0)
            {
                //guardamos el amor que consigue durante el minijuego (y el que ya tenga mas este)
                amorAlimentando = +amor;
                //y si este amor obtenido en el juego es menor  al maximo que se puede conseguir en el juego
                if (amorAlimentando <= maxAmorAlAlimentar)
                {
                    //entonces subira su nivel de amor y se activarán las particulas
                    CambiarAmor(amor);
                    StartCoroutine(MostrarCorazones());
                }
                //si obtiene todo el amor permitido no le subirá mas
            }
            // si el amor es menor que 0 (obstaculo)

            else
            {
                //le restara amor 
                CambiarAmor(amor);
            }

        }
        //Si no tiene hambre, solo podrá bajarle el amor pero si se come un obstaculo le bajara, pero no que le subira.

        else
        {
            // si el amor es menor que 0 (obstaculo)
            if (amor < 0)
            {   
                //entonces se le resta amor
                CambiarAmor(amor);
            }
            //tambien se le añadirá la funcion de engordar (se ensanchará en el eje x)
            else
            {
                engordar();
            }
            
        } 
    }
   //
    public IEnumerator MostrarCorazones()  
    {
        //nada mas terminar se activan 
        particulas.SetActive(true);

        // se espera un segundo 
        yield return new WaitForSeconds(1f);

        //y se desactivan
        particulas.SetActive(false); 

    }

    //en esta funcion  hará que cuando se pare el minijuego de cualquier forma se compruebe de que no tenga hambre si llegó a comer algo
    public void AcabarAlimentar()
    {
        //si recibio amor mayor que 0 (comio una comida)
        if(amorAlimentando > 0)
        {
            //no tendrá hambre
            Hambre = false;
            //se ejecuta la funcion del datemanager de que ha comido para calcular la prox hora que tendrá hambre y perderá puntos por tenerla
            DateManager.Singleton.HaComido();
            //y se cambiará en el toggle de la interfaz 
            Interfaz.Singleton.toggleHambre.isOn = Hambre;
        }
        //Se resetean los puntos que ha recibido en el minijuego para volver a recibirlos la prox vez

        amorAlimentando = 0;
        //se pone la mascota donde estaba (vuelve al centro)
        transform.position = posInicial;
    }

    //esta funcion comprobará cuando la mascota baja a 0 y muere
    public void ComprobarMuerte()
    {
        // si el amor baja a 0 se queda asi
        if(Amor <= 0)
        {
            //le indicamos que sequeda muerta
            muerta = true;
            //ya no tendrá hambre más
            Hambre = false;

            //se activa en la interfaz que al estar muerta se mostrará solo la opcion de resetear
            Interfaz.Singleton.MascotaMuerta();
            Interfaz.Singleton.sliderAmor.value = Amor;
            Interfaz.Singleton.toggleHambre.isOn = Hambre;

            //aqui le indicamos que se quede en el centro por si se muere moviendose
            transform.position = posInicial;
        }
        // si el amor esta por debajo de 0 o es 0
        else
        {
            //entonces indicamos que no esta muerta
            muerta = false;
        }
    }

    //esta funcion hara que el aspecto cambio segun su nivel de amor.
    public void CambiarEstado() 
    {
        // este bucle hace que las acciones se repitan una infinidad de veces y que se pasen por todos los estados establecidos y desactivarlos (para evitar)
        for(int i = 0; i < Estados.Length; i++)
        {
            Estados[i].SetActive(false);
        }


        //despues se comprueba si el amor es menor a 0
        if (Amor<= 0)
        {
            //En ese caso el estado actual será la muerte(0) y se guardará como el actual
            Estados[0].SetActive(true);
            EstadoActual=0;
            Debug.Log("Se ha Muerto");
        }

        else if(Amor>0 && Amor<=5) 
        {
            Estados[1].SetActive(true);
            EstadoActual=1;
            Debug.Log("Has ascendido a Bebe");

        }
        else if (Amor>5 && Amor<=20)
        {
            Estados[2].SetActive(true);
            EstadoActual=2;
            Debug.Log("Has ascendido a Junior");

        }
        else if (Amor>20 && Amor<=60)
        {
            Estados[3].SetActive(true);
            EstadoActual=3;
            Debug.Log("Has ascendido a Senior");

        }
        else if (Amor>60)
        {
            Estados[4].SetActive(true);
            EstadoActual=4;
            Debug.Log("Has ascendido a Queen");

        }
        //Cuando se acabe guardamos cual es el estado actual en el player prefs 
        PlayerPrefs.SetInt("EstadoActual",EstadoActual);
        // se crea un nuevo bucle para mantener el tamaño establecido en todos los estados (para luego ser modificados)
        for (int i = 1; i < Estados.Length; i++)
        {
            //se indica que al comer sin hambre engordara en varios ejes (sobretodo eje x)
            Estados[i].transform.localScale =new Vector3(PlayerPrefs.GetFloat("gorduraX", 1f), PlayerPrefs.GetFloat("gorduraY", 1f), PlayerPrefs.GetFloat("gorduraZ", 1f));
            
        } 
    }
    //esta funcion hará que cuando no tenga hambre engrdará si come
    public void engordar()
    {
        // se crea un nuevo bucle para que en todos los estados afecte la funcion de engordar
        for (int i =1; i < Estados.Length; i++)
        {
            //se establece el limite que podrán tener  todos los estados por igual
            if (Estados[i].transform.localScale.x<1.7f)
            {
                //ademas de el tamaño en el que irá creciendo hasta alcanzar el limite
                Estados[i].transform.localScale = Estados[i].transform.localScale + new Vector3 (0.20f, 0.02f, 0.02f);
               
            }


        }
        //en este apartado se esteblece que esta funcion solo podrá funcionar a partir del estado bebe ademas d eque se guarden en el,playerprefs (para que la tumba no engorde tambien)
        PlayerPrefs.SetFloat("gorduraX", Estados[1].transform.localScale.x);
        PlayerPrefs.SetFloat("gorduraY", Estados[1].transform.localScale.y);
        PlayerPrefs.SetFloat("gorduraZ", Estados[1].transform.localScale.z);
        Debug.Log( "estoyengordando: "+ Estados[1].transform.localScale.x);
    }
} 