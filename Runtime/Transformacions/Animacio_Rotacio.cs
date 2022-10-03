using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_Rotacio : Animacio
{
    public Animacio_Rotacio() { }
    public Animacio_Rotacio(Vector3 inici, Vector3 final, bool local = true, bool dinamic = false)
    {
        corba = Corba.Linear();
        this.inici = inici;
        this.final = final;
        this.local = local;
        this.dinamic = dinamic;
    }

    [Header("Rotacio")]
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

        if (frame == 0)
        {
            if (!local)
                inici = ((Transform)objectiu).eulerAngles;
            else inici = ((Transform)objectiu).localEulerAngles;
        }

        Accio(inici, objectiu, frame);
    }

    void Accio(Vector3 inici, object objectiu, float frame)
    {
        if (!local)
            ((Transform)objectiu).rotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame)));
        else ((Transform)objectiu).localRotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame)));
    }
}
