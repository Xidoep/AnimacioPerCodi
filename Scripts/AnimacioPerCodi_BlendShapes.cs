using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_BlendShapes : AnimacioPerCodi_Base
{
    [SerializeField] T_BlendShape[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();


    [System.Serializable]
    public class T_BlendShape : Transformacions
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
}
