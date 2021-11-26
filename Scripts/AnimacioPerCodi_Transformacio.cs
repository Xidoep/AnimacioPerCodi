using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_Transformacio : AnimacioPerCodi_Base
{
    enum Tipus { Moviment, Rotacio, Escalat, RectPosition }

    [SerializeField] Animacio_Transformacio[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;


    new public void Play() => base.Play();

    [ContextMenu("Play")] void PlayProva() => Play();
    [ContextMenu("Stop")] void StopProva() => StopInmediatament();
    [ContextMenu("Stop Quan acabi")] void StopFinalProva() => StopAlFinal();

}

