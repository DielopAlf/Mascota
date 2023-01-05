using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mascota : MonoBehaviour
{
    public bool Hambre;
    public int Amor;

    int amorAlimentando;
    public int maxAmorAlAlimentar = 12;

    public GameObject particulas;

    public GameObject [] Estados;
    public int EstadoActual =0;

    public bool muerta;

    public Vector3 posInicial;
   
    //Nada mas empezar
    private void Awake()
    {
        //Recuperamos cuanto amor tenia guardada la mascota de partidas anteriores (si no tenia nada, se le da un 1 de base)
        Amor = PlayerPrefs.GetInt("Amor", 1);
        //Lo mismo para saber cual era su estado actual (su forma)
        EstadoActual =  PlayerPrefs.GetInt("EstadoActual", 1);
        
        //Si en los playerprefs habiamos guardado un 0 en hambre (o no habia ningun valor gurado), entonces esto significa que guardamos que no tenia hambre
        if (PlayerPrefs.GetInt("Hambre", 0) == 0)
        {
            Hambre= false;

        }
        //Si habia guarado otro numero en hambre de los playerprefs (un 1), entonces esto signfica que si tenia hambre
        else 
        {
            Hambre= true; 
        }

        //Comprobamos en base a cuando amor tenga, cual es la forma que debe tener
        CambiarEstado();
    }


    public void Start()
    {
        //Al empezar la partida, comprobamos si la mascota esta muerta o vivia
        ComprobarMuerte();

        //Y hacemos que se vea en la interfaz en sliders y toggles, el amor que tiene y si tiene o no hambre
        Interfaz.Singleton.sliderAmor.value = Amor;
        Interfaz.Singleton.toggleHambre.isOn = Hambre;
    }


    //Funcion que permite darle amor o quitarle amor. El que se le da o se le quita, es el que se pone entre parentesis al llamar a la funcion (int darAmor)
    public void CambiarAmor(int DarAmor)
    {
        //Si la mascota esta en una forma que no sea la 0 (la de muerte), entonces puede recibir o perder amor
        if(EstadoActual != 0)
        {
            //El amor sera igual al que ya tiene mas lo que le hayamos dado o quitado
            Amor = Amor + DarAmor;
            //Guardamos este nuevo amor en los playerprefs
            PlayerPrefs.SetInt("Amor", Amor);

            //Comprobamos si debe cambiar de forma en base al amor
            CambiarEstado();
            Debug.Log(Amor);

            //Si el amor bajo de 0
            if (Amor < 0)
            {
                //Entonces ponemos el amor siempre a -1, para que no baje mas de aqui
                Amor = -1;
            }
            else if (Amor > 100)
            {
                //Si subio de 100, lo ponemos a 100, para que no suba mas de aqui
                Amor = 100;
            }

            //Cambiamos en la interfaz el valor del slider del amor
            Interfaz.Singleton.sliderAmor.value = Amor;

            //Comprobamos si la mascota esta muerta o viva
            ComprobarMuerte();
        }
    }
    

    //Funcion que es es similar a la de dar amor, pero especificamente para cuando come, para controlar cuanto puede comer de una sentada (en un minijuego)
    public void DarDeComer(int amor)
    {
        //Si la mascota tiene hambre, entonces le subiremos amor si come (o se lo bajaremos si se come un obstaculo)
        if(Hambre == true)
        {
            //Si el amor que le damos es mayor a 0 (no es un obstaculo)
            if(amor > 0)
            {
                //Guardamos cuando amor a conseguido durante este minijuego (el que ya hubiera conseguido en el mas este)
                amorAlimentando = +amor;

                //Y si este amor conseguido este minijuego es aun menor al maximo que puede conseguir por minijuego
                if (amorAlimentando <= maxAmorAlAlimentar)
                {
                    //Entonces si le subimos el amor y activamos las particulas
                    CambiarAmor(amor);
                    StartCoroutine(MostrarCorazones());
                }
                //Si ya ha conseguido todo el amor que puede conseguir de una sentada, no hacemos nada, no le subimos el amor
            }
            //Si el amor que le damos es menor que 0 (es decir, se come un obstaculo)
          

            else
            {
                //Entonces si que le restamos amor independientemente de cualquier otra cosa
                CambiarAmor(amor);
            }

        }
        

        //Si no tiene hambre, lo unico que permitimos es que le baje el amor si se come un obstaculo que le quite amor, pero no que le suba
        else
        {
            //Si el amor que le damos es menor que 0 (se come un obstaculo)
            if(amor < 0)
            {   
                //Entonces si le restamos este amor
                CambiarAmor(amor);
            }
            else
            {
            
            transform.scale =  GameObject.transform.scale + new Vector3 (0f,0.1f,0f);
            
            }
        }
       
    }
   
    //Funcion temporizada para activar y desactivar las particulas
    IEnumerator MostrarCorazones()
    {
        //Nada mas ser llamada activa las particulas
        particulas.SetActive(true);

        //Espera 1 segundo
        yield return new WaitForSeconds(1f);

        //Las vuelve a desactivar
        particulas.SetActive(false);

    }


    //Funcion que se llama cuando ya se para el minijuego de alimentar, para comprobar que si comio algo, ya no tenga hambre
    public void AcabarAlimentar()
    {
        //Si el amor que recibio durante el minijuego fue mayor que 0 (es decir, se comio algun trozo de comida)
        if(amorAlimentando > 0)
        {
            //Entonces ya no tiene hambre
            Hambre = false;

            //Se llama la funcion de que ha comido en el controlador de horas, para calcular cual sera la proxima hora a la que tendra hambre y perdera puntos de amor por tener hambre
            DateManager.Singleton.HaComido();

            //Y se cambia en la interfaz la cajita que indica que tiene hambre
            Interfaz.Singleton.toggleHambre.isOn = Hambre;
        }
      
        //Reseteamos los puntos que ha recibido durante el minijuego para que este listo para el proximo minijuego
        amorAlimentando = 0;

        //Ponemos a la masctoa en el centro de nuevo
        transform.position = posInicial;
    }


    //Funcion que comprueba si el amor de la mascota baja de 0 y muere
    public void ComprobarMuerte()
    {
        //Si el amor baja de 0 o es 0
        if(Amor <= 0)
        {
            //Entonces consideramos que la mascota esta muerta
            muerta = true;
            //Ya no sufre hambre
            Hambre = false;

            //Activamos en la interfaz que esta muerta, para que solo se nos muestre la opcion de resetear la partida
            Interfaz.Singleton.MascotaMuerta();
            Interfaz.Singleton.sliderAmor.value = Amor;
            Interfaz.Singleton.toggleHambre.isOn = Hambre;

            //La llevamos al centro por si muere durante el minijuego lejos del centro
            transform.position = posInicial;
        }
        //Si el amor no esta por debajo de 0 o es 0
        else
        {
            //Entonces decimos que no esta muerta
            muerta = false;
        }
    }

    //Cambiar el aspceto de la mascota en base al amor que tenga
    public void CambiarEstado()
    {
        //Siempre que se llama, hacemos un bucle para pasar por todos los psoibles estados y descativarlos todods (Para evitar que puedan quedar dos formas activas a la vez)
        for(int i = 0; i < Estados.Length; i++)
        {
            Estados[i].SetActive(false);
        }



        //Despues comprobamos si el amor es menor a 0
        if (Amor<= 0)
        {
            //En ese caso, decimos que el estado acutal  es 0 (el de la muerte) y guardamos ese estado como el actual
            Estados[0].SetActive(true);
            EstadoActual=0;
        }
        //Despues seguimos comprobamos si el amor esta entre unos rangos, y segun el rango, activamos un aspecto en espeficico y guardamos ese estado correspondiente

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

        //Al acabr, guardamos cual es el estado acutal en el playerprefs
        PlayerPrefs.SetInt("EstadoActual",EstadoActual);
    }
    
} 