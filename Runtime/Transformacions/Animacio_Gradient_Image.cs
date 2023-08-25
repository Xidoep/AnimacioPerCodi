using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Gradient_Image : Animacio
{
    public Animacio_Gradient_Image() { }
    public Animacio_Gradient_Image(Gradient gradient)
    {
        this.gradient = gradient;
    }

    [Title("Gradient Image", horizontalLine: false)]
    [SerializeField, HideLabel] Gradient gradient;

    public override void Transformar(Component objectiu, float frame)
    {
        ((Image)objectiu).color = gradient.Evaluate(frame);
    }
}
