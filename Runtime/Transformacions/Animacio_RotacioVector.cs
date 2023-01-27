using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_RotacioVector : Animacio
{
    [SerializeField] string nom = "Rotacio a voltat de vector";
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

    public override void Transformar(Component objectiu, float frame)
    {
        float angle = Mathf.LerpUnclamped(inici, final, corba.Evaluate(frame));
        ((Transform)objectiu).rotation = Quaternion.AngleAxis(angle, eix);
    }

}
