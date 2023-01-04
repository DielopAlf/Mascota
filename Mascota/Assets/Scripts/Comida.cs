using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{

    public float drag = 1;
    public int amorPorComida = 3;

    public Spawner spawner;


    //Nada mas ser spawneada la comida, accedemos a su componente drag del rigidbody y le damos el valor que queramos (esto es lo que controlara la velocidad a la que caera, a mayor mas lento)
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().drag=drag;
    }


    //Cada frame comprobamos si esta ya muy por debajo del suelo
    public void update()
    {
        //Si esta por debajo del suelo a cierta distancia, ya el personaje no puede cogerla asi que la destruimos, es un trozo que no ha podido coger
        if (transform.position.y < -20f)
        {
            Destroy(this);
        }

    }


    //Detectamos contra que colisiona la comida, para si es la mascota, darle de comer  o quitarle amor (comida obstaculo)
    public void OnTriggerEnter(Collider other)
    {
        //Si el objeto contra el que choca tiene el tag de mascota
        if (other.gameObject.tag == "Mascota")
        {
            //Accedemos al codigo de la mascota y llamamos a su funcion de darle de comer
            //(si es un trozo de comida la variable amorPorComida sera positiva, asi que recibira amor, si es negativa sera un obstaculo y le quitara amor)
            other.gameObject.GetComponent<Mascota>().DarDeComer(amorPorComida);

            //Despues de darle o quitarle amor, destruimos el objeto apra que desaparezca y parezca que se lo ha comido
            Destroy(this.gameObject);

        }
    }
}