using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_All : AnimacioPerCodi_Base
{
    [SerializeField] T_All[] transformacions;

    internal override Transformacions[] GetTransformacions => transformacions;

    [ContextMenu("Play0")] void Play0() => Play(0);
    [ContextMenu("Play1")] void Play1() => Play(1);


    new public void Play()
    {
        if (!gameObject.activeSelf)
            return;

        all = false;
        index = 0;

        StartCoroutine(PlayCorrutina());
    }





    [System.Serializable]
    public class T_All : Transformacions
    {
        public AnimacioPerCodi_Transformacio.T_Transformacio[] transformacio;
        public AnimacioPerCodi_Color.T_Color[] color;
        public AnimacioPerCodi_Shader.T_Shader[] shader;
        public AnimacioPerCodi_BlendShapes.T_BlendShape[] blendShape;
        public AnimacioPerCodi_Esdeveniments.T_Esdeveniment[] esdeveniment;
        public AnimacioPerCodi_So.T_So[] so;

        public override void Transformar(Transform transform, float temps)
        {
            for (int i = 0; i < transformacio.Length; i++) transformacio[i].Transformar(transform, temps);
            for (int i = 0; i < color.Length; i++) color[i].Transformar(transform, temps);
            for (int i = 0; i < shader.Length; i++) shader[i].Transformar(transform, temps);
            for (int i = 0; i < blendShape.Length; i++) blendShape[i].Transformar(transform, temps);
            for (int i = 0; i < esdeveniment.Length; i++) esdeveniment[i].Transformar(transform, temps);
            for (int i = 0; i < so.Length; i++) so[i].Transformar(transform, temps);
        }
    }
}
