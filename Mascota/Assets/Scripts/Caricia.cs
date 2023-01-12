using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Caricia : MonoBehaviour
{
    public GameObject mascota;

    Mascota scriptMascota;
    public GameObject particulas;
    bool clicmascota;
    public float tiempoDeCaricia = 2f;
    float timer = 2f;
    public int amorporCaricia = 10;

    string siguienteHoraCaricia;
    public float minsParaSiguienteCaricia = 20f;



    public void Start() 
    {
        //Se establece un timer para calcular la duracion de la caricia hasta recibir amor.
        timer = tiempoDeCaricia;
        //Guardamo el script de la mascota para poder acceder a sus funciones.
        scriptMascota = mascota.GetComponent<Mascota>();

        //Si no hay una hora de recibir caricia, se pone que esta vacio para recibir la caricia (no se recibe)
        if (PlayerPrefs.GetString("horaSiguienteCaricia") == "")
        {
            siguienteHoraCaricia = "";
        }
        //Si  la hay la almacenamos en la variable(para que se guarde)
        else
        {
            siguienteHoraCaricia = PlayerPrefs.GetString("horaSiguienteCaricia");
        }
    }
    
    //En cada momento se comprobará si clicamos en la mascota y se mueve el raton
    void Update()
    {
        RaycastHit h;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        //el rayo será lanzado  desde la camra  a donde apunte el raton.
        if (Input.GetMouseButtonDown(0))
        {
            //si el rayo golpea contra el objeto este deberá tener el tag de mascota para  ser identificado
            if (Physics.Raycast(r, out h))
            {
                if (h.collider.tag.Equals("Mascota"))
                {
                    clicmascota=true;
                    //se guardará el clickeado sobre la mascota (sera una booleana(toca o no toca)
                }
            }
        }
        // si el click se suelta  se guarda que ya no se está clickeando
        else if (Input.GetMouseButtonUp(0))
        {
            clicmascota=false;
        }


        //si se esta dando a la mascota
        if (clicmascota==true)
        {
            //haremos que el raton se mueva a ambos lados (arriba abajo derecha izquierda)
            if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
            {
                //aqui el timer empieza a funcionar y se descuenta el tiempo establecido para la caricia (siempre que se mueva el raton))
                timer -= Time.deltaTime;

                //si llega a 0 recibirá el amor  de la caricia
                if (timer <= 0f)
                {
                    //se resetea el tiempo para la prox vez
                    timer = tiempoDeCaricia;

                    //Ponemos que la caricia no funciona (aunque se haga) para que deje de sumar puntos y funcione cuando quiera caricia. 

                    clicmascota = false;

                    //se esteblece esta coroutine para que se muestren corazones
                    StartCoroutine(MostrarCorazones());

                    //Se guarda en el datemanager que ha recibido la caricia para que se guarda tambien la prox hora a la que quiera o pierda puntos por no tenerla.
                    DateManager.Singleton.HaSidoAcariciada();

                    //si la hora a la que estaba permitida que recibiese no es la primera que recibe.
                    if (siguienteHoraCaricia != "")
                    {
                        //Comprobamos segun  lo establecido en el datemanager si la hora actual es la que puede recibir caricia.
                        if (DateTime.Now > DateTime.Parse(siguienteHoraCaricia))
                        {
                            //si es la hora se le dará amor
                            scriptMascota.CambiarAmor(10);
                            //y se  guarda cual esla prox hora que recibirá amor por la caricia
                            siguienteHoraCaricia = DateTime.Now.AddMinutes(minsParaSiguienteCaricia).ToString();
                            PlayerPrefs.SetString("horaSiguienteCaricia", siguienteHoraCaricia);
                        }
                    }
                    // si no habia ninguna hora a la que recibira  amor guardada
                    else
                    {
                        // se le dará amor
                        scriptMascota.CambiarAmor(10);

                        // y se guarda cuando será la próz hora que podrá recibir caricia
                        siguienteHoraCaricia = DateTime.Now.AddMinutes(minsParaSiguienteCaricia).ToString();
                        PlayerPrefs.SetString("horaSiguienteCaricia", siguienteHoraCaricia);
                    }
                }

            }
        }
    }

    //Ponemos un temporizador  incorporado para hacer que aparezcan y desaparezcan las partículas.
    IEnumerator MostrarCorazones()
    {
        //si la mascota no esta muerta podrá soltar particulas.
        if (scriptMascota.muerta == false)
        {
            particulas.SetActive(true);
        }
        // se espera un segundo 
        yield return new WaitForSeconds(1f);
        //y se desactivan
        particulas.SetActive(false);

    }

}