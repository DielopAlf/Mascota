using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{

    public float drag = 1;
    public int amorPorComida = 3;

    public Spawner spawner;

    //ponemos esta funcion para poder dar el valor del drag del rigidbody la comida(velocidad a la que cae)
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().drag=drag;
    }

    //se comprueba en todo momento que esta por debajo del suelo
    public void update()
    {
        //esto es para que si llega a cierta distancia por debajo del suelo se destruirá
        if (transform.position.y < -20f)
        {
            Destroy(this);
        }

    }

    //Detectamos contra que colisiona la comida , para ver si da contra la mascota (asi se le puede dar de comer)
    public void OnTriggerEnter(Collider other)
    {   
        // si choca contra con objeto que tiene el tag de mascota 
        if (other.gameObject.tag == "Mascota")
        {
            //Ponemos la funcion dardecomer del scriptmascota para ejecutar esta acción. 
            //si es un trozo de comida la variable amorPorComida sera positiva, asi que recibira amor, si es negativa sera un obstaculo y le quitara amor(dependiendo de los valores puestos)      
            other.gameObject.GetComponent<Mascota>().DarDeComer(amorPorComida);

            // al chocar contra la mascota el objeto se destruirá
            Destroy(this.gameObject);

        }
    }
}