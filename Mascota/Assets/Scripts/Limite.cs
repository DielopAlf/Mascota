using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Limite : MonoBehaviour
{
    bool Derecha = false;
    bool Izquierda = false;
    public Button jugar;
    public float Velocidad= 1.5f;
    public float desplazamientoHorizontal;
    public float rangoX = 1f;
    //public float movimientoEjeX;
    //public float movimientoEjeY;
    //public float movimientoEjeZ;
    public Rigidbody rb;
  
    public int Amor;

    [SerializeField]
    TextMeshProUGUI puntuacion;

   void Start()
    {
        
    }

    /*public void clickIzq()
    {

        Izquierda = true;
    }

    public void releaseIzq()
    {

       Izquierda = false;

    }
    public void clickDch()
    {

        Derecha = true;
    }

    public void releaseDch()
    {

        Derecha = false;

    }

    private void FixedUpdate()
    {
        if(Izquierda)
        {

            rb.AddForce(new Vector3(-speedForce, 0)*Time.deltaTime);
        }
        if(Derecha)
        {

            rb.AddForce(new Vector3(-speedForce, 0)*Time.deltaTime);

        }


    }

    /*private void FixedUpdate()
    {
        movimientoEjeX = Input.GetAxis("Horizontal") * Time.deltaTime*velocidaddeMovimiento;
        //movimientoEjeZ= Input.GetAxis("Vertical")*Time.deltaTime* velocidaddeMovimiento;
        transform.Translate(movimientoEjeX, movimientoEjeY, movimientoEjeZ);
    }*/

    void Update()
    {
        if(transform.position.x < -rangoX)
        {
            transform.position = new Vector3(-rangoX, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rangoX)
        {
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }





        desplazamientoHorizontal =Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right* desplazamientoHorizontal * Time.deltaTime * Velocidad);
    }

}


