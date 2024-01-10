using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_ProximaAnimacio : Animacio
{
    [SerializeField] string nom = "Proxima Animacio";
    // Start is called before the first frame update
    public Animacio_ProximaAnimacio() { }
    public Animacio_ProximaAnimacio(AnimacioPerCodi animacio)
    {
        this.animacio = animacio;
    }

    [SerializeField, SerializeScriptableObject] AnimacioPerCodi animacio;

    public override void Transformar(Component objectiu, float frame)
    {
        if (frame != 1)
            return;

        animacio.Play(objectiu);
    }
}
