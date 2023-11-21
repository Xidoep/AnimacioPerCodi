using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioBoto", fileName = "AnimacioBoto")]
public class AnimacioPerCodi_Boto : ScriptableObject
{
    [SerializeScriptableObject] public AnimacioPerCodi onEnable;
    [SerializeScriptableObject] public AnimacioPerCodi onClick;
    [SerializeScriptableObject] public AnimacioPerCodi onEnter;
    [SerializeScriptableObject] public AnimacioPerCodi onExit;
    [SerializeScriptableObject] public AnimacioPerCodi loop;
    [SerializeScriptableObject] public AnimacioPerCodi onDestroyOrDisable;



    public void EnEnable(Component component) 
    {
        if (!onEnable || !onEnable.TeAnimacions)
            return;
            
        onEnable.Play(component);
    } 
    public Coroutine OnEnter(Component component, bool destroyingOrdisabling = false) 
    {
        if (destroyingOrdisabling)
            return null;

        return component.Animacio_LoopDespres(onEnter, loop);
    }
    public Coroutine OnClick(Component component, Coroutine corrutine, bool destroyingOrdisabling = false) 
    {
        if (destroyingOrdisabling)
            return null;

        return component.StopAnterior_Animacio_LoopDespres(onClick, loop, corrutine, loop);
    }
    public Coroutine OnExit(Component component, Coroutine corrutine, bool destroyingOrdisabling = false) 
    {
        if (destroyingOrdisabling)
            return null;

        return component.StopAnterior_Animacio(onExit, loop, corrutine);
    } 

    public bool Destroy(Component component, Coroutine coroutine) 
    {
        if (coroutine != null)
            XS_Coroutine.StopCoroutine(coroutine);

        if(onDestroyOrDisable && onDestroyOrDisable.TeAnimacions)
        {
            Destroy(component.gameObject, onDestroyOrDisable.Temps);
            onDestroyOrDisable.Play(component);
            return true;
        }
        else
        {
            Destroy(component.gameObject);
            return false;
        }
    }

    public bool Disable(Component component, bool performDisable)
    {
        if (onDestroyOrDisable && onDestroyOrDisable.TeAnimacions)
        {
            XS_Coroutine.StartCoroutine_Ending(onDestroyOrDisable.Temps, Disable);
            onDestroyOrDisable.Play(component);
            return true;
        }
        else
        {
            Disable();
            return false;
        }

        void Disable() => component.gameObject.SetActive(false);
    }

}