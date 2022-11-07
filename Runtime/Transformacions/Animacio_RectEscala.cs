using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_RectEscala : Animacio
{
    [SerializeField] string nom = "Rect escala";
    public Animacio_RectEscala() { }
    public Animacio_RectEscala(Vector3 inici, Vector3 final, bool dinamic = false)
    {
        corba = Corba.Linear();
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] Vector2 inici;
    [SerializeField] Vector2 final;
    [Space(10)]
    [SerializeField] bool dinamic;

    //inici
    Vector2 inicidin = Vector2.zero;

    public override void Transformar(object objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(object objectiu, float frame)
    {
        if (frame == 0) inicidin = ((RectTransform)objectiu).sizeDelta;

        Accio(inicidin, objectiu, frame);
    }

    void Accio(Vector3 inici, object objectiu, float frame)
    {
        ((RectTransform)objectiu).sizeDelta = Vector2.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
