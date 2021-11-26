using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimacioPerCodi_Shader : AnimacioPerCodi_Base
{
    enum Tipus { Float, Vector }
    [SerializeField] Animacio_Shader[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

}
