using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Caricia: MonoBehaviour
{

    public Slider BarraDeVida;
    public TextMeshProUGUI tiempo;
    public GameObject malla;
    public GameObject particulas;
    bool estaActiva;

    public Slider SLDAmor;

    public int Amor;
    int EstadoActual = 0;



    void Start()
    {
        estaActiva=true;
    }

    // Update is called once per frame

    void Update()
    {
        RaycastHit h;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(r, out h))
            {
                if (h.collider.tag.Equals("Mascota"))
                {
                    Rigidbody rigidbodyMascota = h.collider.GetComponent<Rigidbody>();
                    Acariciar();
                    particulas.SetActive(true);
                    Amor += 10;

                    
                }
            }
        }
    }
    void Acariciar()
    {
       
        if (EstadoActual > 0)
        {
            if (Amor < 100)
            {
                Amor++;
            }
            SLDAmor.value = Amor;
        }

        Debug.Log("golpea");
    }
}