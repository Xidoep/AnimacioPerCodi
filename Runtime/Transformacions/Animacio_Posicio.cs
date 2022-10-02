using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_Posicio : Animacio
{
    public Animacio_Posicio() { }
    public Animacio_Posicio(Vector3 inici, Vector3 final, bool local = true, bool dinamic = false)
    {
        corba = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });
        this.inici = inici;
        this.final = final;
        this.local = local;
        this.dinamic = dinamic;
    }

    [Header("Posicio")]
    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] Vector3 inici;
    [SerializeField] Vector3 final;
    [Space(10)]
    [SerializeField] bool local;
    [SerializeField] bool dinamic;

    protected override void AddOrGet<T>(Transform transform) => base.AddOrGet<LectorTransform>(transform);
    public override void Transformar(object objectiu, float frame)
    {
        if (dinamic && frame == 0)
        {
            if (!local)
                inici = ((Transform)objectiu).position;
            else inici = ((Transform)objectiu).localPosition;
        }

        if (!local)
            ((Transform)objectiu).position = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
        else ((Transform)objectiu).localPosition = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }

}

