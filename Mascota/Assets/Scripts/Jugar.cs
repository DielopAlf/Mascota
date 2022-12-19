using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugar : MonoBehaviour
{
    int contador;
    Rigidbody rb;
    Vector2 direction;

    public float movimientoEjeX;
    public float movimientoEjeY;
    public float movimientoEjeZ;
    public float velocidaddeMovimiento = 1.5f;

    public void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        contador = contador +1;





    }



    public void Awake()
    {
        rb=GetComponent<Rigidbody>();
        contador = 0;




    }



    public void FixedUpdate()


    {
        movimientoEjeX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidaddeMovimiento;

        transform.Translate(movimientoEjeX, movimientoEjeY, movimientoEjeZ);
    }
}
    

