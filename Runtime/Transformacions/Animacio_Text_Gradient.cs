using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Text_Gradient : Animacio
{
    public Animacio_Text_Gradient() { }
    public Animacio_Text_Gradient(Gradient gradient)
    {
        this.gradient = gradient;
    }

    [Title("Gradient Text", horizontalLine: false)]
    [SerializeField, HideLabel] Gradient gradient;

    public override void Transformar(Component objectiu, float frame)
    {
        ((TMP_Text)objectiu).color = gradient.Evaluate(frame);
    }

}
