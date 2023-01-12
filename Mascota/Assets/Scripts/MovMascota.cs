
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

    //Accedemos al componente Rigidbody y al script mascota para poder ejectuar sus funciones.

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mascota = GetComponent<Mascota>();
    }

    //En cada momento se comprueba que no se esta pulsando el boton 
    private void Update()
    {
        if (botonpulsar == false)
        {
            //Si no estamos pulsando el boton haremos que se pueda mover con las flechas del teclado guardando sus inputs.
            xInput = Input.GetAxis("Horizontal");
        }

    }
    //En cada momento se comprueba si la mascota se mueve. 

    public void FixedUpdate()
    {
        //Se comprueba que la mascota esta viva y la interfaz del menu de alimnetar(el minijuego) esta activo
        if (Interfaz.Singleton.MenuAlimentar.activeSelf == true && mascota.muerta != true)
        {
            //Entonces hacemos que se mueva
            movimiento();

        }
    }

    //Hacemos la funcion del movimiento de la mascota

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

    //Cuando suelte alguno de los botones , el xInput vuelve a ser 0 y la booleana botonpulsada se pone falsa (no se moverá) para que luego se detecte si se vuelven a tocar las flechas

    public void botonsoltado()
    {
        botonpulsar =false;
        xInput = 0;
    }
}
