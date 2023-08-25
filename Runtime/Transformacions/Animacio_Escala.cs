using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Escala : Animacio
{
    public Animacio_Escala() => corba = Corba.Linear;
    public Animacio_Escala(Vector3 inici, Vector3 final, bool dinamic = false)
    {
        corba = Corba.Linear;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    
    [Title("Escala", horizontalLine: false), HideLabel, SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, HorizontalGroup("1"), LabelWidth(45), HideIf("@this.dinamic == true")] Vector3 inici;
    [SerializeField, HorizontalGroup("1", width: 40), LabelText("din"), ToggleLeft] bool dinamic;
    [SerializeField, HorizontalGroup("2", marginRight: 43), LabelWidth(45)] Vector3 final;

    //INTERN
    Vector3 iniciDin;



    public override void Transformar(Component objectiu, float frame)
    { 
        if (!dinamic) Accio(inici, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0)
        {
            iniciDin = objectiu.transform.localScale;
        }

        Accio(iniciDin, objectiu, frame);
    }

    void Accio(Vector3 inici, Component objectiu, float frame)
    {
        objectiu.transform.localScale = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
}
