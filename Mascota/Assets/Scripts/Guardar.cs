using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Guardar : MonoBehaviour
{
    [SerializeField]
    Slider mSlider;
    [SerializeField]
    TMP_InputField mInput;

    private void Awake()
    {
        mInput.text = PlayerPrefs.GetString("nombre", "Escribe tu nombre...");
        mSlider.value = PlayerPrefs.GetFloat("volumen", 0.5f);

    }

    public void Borrar()
    {
        PlayerPrefs.DeleteAll();


    }


    public void GuardarTodo()
    {
        PlayerPrefs.SetString("nombre", mInput.text);
        PlayerPrefs.SetFloat("volumen", mSlider.value);
        PlayerPrefs.Save();
        Debug.Log(mSlider.value);
        Debug.Log(mInput.text);

    }





}
