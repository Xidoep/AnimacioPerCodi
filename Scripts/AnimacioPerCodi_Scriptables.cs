using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_Scriptables : AnimacioPerCodi_Base
{
    [SerializeField] Transformacions[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    [ContextMenu("Play0")] void Play0() => Play(0);
    [ContextMenu("Play1")] void Play1() => Play(1);
}
