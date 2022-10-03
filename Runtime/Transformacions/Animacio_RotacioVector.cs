using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_RotacioVector : Animacio
{
    public Animacio_RotacioVector() { }
    public Animacio_RotacioVector(Vector3 eix, float inici, float final)
    {
        corba = Corba.Linear();
        this.eix = eix;
        this.inici = inici;
        this.final = final;
    }

    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] Vector3 eix;
    [SerializeField] float inici, final;

    protected override void AddOrGet<T>(Transform transform) => base.AddOrGet<T>(transform);

    public override void Transformar(object objectiu, float frame)
    {
        float angle = Mathf.LerpUnclamped(inici, final, corba.Evaluate(frame));
        ((Transform)objectiu).rotation = Quaternion.AngleAxis(angle, eix);
    }

}
