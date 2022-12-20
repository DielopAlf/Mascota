
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class MovMascota : MonoBehaviour
{

    [SerializeField] float Velocidad = 7f;

    float xInput;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
          HorizontalInput(-1f);


        }
         else if  (Input.GetKeyDown(KeyCode.RightArrow)) 
        {

          HorizontalInput(1f);

        }
       else
       {
        HorizontalInput(0f);


       }
       Debug.Log(xInput);
    }

    public void FixedUpdate()
    {

        movimiento();


    }
    public void HorizontalInput(float value)
    {
        xInput = value;
    }
    public void movimiento()
    {
     
      //rb.velocity = new Vector3(xInput, 0f, 0f)* Velocidad * Time.deltaTime; 
      rb.MovePosition(transform.position + new Vector3(xInput, 0f, 0f)* Velocidad * Time.deltaTime); 

    }

}
