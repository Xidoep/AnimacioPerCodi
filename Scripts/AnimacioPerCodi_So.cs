using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_So : AnimacioPerCodi_Base
{
    [SerializeField] T_So[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

    [System.Serializable]
    public class T_So : Transformacions
    {
        [SerializeField] [Range(0, 1)] float play = 0.5f;
        [SerializeField] So so;

        bool played;


        public override void Transformar(Transform transform, float temps)
        {
            if (temps < play) played = false;
            else Play();
        }
        void Play()
        {
            if (played)
                return;

            so.Play();
            played = true;
        }
    }
}
