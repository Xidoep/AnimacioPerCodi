using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_Enable : AnimacioPerCodi_Base
{
    [SerializeField] Transformacions[] transformacions;
    internal override Transformacions[] GetTransformacions { get => GetInstancedTransformacions(transformacions); set => transformacions = value; }

    private void OnEnable()
    {
        Play();
    }
}
