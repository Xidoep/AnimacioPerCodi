using UnityEngine;

[DisallowMultipleComponent]
public class AnimacioPerCodi : AnimacioPerCodi_Base
{
    [SerializeField] Transformacions[] transformacions;
    internal override Transformacions[] GetTransformacions { get => GetInstancedTransformacions(transformacions); set => transformacions = value; }
}
