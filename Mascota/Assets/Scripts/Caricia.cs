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
        //Establecemos el timer que calculara cuanto debe durar la caricia para que reciba amor (que es igual a lo que nosotros digamos en tiempoDeCaricia)
        timer = tiempoDeCaricia;

        //Guardamos el script de la mascota para poder acceder a el desde este script y poder llamar a funciones de el
        scriptMascota = mascota.GetComponent<Mascota>();


        //Si no habia guardada ninguna hora de poderRecibir otra caricia, entonces decimos que la hora para poder recibir la siguiente caricia esta vacia (aun no ha recibido ninguna asi que hasta que no lo haga no sabremos cual sera esta hora)
        if (PlayerPrefs.GetString("horaSiguienteCaricia") == "")
        {
            siguienteHoraCaricia = "";
        }
        //En cambio, si ya habia una hora a la que podia recibir la siguiente caricia (es decir, ya habia sido acariciada antes), la guardamos en una variable
        else
        {
            siguienteHoraCaricia = PlayerPrefs.GetString("horaSiguienteCaricia");
        }
    }


    //Cada frame comprobaremos si clickamos encima de la mascota y movemos el ratos (la acariciamos vamos)
    void Update()
    {
        RaycastHit h;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Si se hace click con el raton
        if (Input.GetMouseButtonDown(0))
        {
            //Si este click esta encima de un objeto que tenga el tag de "mascota" (que sera obviamente nuestra mascota)
            if (Physics.Raycast(r, out h))
            {
                if (h.collider.tag.Equals("Mascota"))
                {
                    Rigidbody rigidbodyMascota = h.collider.GetComponent<Rigidbody>();
                    //Entonces guardamos en una booleana que hemos clickado sobre la mascota 
                    clicmascota=true;
                }
            }
        }
        //Si soltamos el click
        else if (Input.GetMouseButtonUp(0))
        {
            //Guardamos que ya no estamos clickando sobre la mascota
            clicmascota=false;
        }



        //Si estamos clickando sobre la mascota
        if (clicmascota==true)
        {
            //Si ademas movemos el raton (sea hacia arriba/abajo o derecha/izquiera)
            if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
            {
                //Comenzamos a descontar tiempo del necesario para recibir la caricia, siempre que el raton se mueva
                timer -= Time.deltaTime;

                //Si este tiempo llega a 0, significa que ya debe recibir el amor por la caricia
                if (timer <= 0f)
                {
                    //Reseteamos el tiempo para la proxima caricia
                    timer = tiempoDeCaricia;
                    //Decimos como que ya no estamos clickando sobre la mascota (aunque lo estuvieramos haciendo), para obligar al usuario a soltar el click y volver a darle si quiere volver a acariciar
                    clicmascota = false;
                    //Ejecutamos una pequeña funcion para que se vean corazones
                    StartCoroutine(MostrarCorazones());

                    //En nuestro controlador de horas, indicamos que la mascota ha sido acariciada para que se guarde la proxima hora a la que querra caricias o pierde puntos por no tenerlas, etc.
                    DateManager.Singleton.HaSidoAcariciada();


                    //Si la hora a la que estaba permitida recibir amor por caricia no vacia (es decir, no es la primera caricia que recibe)
                    if (siguienteHoraCaricia != "")
                    {
                        //Comprobamos si la hora actual es despues de la hora a la que podia recibir amor por caricia
                        if (DateTime.Now > DateTime.Parse(siguienteHoraCaricia))
                        {
                            //Si lo es, le damos amor a la mascota
                            scriptMascota.CambiarAmor(10);

                            //Y guardamos cuando es la proxima hora a la que podra recibir amor por caricia (hora actual mas lo que decidamos)
                            siguienteHoraCaricia = DateTime.Now.AddMinutes(minsParaSiguienteCaricia).ToString();
                            PlayerPrefs.SetString("horaSiguienteCaricia", siguienteHoraCaricia);
                        }
                    }
                    //Si en cambio, no habia guardada ninguna hora a la que podia recibir amor por caricia (es decir, es la primera que recibe)
                    else
                    {
                        //Le damos amor a la mascota
                        scriptMascota.CambiarAmor(10);

                        //Y guardamos cuando es la proxima hora a la que podra recibir amor por caricia (hora actual mas lo que decidamos)
                        siguienteHoraCaricia = DateTime.Now.AddMinutes(minsParaSiguienteCaricia).ToString();
                        PlayerPrefs.SetString("horaSiguienteCaricia", siguienteHoraCaricia);
                    }
                }

            }
        }
    }


    //Funcion con temporizador incorporado para hacer aparecer y desaparecer las particulas
    IEnumerator MostrarCorazones()
    {
        //Nada mas se llame la funcion, se activan las particulas
        particulas.SetActive(true);

        //Espera 1 segundo
        yield return new WaitForSeconds(1f);

        //Vuelve a desactivar las particulas
        particulas.SetActive(false);

    }

}