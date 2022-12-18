using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeys : MonoBehaviour
{
    MovMascota movMascota;

   

    

    private void Awake()
    {

        movMascota = FindObjectOfType<MovMascota>();
    }
    

    public void OnLeftDown()
    {
        movMascota.HorizontalInput(-1f);
    }

    public void OnLeftUp()
    {
        movMascota.HorizontalInput(0f);
    }
    public void OnRightDown()
    {
        movMascota.HorizontalInput(1f);
    }
    public void OnRightUp()
    {
        movMascota.HorizontalInput(0f);
    }




}
