using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AnimacioPerCodi_Esdeveniments : AnimacioPerCodi_Base
{
    [SerializeField] Animacio_Esdeveniment[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

    [ContextMenu("Play")] void PlayProva() => Play();

}
