using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimacioPerCodi_Color : AnimacioPerCodi_Base
{
    enum Tipus { Color, Alfa }
    [SerializeField] Animacio_Color[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

}
