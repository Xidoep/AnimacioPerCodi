using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_ShaderFloat : Animacio
{
    [SerializeField] string nom = "Shader float";
    public Animacio_ShaderFloat() { }
    public Animacio_ShaderFloat(string propietat, float inici, float final, bool dinamic = false)
    {
        corba = Corba.Linear();
        this.propietat = propietat;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] string propietat;
    [SerializeField] float inici, final;
    [Space(10)]
    [SerializeField] bool dinamic;


    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0) inici = ((MeshRenderer)objectiu).material.GetFloat(propietat);

        Accio(inici, objectiu, frame);
    }
    void Accio(float inici, Component objectiu, float frame)
    {
        ((MeshRenderer)objectiu).material.SetFloat(propietat, Mathf.LerpUnclamped(inici, final, corba.Evaluate(frame)));
    }

}
