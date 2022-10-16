using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_RectPosicio : Animacio
{
    [SerializeField] string nom = "Rect posicio";
    public Animacio_RectPosicio() { }
    public Animacio_RectPosicio(Vector3 inici, Vector3 final, bool dinamic = false)
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

    public override void Transformar(object objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(object objectiu, float frame)
    {
        Vector2 inici = Vector2.zero;

        if (frame == 0) inici = ((RectTransform)objectiu).anchoredPosition;

        Accio(inici, objectiu, frame);
    }

    void Accio(Vector3 inici, object objectiu, float frame)
    {
        ((RectTransform)objectiu).anchoredPosition = Vector2.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
