using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public static class Extenions_AnimacioPerCodi
{

    public static void SetupAndPlay(this Component component, Lector lector, Animacio[] animacions, float temps, Transicio transicio)
    {
        if (animacions == null || animacions.Length == 0)
            return;

        lector.Setup(animacions, component, temps, transicio);
        lector.Play();
    }
    static void GetLector(this Component component, ref Lector lector)
    {
        if(lector)
            lector = component.gameObject.GetComponent<Lector>();
        else lector = component.gameObject.AddComponent<Lector>();
    }


    public static Coroutine Animacio_LoopDespres(this Component component, AnimacioPerCodi animacio, AnimacioPerCodi proxima)
    {
        animacio?.Play(component);

        if (animacio.TeAnimacions)
            return component.Loop(proxima, animacio.Temps);
        else proxima?.Play(component);

        return null;
    }

    public static Coroutine StopAnterior_Animacio_LoopDespres(this Component component, AnimacioPerCodi animacio, AnimacioPerCodi anteiorAnimacio, Coroutine anteriorCoroutine, AnimacioPerCodi proxima)
    {
        component.Stop(anteiorAnimacio, anteriorCoroutine);

        animacio?.Play(component);

        if (animacio.TeAnimacions)
            return component.Loop(proxima, animacio.Temps);
        else proxima?.Play(component);

        return null;
    }

    public static Coroutine StopAnterior_Animacio(this Component component, AnimacioPerCodi animacio, AnimacioPerCodi anteiorAnimacio, Coroutine anteriorCoroutine)
    {
        component.Stop(anteiorAnimacio, anteriorCoroutine);

        animacio.Play(component);

        return null;
    }

    public static Coroutine Loop(this Component component, AnimacioPerCodi loop, float temps)
    {
        return XS_Coroutine.StartCoroutine_Ending(temps, LoopAfter);

        void LoopAfter() => loop?.Play(component);
    }
    public static Coroutine Stop(this Component component, AnimacioPerCodi loopAnimacio, Coroutine loopCorrutine)
    {
        if (loopCorrutine != null)
        {
            if (loopAnimacio) loopAnimacio.Lector(component).Stop();
            XS_Coroutine.StopCoroutine(loopCorrutine);
        }
        return null;
    }
}
