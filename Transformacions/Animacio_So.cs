using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/So", fileName = "So")]
public class Animacio_So : AnimacioPerCodi_Base.Transformacions
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
