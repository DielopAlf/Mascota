using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Caricia: MonoBehaviour
{
   public GameObject mascota;

    Mascota scriptMascota;
    public GameObject particulas;
    bool clicmascota;
    public float TiempodeCaricia = 2f;
    float temporizador;
    public int amorporCaricia = 10;
    void Start()
    {
        temporizador = TiempodeCaricia;
         scriptMascota = mascota.GetComponent<Mascota>();
    }

    // Update is called once per frame

    void Update()
    {
        RaycastHit h;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(r, out h))
            {
                if (h.collider.tag.Equals("Mascota"))
                {
                    Rigidbody rigidbodyMascota = h.collider.GetComponent<Rigidbody>();
                    
                    clicmascota=true;
                   
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
          clicmascota=false;
        }

        
        
        if (clicmascota==true)
        {
           if (Input.GetAxis("Mouse X")!=0f||Input.GetAxis("Mouse Y")!=0f) 
           {
               temporizador -= Time.deltaTime;    
       
               if (temporizador<= 0f)
               {
                scriptMascota.CambiarAmor(amorporCaricia); 
                
                temporizador = TiempodeCaricia;
                clicmascota=false;
                StartCoroutine(mostrarcorazones());
               }
           }
        }

        
    }
    IEnumerator mostrarcorazones()
    {
        particulas.SetActive(true);
        yield return new WaitForSeconds(1);
        particulas.SetActive(false);
        
    }
 
}