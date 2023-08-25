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

    [Title("Posicio", horizontalLine: false), SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, HorizontalGroup("1"), LabelWidth(35), HideIf("@this.dinamic == true")] Vector3 inici;
    [SerializeField, HorizontalGroup("1", width: 40), ToggleLeft, LabelText("din")] bool dinamic;
    [SerializeField, HorizontalGroup("2", marginRight: 43), LabelWidth(35)] Vector3 final;
    [SerializeField, ToggleLeft] bool local;


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

