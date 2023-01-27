using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_ProximaAnimacio : Animacio
{
    [SerializeField] string nom = "Proxima Animacio";
    // Start is called before the first frame update
    public Animacio_ProximaAnimacio() { }
    public Animacio_ProximaAnimacio(Animacio_Scriptable animacio)
    {
        this.animacio = animacio;
    }

    [SerializeField] Animacio_Scriptable animacio;

    public override void Transformar(Component objectiu, float frame)
    {
        if (frame != 1)
            return;

        animacio.Play(objectiu);
    }
}
