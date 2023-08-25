using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Rotacio : Animacio
{
    public Animacio_Rotacio() { }
    public Animacio_Rotacio(Vector3 inici, Vector3 final, bool local = true, bool dinamic = false)
    {
        corba = Corba.Linear;
        this.inici = inici;
        this.final = final;
        this.local = local;
        this.dinamic = dinamic;
    }

    [Title("Rotacio", horizontalLine: false), SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, HorizontalGroup("1"), LabelWidth(35), HideIf("@this.dinamic == true")] Vector3 inici;
    [SerializeField, HorizontalGroup("1", width: 40), ToggleLeft, LabelText("din")] bool dinamic;
    [SerializeField, HorizontalGroup("2", marginRight: 43), LabelWidth(35)] Vector3 final;
    [SerializeField, ToggleLeft] bool local;

    //INTERN
    //Vector3 iniciDin;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0)
        {
            if (!local)
                inici = objectiu.transform.eulerAngles;
            else inici = objectiu.transform.localEulerAngles;
        }

        Accio(inici, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        if (!local)
            objectiu.transform.rotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame)));
        else objectiu.transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame)));
    }

}
