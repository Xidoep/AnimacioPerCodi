using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Text_Color : Animacio
{
    public Animacio_Text_Color() { }
    public Animacio_Text_Color(Color inici, Color final, bool dinamic = false)
    {
        corba = Corba.Linear;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [Title("Text color", horizontalLine: false), SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, HorizontalGroup("1"), LabelWidth(45), HideIf("@this.dinamic == true")] Color inici;
    [SerializeField, HorizontalGroup("1", width: 40), LabelText("din"), ToggleLeft] bool dinamic;
    [SerializeField, HorizontalGroup("2", marginRight: 43), LabelWidth(45)] Color final;

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
