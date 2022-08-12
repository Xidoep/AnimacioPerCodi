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

    public void Add(Transformacions transformacio)
    {
        List<Transformacions> tmp;
        if (transformacions != null) tmp = new List<Transformacions>(transformacions);
        else tmp = new List<Transformacions>();
        tmp.Add(transformacio);
        transformacions = tmp.ToArray();
    }
}
