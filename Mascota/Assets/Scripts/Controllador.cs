using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllador : MonoBehaviour
{
   public static Controllador SingleTon;
    
   public int Amor;
   public int Hambre;

   public Mascota MascotaActual;
   public Mascota []EstadosDeMascota;

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
      MascotaActual = Instantiate(EstadosDeMascota[0]);
    }

}
