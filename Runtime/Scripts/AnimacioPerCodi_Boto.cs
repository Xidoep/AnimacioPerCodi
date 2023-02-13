using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioBoto", fileName = "AnimacioBoto")]
public class AnimacioPerCodi_Boto : ScriptableObject
{
    public AnimacioPerCodi onClick;
    public AnimacioPerCodi onEnter;
    public AnimacioPerCodi onExit;
    public AnimacioPerCodi loop;


    public void PlayOnEnter(Component component, ref Coroutine corrutineLoop)
    {
        Debug.Log($"1.-Component = {component.name}");

        onEnter.Play(component);
        if (onEnter.TeAnimacions)
            corrutineLoop = LoopPlayDelayed(component, onEnter.Temps);
        else loop.Play(component);
    }

    public void PlayOnClick(Component component, ref Coroutine corrutineLoop) 
    {
        //onEnter.Lector(component).Stop();
        LoopStop(component, corrutineLoop);

        onClick.Play(component);

        corrutineLoop = LoopPlayDelayed(component, onClick.Temps);
    }
    

    public void PlayOnExit(Component component, ref Coroutine corrutineLoop)
    {
        corrutineLoop = LoopStop(component, corrutineLoop);
        onExit.Play(component);
    }




    Coroutine LoopPlayDelayed(Component component, float temps)
    {
        Coroutine corrutineLoop = XS_Coroutine.StartCoroutine_Ending(temps, LoopAfter);

        void LoopAfter() => loop.Play(component);
        return corrutineLoop;
    }
    Coroutine LoopStop(Component component, Coroutine corrutineLoop)
    {
        if (corrutineLoop != null)
        {
            loop.Lector(component).Stop();
            XS_Coroutine.StopCoroutine(corrutineLoop);
        }
        return null;
    }





}
