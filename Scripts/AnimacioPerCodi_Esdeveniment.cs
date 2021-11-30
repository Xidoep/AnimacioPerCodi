using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimacioPerCodi_Esdeveniment : AnimacioPerCodi_Base
{
    internal override Transformacions[] GetTransformacions => null;
    [SerializeField] ModificacioEvent[] transformacions;

    internal override void TransformarAll(float temps) 
    {
        for (int i = 0; i < transformacions.Length; i++) transformacions[i].Transformar(transform, temps);
    }

    [System.Serializable]
    public class ModificacioEvent
    {
        [Range(0, 1)] [SerializeField] float play = 0.5f;
        //internal System.Action esdeveniment;
        [SerializeField] UnityEvent esdeveniment;

        bool esdevingut;

        public void Transformar(Transform transform, float temps)
        {
            if (temps < play) esdevingut = false;
            else Esdevenir();
        }

        void Esdevenir()
        {
            if (esdevingut)
                return;

            esdeveniment?.Invoke();
            esdevingut = true;
        }
    }

}
