using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_RotacioVector : Animacio
{
    [SerializeField] string nom = "Rotacio a voltat de vector";
    public Animacio_RotacioVector() { }
    public Animacio_RotacioVector(Vector3 eix, float inici, float final)
    {
        corba = Corba.Linear;
        this.eix = eix;
        this.inici = inici;
        this.final = final;
    }

    [Title("Rotacio a voltat de vector", horizontalLine: false), SerializeField, HideLabel] protected AnimationCurve corba = new AnimationCurve();
    [SerializeField, LabelWidth(40)] Vector3 eix;
    [SerializeField, HorizontalGroup("1"), LabelWidth(30)] float inici, final;

    public override void Transformar(Component objectiu, float frame)
    {
        float angle = Mathf.LerpUnclamped(inici, final, corba.Evaluate(frame));
        ((Transform)objectiu).rotation = Quaternion.AngleAxis(angle, eix);
    }
}
