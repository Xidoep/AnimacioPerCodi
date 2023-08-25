using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Shader_Image_TempsActual : Animacio
{
    public Animacio_Shader_Image_TempsActual() { }
    public Animacio_Shader_Image_TempsActual(string propietat)
    {
        this.propietat = propietat;
    }

    [Title("Image.shader => Time.time", horizontalLine: false)]
    [SerializeField] string propietat;

    public override void Transformar(Component objectiu, float frame)
    {
        if (frame == 0) ((Image)objectiu).material.SetFloat(propietat, Time.time);
    }
}
