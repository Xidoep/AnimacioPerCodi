using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_RectPosicio : Animacio
{
    public Animacio_RectPosicio() { }
    public Animacio_RectPosicio(Vector2 inici, Vector2 final, AnimationCurve corba = null, bool dinamic = false)
    {
        this.corba = corba != null ? Corba.Linear : corba;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [Title("Rect posicio", horizontalLine: false), SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, HorizontalGroup("1"), LabelWidth(35), HideIf("@this.dinamic == true")] Vector2 inici;
    [SerializeField, HorizontalGroup("1", width: 40), ToggleLeft, LabelText("din")] bool dinamic;
    [SerializeField, HorizontalGroup("2", marginRight: 43), LabelWidth(35)] Vector2 final;

    //INTERN
    Vector2 inicidin = Vector2.zero;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0) inicidin = ((RectTransform)objectiu.transform).anchoredPosition;

        Accio(inicidin, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        ((RectTransform)objectiu.transform).anchoredPosition = Vector2.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
