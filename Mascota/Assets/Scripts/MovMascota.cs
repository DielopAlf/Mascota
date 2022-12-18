
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class MovMascota : MonoBehaviour
{

    [SerializeField] float Velocidad = 7f;

    float xInput;


    bool facingRight;


    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(xInput * Velocidad, rb.velocity.y, 0f); 
    }

    public void HorizontalInput(float value)
    {
        xInput = value;
    }


}
