using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_Posicio : Animacio
{
    [SerializeField] string nom = "Posicio";
    public Animacio_Posicio() { }
    public Animacio_Posicio(Vector3 inici, Vector3 final, bool local = true, bool dinamic = false)
    {
        corba = Corba.Linear();
        this.inici = inici;
        this.final = final;
        this.local = local;
        this.dinamic = dinamic;
    }

    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] Vector3 inici;
    [SerializeField] Vector3 final;
    [Space(10)]
    [SerializeField] bool local;
    [SerializeField] bool dinamic;


    public override void Transformar(object objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(object objectiu, float frame)
    {
        Vector3 inici = Vector3.zero;

        if (!local)
            inici = ((Transform)objectiu).position;
        else inici = ((Transform)objectiu).localPosition;

        Accio(inici, objectiu, frame);
    }
    void Accio(Vector3 inici, object objectiu, float frame)
    {
        if (!local)
            ((Transform)objectiu).position = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
        else ((Transform)objectiu).localPosition = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}

