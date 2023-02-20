using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_Rotacio : Animacio
{
    [SerializeField] string nom = "Rotacio";
    public Animacio_Rotacio() { }
    public Animacio_Rotacio(Vector3 inici, Vector3 final, bool local = true, bool dinamic = false)
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

    //INTERN
    //Vector3 iniciDin;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0)
        {
            if (!local)
                inici = objectiu.transform.eulerAngles;
            else inici = objectiu.transform.localEulerAngles;
        }

        Accio(inici, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        if (!local)
            objectiu.transform.rotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame)));
        else objectiu.transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame)));
    }

}
