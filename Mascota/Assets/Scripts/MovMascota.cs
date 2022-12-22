
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class MovMascota : MonoBehaviour
{

    [SerializeField] float Velocidad = 7f;
    public GameObject menuAlimentar; 
    float xInput;

    Rigidbody rb;

    public float maxDerecha = 3f;

    public float maxIzquierda = -3f;

    bool botonpulsar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (botonpulsar == false)
        {       
           xInput = Input.GetAxis("Horizontal");  
        }

    }

    public void FixedUpdate()
    {
        if (menuAlimentar.activeSelf == true)
        {

                movimiento();

        }
    }
    
    public void movimiento()
    {
     
      if (xInput<0 && transform.position.x > maxIzquierda)
      {

         rb.MovePosition(transform.position + new Vector3(xInput,0,0f)*Velocidad* Time.deltaTime);
      }
      else if  (xInput>0 && transform.position.x < maxDerecha)
      {
         rb.MovePosition(transform.position + new Vector3(xInput,0,0f)*Velocidad* Time.deltaTime);
      }

    }
    public void botonpulsado(float Input)
    {
     botonpulsar = true;
     xInput = Input;
    }
     public void botonsoltado ()
    {
     botonpulsar =false;
     xInput = 0;
    }
}
