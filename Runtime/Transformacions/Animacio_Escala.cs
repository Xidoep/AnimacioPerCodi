using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_Escala : Animacio
{
    [SerializeField] string nom = "Escala";
    public Animacio_Escala() { }
    public Animacio_Escala(Vector3 inici, Vector3 final, bool dinamic = false)
    {
        corba = Corba.Linear();
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }
    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] Vector3 inici;
    [SerializeField] Vector3 final;
    [Space(10)]
    [SerializeField] bool dinamic;

    //INTERN
    [SerializeField] Vector3 iniciDin;

    public override void Transformar(Component objectiu, float frame)
    { 
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0)
        {
            iniciDin = objectiu.transform.localScale;
        }

        Accio(iniciDin, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        objectiu.transform.localScale = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
