using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioPerCodi_All : AnimacioPerCodi_Base
{
    [SerializeField] Animacio_Multiple[] transformacions;

    internal override Transformacions[] GetTransformacions => transformacions;

    [ContextMenu("Play0")] void Play0() => Play(0);
    [ContextMenu("Play1")] void Play1() => Play(1);


    new public void Play()
    {
        if (!gameObject.activeSelf)
            return;

        all = false;
        index = 0;

        StartCoroutine(PlayCorrutina());
    }

}
