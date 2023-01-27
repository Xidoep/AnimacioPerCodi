using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Animacio_Gradient_Image : Animacio
{
    [SerializeField] string nom = "Gradient imatge";
    public Animacio_Gradient_Image() { }
    public Animacio_Gradient_Image(Gradient gradient)
    {
        this.gradient = gradient;
    }

    [SerializeField] Gradient gradient;

    public override void Transformar(Component objectiu, float frame)
    {
        ((Image)objectiu).color = gradient.Evaluate(frame);
    }
}
