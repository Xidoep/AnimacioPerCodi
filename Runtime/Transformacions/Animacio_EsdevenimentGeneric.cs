using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_EsdevenimentGeneric : Animacio
{
    [SerializeField] string nom = "Esdeveniment Generic";
    public Animacio_EsdevenimentGeneric() { }
    public Animacio_EsdevenimentGeneric(Esdeveniment esdeveniment)
    {
        this.esdeveniment = esdeveniment;
    }

    public enum Esdeveniment { deshabilitar, destruir}

    [SerializeField] Esdeveniment esdeveniment;


    public override void Transformar(object objectiu, float frame)
    {
        if (frame != 1)
            return;

        switch (esdeveniment)
        {
            case Esdeveniment.deshabilitar:
                ((Component)objectiu).gameObject.SetActive(false);
                break;
            case Esdeveniment.destruir:
                GameObject.Destroy(((Component)objectiu).gameObject);
                break;
        }
    }
}
