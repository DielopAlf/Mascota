
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class MovMascota : MonoBehaviour
{

    [SerializeField] float Velocidad = 7f;
    float xInput;
    Mascota mascota;

    Rigidbody rb;

    public float maxDerecha = 2.5f;

    public float maxIzquierda = -2.5f;

    bool botonpulsar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mascota = GetComponent<Mascota>();
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
        if (Interfaz.Singleton.MenuAlimentar.activeSelf == true && mascota.muerta != true)
        {
            movimiento();

        }
    }

   
    public void movimiento()
    {
       
        if (xInput<0 && transform.position.x > maxIzquierda)
        {
            rb.MovePosition(transform.position + new Vector3(xInput, 0, 0f)*Velocidad* Time.deltaTime);
        }
        else if (xInput>0 && transform.position.x < maxDerecha)
        {
            rb.MovePosition(transform.position + new Vector3(xInput, 0, 0f)*Velocidad* Time.deltaTime);
        }

    }

   
    public void botonpulsado(float Input)
    {
        botonpulsar = true;
        xInput = Input;
    }

   
    public void botonsoltado()
    {
        botonpulsar =false;
        xInput = 0;
    }
}
