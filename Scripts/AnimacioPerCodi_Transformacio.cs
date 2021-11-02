using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_Transformacio : AnimacioPerCodi_Base
{
    enum Tipus { Moviment, Rotacio, Escalat, RectPosition }

    [SerializeField] T_Transformacio[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;


    new public void Play() => base.Play();

    [ContextMenu("Play")] void PlayProva() => Play();
    [ContextMenu("Stop")] void StopProva() => StopInmediatament();
    [ContextMenu("Stop Quan acabi")] void StopFinalProva() => StopAlFinal();



    [System.Serializable]
    public class T_Transformacio : Transformacions
    {
        [SerializeField] Tipus tipus;
        [SerializeField] AnimationCurve corba = new AnimationCurve();

        [Space(10)]
        [SerializeField] bool iniciDinamic;
        [SerializeField] Vector3 inici;
        [SerializeField] Vector3 final;

        RectTransform rectTransform;

        Vector3 actual;

        public override void Transformar(Transform transform, float temps)
        {
            if (iniciDinamic && corba.Evaluate(temps) == 0) actual = GetIniciDinamic(transform);
            else actual = inici;

            switch (tipus)
            {
                case Tipus.Moviment:
                    transform.localPosition = Vector3.LerpUnclamped(actual, final, corba.Evaluate(temps));
                    break;
                case Tipus.Rotacio:
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(actual, final, corba.Evaluate(temps)));
                    break;
                case Tipus.Escalat:
                    transform.localScale = Vector3.LerpUnclamped(actual, final, corba.Evaluate(temps));
                    break;
                case Tipus.RectPosition:
                    if (rectTransform == null) rectTransform = transform.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = Vector2.LerpUnclamped(actual, final, corba.Evaluate(temps));
                    break;
            }
        }

        Vector3 GetIniciDinamic(Transform transform)
        {
            switch (tipus)
            {
                case Tipus.Moviment:
                    return transform.localPosition;
                case Tipus.Rotacio:
                    return transform.localEulerAngles;
                case Tipus.Escalat:
                    return transform.localScale;
                case Tipus.RectPosition:
                    if (rectTransform == null) rectTransform = transform.GetComponent<RectTransform>();
                    return inici = rectTransform.anchoredPosition;
                default:
                    return Vector3.zero;
            }
        }
    }
}

