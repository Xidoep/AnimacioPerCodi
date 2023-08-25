using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_ShaderFloat : Animacio
{
    public Animacio_ShaderFloat() { }
    public Animacio_ShaderFloat(string propietat, float inici, float final, AnimationCurve corba = null, bool dinamic = false)
    {
        this.corba = corba != null ? Corba.Linear : corba;
        this.propietat = propietat;
        this.inici = inici;
        this.final = final;
        this.dinamic = dinamic;
    }

    [Title("Shader float", horizontalLine: false), LabelWidth(100), SerializeField] string propietat;
    [SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, HorizontalGroup("1"), ShowIf("@this.dinamic == false"), LabelWidth(30)] float inici;
    [SerializeField, HorizontalGroup("1"), LabelWidth(30)] float final;
    [SerializeField, HorizontalGroup("1", 40), ToggleLeft, LabelText("din"), ToggleLeft] bool dinamic;


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
