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
    public Slider BarraDeVida;
    public TextMeshProUGUI tiempo;
    public GameObject malla;
    public GameObject particulas;
    bool clicmascota;
    public Slider SLDAmor;

    public int Amor;
    int EstadoActual = 0;



    void Start()
    {
        
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
                    
                    particulas.SetActive(true);
                    clicmascota=true;
                    

                    
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
          clicmascota=false;
          StopCoroutine(Acariciar());
        }

        if (clicmascota==true)
        {
           if (Input.GetAxis("Mouse X")!=0f||Input.GetAxis("Mouse Y")!=0f) 
           {
                    
            StartCoroutine(Acariciar());


           }
        }

        
    }

    IEnumerator Acariciar()
    {   
       
        if (scriptMascota.EstadoActual > 0)
        {
            if (scriptMascota.Amor < 100)
            {
                 yield return new WaitForSeconds(2f);
                scriptMascota.CambiarAmor(10);
            }
            SLDAmor.value = Amor;
        }

    }
}