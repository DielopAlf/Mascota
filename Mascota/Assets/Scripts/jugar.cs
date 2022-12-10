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
   

    [SerializeField]
    public Text puntuacion;
    public Image final;
    public Text Victoria;
    public Button text;


     float IMPULSE = 2F;

    public float movimientoEjeX;
    public float movimientoEjeY;


    public float velocidaddeMovimiento = 1.5f;


    
    public void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); 
        contador = contador +3; 
         puntuacion.text="puntuacion"+contador; 
     



       if (contador>= 12) 
       {
            Victoria.gameObject.SetActive(true);
          

        }

    }

    private void  puntuacion.text="puntuacion"+contador(); 
    {
        puntuacion.text="Amor:"+contador; 
    }
    
   public void Awake()
    {
        rb=GetComponent<Rigidbody>();
        contador= 0;
          puntuacion.text="puntuacion"+contador; 
        Victoria.gameObject.SetActive(false); 
       
    }


   
 void FixedUpdate() 
   
   {
    movimientoEjeX = Input.GetAxis("Horizontal") * Time.deltaTime* IMPULSE * velocidaddeMovimiento;
    transform.Translate(movimientoEjeX, movimientoEjeY);
   }
 
}



