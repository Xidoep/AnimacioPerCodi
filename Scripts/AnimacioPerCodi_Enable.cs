using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_Enable : AnimacioPerCodi_Base
{
    [SerializeField] Transformacions[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    private void OnEnable()
    {
        Play();
    }
}
