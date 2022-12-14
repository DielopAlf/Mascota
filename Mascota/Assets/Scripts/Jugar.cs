using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugar : MonoBehaviour
{
    bool Derecha = false;
    bool Izquierda = false;

    //public float velocidaddeMovimiento = 1.5f;
    //public float movimientoEjeX;
    //public float movimientoEjeY;
    //public float movimientoEjeZ;
    public Rigidbody rb;
    public float speedForce;

    public void clickIzq()
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
    






}


