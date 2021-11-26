using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "XS/AnimacioPerCodi/Esdeveniment", fileName = "Esdeveniment")]
public class Animacio_Esdeveniment : AnimacioPerCodi_Base.Transformacions
{
    [SerializeField] [Range(0, 1)] float play = 0.5f;
    [SerializeField] UnityEvent esdeveniment;

    bool esdevingut;

    public override void Transformar(Transform transform, float temps)
    {
        if (temps < play) esdevingut = false;
        else Esdevenir();
        /*if (temps >= play && !esdevingut)
        {
            esdevingut = true;
            esdeveniment?.Invoke();
        }
        if (esdevingut && temps >= 1) 
        {
            esdevingut = false;
        } */
    }
    void Esdevenir()
    {
        if (esdevingut)
            return;

        esdeveniment?.Invoke();
        esdevingut = true;
    }
}
