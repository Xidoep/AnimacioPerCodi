using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AnimacioPerCodi_Esdeveniments : AnimacioPerCodi_Base
{
    [SerializeField] T_Esdeveniment[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

    [ContextMenu("Play")] void PlayProva() => Play();



    [System.Serializable]
    public class T_Esdeveniment : Transformacions
    {
        [SerializeField] [Range(0, 1)] float play = 0.5f;
        [SerializeField] UnityEvent esdeveniment;

        bool activat = false;

        public override void Transformar(Transform transform, float temps)
        {
            if (temps > play && !activat)
            {
                activat = true;
                esdeveniment?.Invoke();
            }
            if (activat && temps >= 1) activat = false;
        }

    }

}
