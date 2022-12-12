using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controllador : MonoBehaviour
{
   public static Controllador SingleTon;


   public float DivisorDeTiempo=1;

   public int EstadoDeHambre=10;
    public int EstadoDeAmor=5;


   public int Amor= 100;
   public int Hambre= 100;

    public int TiempoNesesarioParaSubir = 25;

   public Mascota MascotaActual;

   public Mascota [] EstadosDeMascota;


    int TiempoTranscurrido;
    int EstadoActual= 0;


    public Slider SLDHambre;
    public Slider SLDAmor;

    public Button BTNAlimentar;


    void Update()
    {
        RaycastHit h;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(r, out h))
            {
                if (h.collider.CompareTag("Jugador"))
                {
                    Acariciar();
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
    }

    void Awake()
    {
        if (SingleTon == null) 
        {
           SingleTon = this;
        }
        else
        {
           Destroy(this);
        }
    }
    void Start()
    {
        BTNAlimentar.onClick.AddListener(Alimentar);
        SetUp();
      MascotaActual = Instantiate(EstadosDeMascota[1], Vector3.zero,Quaternion.identity)as Mascota;
        InvokeRepeating("ActualizarEstados",0, DivisorDeTiempo);    
    }

    void Alimentar()
    {
        if (EstadoActual > 0)
        {

            if (Hambre < 100)
            {
                if (Amor > 25)
                {
                    Hambre += 20;
                }
            }
            else
            {
                Hambre = 100;
            }
            SLDHambre.value = Hambre;
        }
    }
    void SetUp()
    {
        SLDHambre.value = Hambre;
        SLDAmor.value = Amor;


    }

    int TiempoHambre;
    int TiempoAmor;
    void ActualizarCosas()
    {
        if (EstadoActual > 0)
        {
            if (TiempoAmor > EstadoDeAmor)
            {
                TiempoAmor = 0;
                Amor--;
                SLDAmor.value = Amor;
            }
            TiempoAmor++;

            if (TiempoHambre > EstadoDeHambre)
            {
                TiempoHambre = 0;
                Hambre--;

                if (Hambre <= 40)
                {
                    Amor--;
                    SLDAmor.value = Amor;
                    if (Amor <= 0)
                    {
                        Morir();
                    }
                }
                else
                {
                    if (Amor < 100)
                    {
                        Amor++;
                        SLDAmor.value = Amor;
                    }
                }

                SLDHambre.value = Hambre;
            }
            TiempoHambre++;
        }
        if (EstadoActual > -1)
        {
            if ((TiempoTranscurrido % TiempoNesesarioParaSubir) == 0)
            {
                CalcularCambioDeEstado();
            }
        }


        TiempoTranscurrido++;
    }
    void Morir()
    {
        Destroy(MascotaActual.gameObject);
        MascotaActual = Instantiate(EstadosDeMascota[0], Vector3.zero, Quaternion.identity) as Mascota;
        EstadoActual = -1;
    }



    void CalcularCambioDeEstado()
    {
        int s = TiempoTranscurrido / TiempoNesesarioParaSubir;
        if (s == EstadoActual || EstadoActual >= EstadosDeMascota.Length - 2)
        {

            return;

        }

        Destroy(MascotaActual.gameObject);
        MascotaActual = Instantiate(EstadosDeMascota[s+1],Vector3.zero, Quaternion.identity) as Mascota;
        EstadoActual = s;

    }
}
