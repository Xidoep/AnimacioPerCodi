using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/BlendShape", fileName = "BlendShape")]
public class Animacio_BlendShape : AnimacioPerCodi_Base.Transformacions
{
    public override AnimacioPerCodi_Base.Transformacions Create()
    {
        Animacio_BlendShape t = (Animacio_BlendShape)ScriptableObject.CreateInstance<Animacio_BlendShape>();
        t.blendShapeIndex = blendShapeIndex;
        t.corba = corba;
        t.skinnedMeshRenderer = null;
        return t;
    }

    [SerializeField] int blendShapeIndex;
    [SerializeField] AnimationCurve corba = new AnimationCurve();
    SkinnedMeshRenderer skinnedMeshRenderer;

    public override void Transformar(Transform transform, float temps)
    {
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = transform.GetComponent<SkinnedMeshRenderer>();
        }

        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, corba.Evaluate(temps) * 100);
    }
}
