using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "XS/AnimacioPerCodi/BlendShape", fileName = "BlendShape")]
public class Animacio_BlendShape : AnimacioPerCodi_Base.Transformacions
{
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
