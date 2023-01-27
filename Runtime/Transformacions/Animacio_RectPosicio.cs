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

    //INTERN
    Vector2 inicidin = Vector2.zero;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0) inicidin = ((RectTransform)objectiu).anchoredPosition;

        Accio(inicidin, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        ((RectTransform)objectiu).anchoredPosition = Vector2.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
