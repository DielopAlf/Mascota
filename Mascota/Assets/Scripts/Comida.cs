using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{

   public float drag = 1;
 


    public enum state 
    {
    Comida,
    obstaculos,
    
    }
    public state quesoy;



    void Start()
    
    {
    this.gameObject.GetComponent<Rigidbody>().drag=drag;
    }
    public void update()
    {
        if (transform.position.y < -20f)
        {

        Destroy(this);

        }

    }
    

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mascota")
        {
            other.gameObject.GetComponent<Mascota>().CambiarAmor(1);
            Destroy(this);
        }

        
    }
}
