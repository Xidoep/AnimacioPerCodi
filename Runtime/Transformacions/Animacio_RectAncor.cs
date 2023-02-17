using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_RectAncor : Animacio
{
    [SerializeField] string nom = "Rect ancor";
    public Animacio_RectAncor() { }
    public Animacio_RectAncor(Vector3 iniciMin, Vector3 iniciMax, Vector3 finalMin, Vector3 finalMax, bool dinamic)
    {
        min = Corba.Linear();
        max = Corba.Linear();
        this.iniciMin = iniciMin;
        this.iniciMax = iniciMax;

        this.finalMin = finalMin;
        this.finalMax = finalMax;

        this.dinamic = dinamic;
    }

    [SerializeField] protected AnimationCurve min = new AnimationCurve();
    [SerializeField] protected AnimationCurve max = new AnimationCurve();
    [Space(10)]
    [SerializeField] Vector2 iniciMin;
    [SerializeField] Vector2 iniciMax;
    [Space(10)]
    [SerializeField] Vector2 finalMin;
    [SerializeField] Vector2 finalMax;
    [Space(10)]
    [SerializeField] bool dinamic;

    //INTERN
    Vector2 iniciMinDin;
    Vector2 iniciMaxDin;
    RectTransform rectTransform;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(iniciMin, iniciMax, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0) 
        {
            iniciMinDin = ((RectTransform)objectiu.transform).anchorMin;
            iniciMaxDin = ((RectTransform)objectiu.transform).anchorMax;
        }

        Accio(iniciMinDin, iniciMaxDin, objectiu, frame);
    }

    void Accio(Vector2 iniciMin, Vector2 iniciMax, Component objectiu, float frame)
    {
        ((RectTransform)objectiu.transform).anchorMin = Vector2.LerpUnclamped(iniciMin, finalMin, min.Evaluate(frame));
        ((RectTransform)objectiu.transform).anchorMax = Vector2.LerpUnclamped(iniciMax, finalMax, max.Evaluate(frame));
    }
}
