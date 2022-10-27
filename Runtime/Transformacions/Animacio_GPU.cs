using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[System.Serializable]
public class Animacio_GPU : Animacio
{
    [SerializeField] string nom = "GPU";
    public Animacio_GPU() { }
    public Animacio_GPU(Accio accio)
    {
        this.accio = accio;
    }

    public enum Accio { add, remove}

    [SerializeField] Accio accio;

    public override void Transformar(object objectiu, float frame)
    {
        switch (accio)
        {
            case Accio.add:
                if (frame == 1)
                {
                    ((Transform)objectiu).gameObject.AddGrafics();
                }
                break;
            case Accio.remove:
                if (frame == 0)
                {
                    ((Transform)objectiu).gameObject.RemoveGrafic();
                }
                break;
        }
    }
}
