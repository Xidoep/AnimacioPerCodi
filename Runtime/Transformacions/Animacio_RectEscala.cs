using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_RectEscala : Animacio
{
    public Animacio_RectEscala() { }
    public Animacio_RectEscala(Vector3 inici, Vector3 final, bool dinamic = false)
    {
        corba = Corba.Linear;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [Title("Rect escala", horizontalLine: false), SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, HorizontalGroup("1"), LabelWidth(35), HideIf("@this.dinamic == true")] Vector2 inici;
    [SerializeField, HorizontalGroup("1", width: 40), ToggleLeft, LabelText("din")] bool dinamic;
    [SerializeField, HorizontalGroup("2", marginRight: 43), LabelWidth(35)] Vector2 final;

    //inici
    Vector2 inicidin = Vector2.zero;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0) inicidin = ((RectTransform)objectiu.transform).sizeDelta;

        Accio(inicidin, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        ((RectTransform)objectiu.transform).sizeDelta = Vector2.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
