
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class MovMascota : MonoBehaviour
{

    [SerializeField] float Velocidad = 7f;
    float xInput;
    Mascota mascota;

    Rigidbody rb;

    public float maxDerecha = 3f;

    public float maxIzquierda = -3f;

    bool botonpulsar;

    //Accedemos al componente Rigidbody y al script mascota para poder ejectuar sus funciones
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mascota = GetComponent<Mascota>();
    }

    //Cada frame, comprobamos si no estamos pulsando el boton del canvas de mover al personaje
    private void Update()
    {
        if (botonpulsar == false)
        {
            //Si no estamos pulsando este boton, guardamos el input que recibamos con las felchitas del teclado por si el usuario quiere mover a la mascota con estas
            xInput = Input.GetAxis("Horizontal");
        }

    }

    //En cada frame, comprobamos si movemos a la mascota
    public void FixedUpdate()
    {
        //Si en la interfaz, el menu de alimentar esta activado (es decir, estamos en el minijuego de alimentar) y la mascota no esta muerta
        if (Interfaz.Singleton.MenuAlimentar.activeSelf == true && mascota.muerta != true)
        {
            //Entonces ejecutamos su movimiento
            movimiento();

        }
    }

    //Movimiento de la mascota
    public void movimiento()
    {
        //Si la variable donde almacenamos el input (que puede ser 1 o -1, por las flechas del teclado o porque clicke sobre el boton de flecihtas del canvas)
        //es menor a 0 (hacia la izquierda) y ademas, la posicion en x de nuestra mascota  es mayor a la maxima posicion de x de la que no puede pasar (por la izquierda)
        if (xInput<0 && transform.position.x > maxIzquierda)
        {
            //Entonces al rigidboigy de la mascota le damos movimiento, que es su posicion actual + moverse en x a la izquuierda tanto como le hayamos puesto en velocidad cada segundo
            rb.MovePosition(transform.position + new Vector3(xInput, 0, 0f)*Velocidad* Time.deltaTime);
        }
        //Si la variable donde almacenamos el input es mayor que 0 (hacia la derecha) y la posicion en x de nuestra mascota es menor a la maxima posicion en x de la que no puede pasar (por la derecha)
        else if (xInput>0 && transform.position.x < maxDerecha)
        {
            //Entonces al rigidboigy de la mascota le damos movimiento, que es su posicion actual + moverse en x a la derecha tanto como le hayamos puesto en velocidad cada segundo
            rb.MovePosition(transform.position + new Vector3(xInput, 0, 0f)*Velocidad* Time.deltaTime);
        }

    }

    //Si le damos al boton del canvas para moverla, le decimos -1 o  1 (en float input) para saber si es izquierda o derecha, y guardamos ese valor en xInput
    //Tambien marcamos la booleana botonpulsar como true para que no se detecta las felchas del teclado mientras pulsa estos botones
    public void botonpulsado(float Input)
    {
        botonpulsar = true;
        xInput = Input;
    }

    //Cuando suelte alguno de los botones del canvas, el xInput vuelve a ser 0 y la booleana botonpulsada se pone falsa, para que se pueda volver a detectar si toca las felchas del teclado para mover a la mascota
    public void botonsoltado()
    {
        botonpulsar =false;
        xInput = 0;
    }
}
