using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Animacio_Shader_Image_TempsActual : Animacio
{
    [SerializeField] string nom = "Image shader Time.time";
    public Animacio_Shader_Image_TempsActual() { }
    public Animacio_Shader_Image_TempsActual(string propietat, float inici, float final, bool dinamic = false)
    {
        this.propietat = propietat;
    }

    [SerializeField] string propietat;

    public override void Transformar(Component objectiu, float frame)
    {
        if (frame == 0) ((Image)objectiu).material.SetFloat(propietat, Time.time);
    }
}
