using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Caricia : MonoBehaviour
{
    public GameObject mascota;

    Mascota scriptMascota;
    public GameObject particulas;
    bool clicmascota;
    public float tiempoDeCaricia = 2f;
    float timer = 2f;
    public int amorporCaricia = 10;

    string siguienteHoraCaricia;
    public float minsParaSiguienteCaricia = 20f;



    public void Start() 
    {
        timer = tiempoDeCaricia;

        scriptMascota = mascota.GetComponent<Mascota>();


        if (PlayerPrefs.GetString("horaSiguienteCaricia") == "")
        {
            siguienteHoraCaricia = "";
        }
        else
        {
            siguienteHoraCaricia = PlayerPrefs.GetString("horaSiguienteCaricia");
        }
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
        else if (Input.GetMouseButtonUp(0))
        {
            clicmascota=false;
        }



        if (clicmascota==true)
        {
            if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
            {
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    timer = tiempoDeCaricia;

                    clicmascota = false;

                    StartCoroutine(MostrarCorazones());

                    DateManager.Singleton.HaSidoAcariciada();


                    if (siguienteHoraCaricia != "")
                    {
                        if (DateTime.Now > DateTime.Parse(siguienteHoraCaricia))
                        {
                            scriptMascota.CambiarAmor(10);

                            siguienteHoraCaricia = DateTime.Now.AddMinutes(minsParaSiguienteCaricia).ToString();
                            PlayerPrefs.SetString("horaSiguienteCaricia", siguienteHoraCaricia);
                        }
                    }
                    else
                    {
                        scriptMascota.CambiarAmor(10);

                        siguienteHoraCaricia = DateTime.Now.AddMinutes(minsParaSiguienteCaricia).ToString();
                        PlayerPrefs.SetString("horaSiguienteCaricia", siguienteHoraCaricia);
                    }
                }

            }
        }
    }


    IEnumerator MostrarCorazones()
    {
        if (scriptMascota.muerta == false)
        {
            particulas.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        particulas.SetActive(false);

    }

}