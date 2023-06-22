using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioGameObject", fileName = "AnimacioGameObject")]
public class AnimacioPerCodi_GameObject : ScriptableObject
{
    [SerializeScriptableObject] public AnimacioPerCodi onEnabled;
    [SerializeScriptableObject] public AnimacioPerCodi idle;
    [SerializeScriptableObject] public AnimacioPerCodi onPointerEnter;
    [SerializeScriptableObject] public AnimacioPerCodi apuntat;
    [SerializeScriptableObject] public AnimacioPerCodi onPointerDown;
    [SerializeScriptableObject] public AnimacioPerCodi onPointerUp;
    [SerializeScriptableObject] public AnimacioPerCodi onPointerExit;
    [SerializeScriptableObject] public AnimacioPerCodi onDestroyOrDisable;

    bool destroyingOrdisabling = false;

    /// <summary>
    /// </summary>
    /// <param name="component"></param>
    /// <returns>Corrutine Idle</returns>
    public Coroutine OnEnabled(Component component) 
    {
        destroyingOrdisabling = false;

        return component.Animacio_LoopDespres(onEnabled, this.idle);
    }
    /// <summary>
    /// </summary>
    /// <param name="component"></param>
    /// <param name="coroutine"></param>
    /// <returns>Corrutine apuntat</returns>
    public Coroutine OnPointerEnter(Component component, Coroutine coroutine)
    {
        if (destroyingOrdisabling)
            return null;

        return component.StopAnterior_Animacio_LoopDespres(onPointerEnter, idle, coroutine, apuntat);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="coroutine"></param>
    /// <returns>Corrutine apuntat</returns>
    public Coroutine OnPointerDown(Component component, Coroutine coroutine)
    {
        if (destroyingOrdisabling)
            return null;

        if (!onPointerUp || !onPointerUp.TeAnimacions) //No hi ha onPointerUp
        {
            return component.StopAnterior_Animacio_LoopDespres(onPointerDown, apuntat, coroutine, apuntat);
        }
        else return component.StopAnterior_Animacio(onPointerDown, apuntat, coroutine);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="coroutine"></param>
    /// <param name="destroy"></param>
    /// <param name="disable"></param>
    /// <returns>Corrutine Apuntat</returns>
    public Coroutine OnPointerUp(Component component, Coroutine coroutine, bool destroy = false, bool disable = false)
    {
        if (destroyingOrdisabling)
            return null;

        if (!onPointerDown || !onPointerDown.TeAnimacions) //No s'ha fet el onPointerDown
        {
            if (destroy) return DestroyAmbAnimacio(component, coroutine);
            else if (disable) return DisableAmbAnimacio(component, coroutine);
            else return component.StopAnterior_Animacio_LoopDespres(onPointerUp, apuntat, coroutine, apuntat);
        }
        else
        {
            if (destroy) return DestroyAmbAnimacio(component, true);
            else if (disable) return DisableAmbAnimacio(component, true);
            else  return component.Animacio_LoopDespres(onPointerUp, apuntat);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="coroutine"></param>
    /// <returns>Corrutine Idle</returns>
    public Coroutine OnPointerExit(Component component, Coroutine coroutine)
    {
        if (destroyingOrdisabling)
            return null;

        return component.StopAnterior_Animacio_LoopDespres(onPointerExit, apuntat, coroutine, idle);
    }



    public void Destroy(Component component, ref Coroutine loop, bool performDestroy = true)
    {
        loop = CorrutineStop(loop);
        DestroyAmbAnimacio(component, performDestroy);
    }
    Coroutine DestroyAmbAnimacio(Component component, Coroutine loop, bool performDestroy = true)
    {
        DestroyAmbAnimacio(component, performDestroy);
        return CorrutineStop(loop);
    }
    Coroutine DestroyAmbAnimacio(Component component, bool performDestroy)
    {
        destroyingOrdisabling = true;
        if (onDestroyOrDisable) onDestroyOrDisable.Play(component);
        if (performDestroy) Destroy(component.gameObject, (onDestroyOrDisable && onDestroyOrDisable.TeAnimacions) ? onDestroyOrDisable.Temps : 0);
        return null;
    }



    public void Disable(Component component, ref Coroutine loop, bool performDisable = true)
    {
        loop = CorrutineStop(loop);
        DisableAmbAnimacio(component, performDisable);
    }
    Coroutine DisableAmbAnimacio(Component component, Coroutine loop, bool performDisable = true)
    {
        DisableAmbAnimacio(component, performDisable);
        return CorrutineStop(loop);
    }
    Coroutine DisableAmbAnimacio(Component component, bool performDisable)
    {
        destroyingOrdisabling = true;
        if (onDestroyOrDisable) onDestroyOrDisable.Play(component);

        if (performDisable)
        {
            if (onDestroyOrDisable && onDestroyOrDisable.TeAnimacions) XS_Coroutine.StartCoroutine_Ending(onDestroyOrDisable.Temps, Disable);
            else Disable();
        }

        return null;

        void Disable() => component.gameObject.SetActive(false);
    }





    Coroutine LoopPlayDelayed(Component component, float temps)
    {
        Coroutine corrutineLoop = XS_Coroutine.StartCoroutine_Ending(temps, LoopAfter);

        void LoopAfter() => apuntat.Play(component);
        return corrutineLoop;
    }
    Coroutine IdlePlayDelayed(Component component, float temps)
    {
        Coroutine corrutineLoop = XS_Coroutine.StartCoroutine_Ending(temps, IdleAfter);

        void IdleAfter() => idle.Play(component);
        return corrutineLoop;
    }
    Coroutine CorrutineStop(Coroutine corrutineLoop)
    {
        if (corrutineLoop != null)
            XS_Coroutine.StopCoroutine(corrutineLoop);

        return null;
    }


}
