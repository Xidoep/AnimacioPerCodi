using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_So : AnimacioPerCodi_Base
{
    [SerializeField] Animacio_So[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();
}
