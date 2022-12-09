using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugar : MonoBehaviour
{
    int contador;
    Rigidbody rb;
    Vector2 direction;
    float IMPULSE = 2F;

    [SerializeField]
    public Text puntuacion;
    public Image final;
    public Text Victoria;
    public Button text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
