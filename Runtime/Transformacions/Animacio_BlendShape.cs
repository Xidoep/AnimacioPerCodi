using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_BlendShape : Animacio
{
    public Animacio_BlendShape() { }
    public Animacio_BlendShape(int index)
    {
        corba = Corba.Linear;
        this.index = index;
    }

    [SerializeField, HorizontalGroup("1", Title = "Blend Shape", Width = 110, Gap = 10), LabelWidth(75)] int index;
    [SerializeField, HorizontalGroup("1", Title = "Blend Shape"), HideLabel] protected AnimationCurve corba = new AnimationCurve();

    public override void Transformar(Component objectiu, float frame)
    {
        ((SkinnedMeshRenderer)objectiu).SetBlendShapeWeight(index, corba.Evaluate(frame));
    }
}
