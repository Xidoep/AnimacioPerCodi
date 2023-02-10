using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioBoto", fileName = "AnimacioBoto")]
public class AnimacioPerCodi_Boto : ScriptableObject
{
    [ContextMenuItem("AddPosicio", "AddPosicio")]
    [SerializeField] AnimacioPerCodi.Interaccio onClick;
    [SerializeField] AnimacioPerCodi.Interaccio onEnter;
    [SerializeField] AnimacioPerCodi.Interaccio onExit;
    [SerializeField] AnimacioPerCodi.Interaccio loop;

    public AnimacioPerCodi.Interaccio OnClick => onClick;
    public AnimacioPerCodi.Interaccio OnEnter => onEnter;
    public AnimacioPerCodi.Interaccio OnExit => onExit;
    public AnimacioPerCodi.Interaccio Loop => loop;

    Lector lector;
    /*public void PlayOnClick(Component component) => onClick.Play(component, Transicio.clamp, ref lector);
    public void PlayOnEnter(Component component) => onEnter.Play(component, Transicio.clamp, ref lector);
    public void PlayOnExit(Component component) => onExit.Play(component, Transicio.clamp, ref lector);
    public void PlayLoop(Component component) => loop.Play(component, Transicio.loop, ref lector);
    */
    private void OnEnable()
    {
        lector = null;
    }

    //Fa animacio de Entrar si la te, i animacio Loop amb delay si te animacio Entrar
    public void PlayOnEnter(Component component, ref Coroutine corrutineLoop)
    {
        onEnter.Play(component, Transicio.clamp, ref lector);

        if (onEnter.TeAnimacions)
            corrutineLoop = LoopPlayDelayed(component, onEnter.Temps);
        else loop.Play(component, Transicio.loop, ref lector);
    }

    //Atura Loop, fa animacio Play, i torna a animacio Loop amb delay
    public void PlayOnClick(Component component, ref Coroutine corrutineLoop) 
    {
        LoopStop(corrutineLoop);

        onClick.Play(component, Transicio.clamp, ref lector);

        corrutineLoop = LoopPlayDelayed(component, onClick.Temps);
    }
    

    
    //Atura animacio Loop i fa animacio Exit.
    public void PlayOnExit(Component component, ref Coroutine corrutineLoop)
    {
        corrutineLoop = LoopStop(corrutineLoop);
        onExit.Play(component, Transicio.clamp, ref lector);
    }




    Coroutine LoopPlayDelayed(Component component, float temps)
    {
        Coroutine corrutineLoop = XS_Coroutine.StartCoroutine_Ending(temps, LoopAfter);

        void LoopAfter() => loop.Play(component, Transicio.loop, ref lector);
        return corrutineLoop;
    }
    Coroutine LoopStop(Coroutine corrutineLoop)
    {
        if (corrutineLoop != null)
        {
            XS_Coroutine.StopCoroutine(corrutineLoop);
        }
        return null;
    }





}
