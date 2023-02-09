using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Animacio_Text_Gradient : Animacio
{
    [SerializeField] string nom = "Text color";
    public Animacio_Text_Gradient() { }
    public Animacio_Text_Gradient(Gradient gradient)
    {
        this.gradient = gradient;
    }

    [SerializeField] Gradient gradient;

    public override void Transformar(Component objectiu, float frame)
    {
        ((TMP_Text)objectiu).color = gradient.Evaluate(frame);
    }
}
