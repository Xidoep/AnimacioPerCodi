using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_Escala : Animacio
{
    public Animacio_Escala() { }
    public Animacio_Escala(Vector3 inici, Vector3 final, bool dinamic = false)
    {
        corba = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [Header("Rotacio")]
    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] Vector3 inici;
    [SerializeField] Vector3 final;
    [Space(10)]
    [SerializeField] bool dinamic;

    protected override void AddOrGet<T>(Transform transform) => base.AddOrGet<LectorTransform>(transform);
    public override void Transformar(object objectiu, float frame)
    {
        if (dinamic && frame == 0)
            inici = ((Transform)objectiu).localScale;

        ((Transform)objectiu).localScale = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
