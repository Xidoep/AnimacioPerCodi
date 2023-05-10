using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Animacio_Text_Color : Animacio
{
    [SerializeField] string nom = "Text color";
    public Animacio_Text_Color() { }
    public Animacio_Text_Color(Color inici, Color final, bool dinamic = false)
    {
        corba = Corba.Linear;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] Color inici;
    [SerializeField] Color final;
    [Space(10)]
    [SerializeField] bool dinamic;

    Color iniciDin;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0)
        {
            iniciDin = ((TMP_Text)objectiu).color;
        }

        Accio(iniciDin, objectiu, frame);
    }

    void Accio(Color inici, Component objectiu, float frame)
    {
        ((TMP_Text)objectiu).color = Color.Lerp(inici, final, corba.Evaluate(frame));
    }
}
