using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_All : AnimacioPerCodi_Base
{
    [SerializeField] Animacio_Multiple[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

}
