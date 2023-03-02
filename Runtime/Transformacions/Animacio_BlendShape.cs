using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_BlendShape : Animacio
{
    [SerializeField] string nom = "BlendShape";
    public Animacio_BlendShape() { }
    public Animacio_BlendShape(int index)
    {
        corba = Corba.Linear;
        this.index = index;
    }

    [SerializeField] protected AnimationCurve corba = new AnimationCurve();
    [Space(10)]
    [SerializeField] int index;

    public override void Transformar(Component objectiu, float frame)
    {
        ((SkinnedMeshRenderer)objectiu).SetBlendShapeWeight(index, corba.Evaluate(frame));
    }
}
