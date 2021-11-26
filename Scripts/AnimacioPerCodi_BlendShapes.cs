using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_BlendShapes : AnimacioPerCodi_Base
{
    [SerializeField] Animacio_BlendShape[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

}
