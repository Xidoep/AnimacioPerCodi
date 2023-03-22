using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_RectPosicio : Animacio
{
    [SerializeField] string nom = "Rect posicio";
    public Animacio_RectPosicio() { }
    public Animacio_RectPosicio(Vector2 inici, Vector2 final, AnimationCurve corba = null, bool dinamic = false)
    {
        this.corba = corba != null ? Corba.Linear : corba;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }
    public void SetInici(Vector2 inici)
    {
        this.inici = inici;
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
        if (frame == 0) inicidin = ((RectTransform)objectiu.transform).anchoredPosition;

        Accio(inicidin, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        ((RectTransform)objectiu.transform).anchoredPosition = Vector2.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
