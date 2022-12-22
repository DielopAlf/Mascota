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
    public float tiempoDeCaricia = 2f;
    float timer =2f;
    public int amorporCaricia = 10;
    
    void Start()
    {
        timer = tiempoDeCaricia;
        scriptMascota = mascota.GetComponent<Mascota>();
    }


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
        if (Input.GetMouseButtonUp(0))
        {
          clicmascota=false;
        }




        if (clicmascota==true)
        {
           if (Input.GetAxis("Mouse X")!=0f||Input.GetAxis("Mouse Y")!=0f)
           {
                   
                timer -= Time.deltaTime;
                if(timer <= 0f)
                {
                    scriptMascota.CambiarAmor(10);
                    timer = tiempoDeCaricia;
                    clicmascota = false;
                    StartCoroutine(MostrarCorazones());
                }

           }
        }

       
    }


    IEnumerator MostrarCorazones()
    {
        particulas.SetActive(true);

        yield return new WaitForSeconds(1f);

        particulas.SetActive(false);

    }
 
}