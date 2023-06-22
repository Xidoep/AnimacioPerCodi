using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Posicio : Animacio
{
    //[SerializeField] string nom = "Posicio";
    public Animacio_Posicio() { }
    public Animacio_Posicio(Vector3 inici, Vector3 final, bool local = true, bool dinamic = false)
    {
        corba = Corba.Linear;
        this.inici = inici;
        this.final = final;
        this.local = local;
        this.dinamic = dinamic;
    }

    [BoxGroup("Posicio"), SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [BoxGroup("Posicio"), SerializeField, LabelWidth(34), HideIf("@this.dinamic == true")] Vector3 inici;
    [BoxGroup("Posicio"), SerializeField, LabelWidth(34), HorizontalGroup("Posicio/p")] Vector3 final;
    [Space(10)]
    [BoxGroup("Posicio"), SerializeField, LabelWidth(34)] bool local;
    [BoxGroup("Posicio"), SerializeField, HorizontalGroup("Posicio/p", width: 40), ToggleLeft, LabelText("Din.")] bool dinamic;


    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        Vector3 inici = Vector3.zero;

        if (!local)
            inici = ((Transform)objectiu).position;
        else inici = ((Transform)objectiu).localPosition;

        Accio(inici, objectiu, frame);
    }
    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        if (!local)
            objectiu.transform.position = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
        else objectiu.transform.localPosition = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}

