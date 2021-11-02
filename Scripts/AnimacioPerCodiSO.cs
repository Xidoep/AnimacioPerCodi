using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimacioPerCodiSO : MonoBehaviour
{
    public AnimacioSO[] animacions;


    public void Play(int index = 0)
    {
        for (int i = 0; i < animacions.Length; i++) animacions[i].Stop(false);
        StartCoroutine(animacions[index].Play(transform, Prova));
    }

    void Stop(int index)
    {
        animacions[index].Stop(false);
    }

    void StopAlFinal(int index)
    {
        animacions[index].Stop(true);
    }



    void Prova()
    {
        Debug.Log("wea");
    }

}
