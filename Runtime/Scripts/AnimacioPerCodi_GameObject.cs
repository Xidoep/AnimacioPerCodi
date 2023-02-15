using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioGameObject", fileName = "AnimacioGameObject")]
public class AnimacioPerCodi_GameObject : ScriptableObject
{
    public AnimacioPerCodi onEnabled;
    public AnimacioPerCodi idle;
    public AnimacioPerCodi onPointerEnter;
    public AnimacioPerCodi loop;
    public AnimacioPerCodi onPointerDown;
    public AnimacioPerCodi onPointerUp;
    public AnimacioPerCodi onPointerExit;
    public AnimacioPerCodi onDestroyOrDisable;

    bool destroyingOrdisabling = false;

    /// <summary>
    /// Play a animacio apareixre
    /// </summary>
    /// <param name="component"></param>
    public void PlayOnEnabled(Component component, ref Coroutine idle) 
    {
        destroyingOrdisabling = false;
        if (onEnabled) onEnabled.Play(component);

        if (this.idle && this.idle.TeAnimacions)
        {
            if (onEnabled && onEnabled.TeAnimacions)
                idle = IdlePlayDelayed(component, onEnabled.Temps);
            else this.idle.Play(component);
        }      
    } 

    /// <summary>
    /// Play a animacio en Apuntar, i passa a la animacio Apuntat despres.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="loop"></param>
    public void PlayOnPointerEnter(Component component, ref Coroutine loop, ref Coroutine idle)
    {
        if (destroyingOrdisabling)
            return;

        idle = CorrutineStop(idle);

        if (onPointerEnter) onPointerEnter.Play(component);

        if (this.loop && this.loop.TeAnimacions)
        {
            if (onPointerEnter && onPointerEnter.TeAnimacions)
                loop = LoopPlayDelayed(component, onPointerEnter.Temps);
            else this.loop.Play(component);
        }
    }

    /// <summary>
    /// Atura l'animacio Apuntat i fa Play animacio Clicar. Si no hi ha l'animacio Desclicar, torna a l'animacio Apuntat. 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="loop"></param>
    public void PlayOnPointerDown(Component component, ref Coroutine loop)
    {
        loop = CorrutineStop(loop);
        if (onPointerDown) onPointerDown.Play(component);

        if (this.loop && this.loop.TeAnimacions)
        {
            if (!onPointerUp || !onPointerUp.TeAnimacions)
            {
                if (onPointerDown && onPointerDown.TeAnimacions)
                    loop = LoopPlayDelayed(component, onPointerDown.Temps);
                else this.loop.Play(component);
            }
        }
    }

    /// <summary>
    /// Atura l'animacio Apuntat si es que PlayOnPointerDown no ho ha fet. fa Play a animacio Desclicar i fa el seguent.
    /// Destrueix i amaga el gameObjecte del component si l'hi dius.
    /// o passa a l'animacio Apuntat, si no.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="loop"></param>
    /// <param name="destroy"></param>
    /// <param name="disable"></param>
    public void PlayOnPointerUp(Component component, ref Coroutine loop, ref Coroutine idle, bool destroy = false, bool disable = false)
    {
        if (destroyingOrdisabling)
            return;

        loop = CorrutineStop(loop);

        if (onPointerUp) onPointerUp.Play(component);

        if (destroy || disable)
        {
            destroyingOrdisabling = true;
            if (destroy)
            {
                DestroyAmbAnimacio(component, ref loop, ref idle);
            }
            else
            {
                //loop = CorrutineStop(loop);
                if (onPointerUp && onPointerUp.TeAnimacions) 
                    XS_Coroutine.StartCoroutine_Ending(onPointerUp.Temps, Disable);
                else Disable();
            }
        }
        else
        {
            if (this.loop && this.loop.TeAnimacions)
            {
                if (onPointerUp && onPointerUp.TeAnimacions)
                    loop = LoopPlayDelayed(component, onPointerUp.Temps);
                else this.loop.Play(component);
            }
           
        }

        void Disable() => DisableAmbAnimacio(component, disable);
    }

    /// <summary>
    /// Atura l'animacio Apuntat, i fa Play a l'animacio Desapuntar.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="loop"></param>
    public void PlayOnPointerExit(Component component, ref Coroutine loop, ref Coroutine idle)
    {
        if (destroyingOrdisabling)
            return;

        loop = CorrutineStop(loop);

        if (onPointerExit) onPointerExit.Play(component);

        if (this.idle && this.idle.TeAnimacions)
        {
            if (onPointerExit && onPointerExit.TeAnimacions)
                idle = IdlePlayDelayed(component, onPointerExit.Temps);
            else this.idle.Play(component);
        }
    }

    /// <summary>
    /// Destrueix el gameObject del component despres de l'animacio de DestruirOrAmagar
    /// </summary>
    /// <param name="component"></param>
    /// <param name="loop"></param>
    public void DestroyAmbAnimacio(Component component, ref Coroutine loop, ref Coroutine idle, bool performDestroy = true)
    {
        loop = CorrutineStop(loop);
        idle = CorrutineStop(idle);

        if (onDestroyOrDisable) onDestroyOrDisable.Play(component);
        if (performDestroy) Destroy(component.gameObject, onDestroyOrDisable && onDestroyOrDisable.TeAnimacions ? onDestroyOrDisable.Temps : 0);
        //DestroyAmbAnimacio(component);
    }
    /*void DestroyAmbAnimacio(Component component)
    {
        onDestroyOrDisable.Play(component);
        Destroy(component.gameObject, onDestroyOrDisable.TeAnimacions ? onDestroyOrDisable.Temps : 0);
    }*/

    /// <summary>
    /// Amaga el gameObject del component despres de l'animacio de DestruirOrAmagar
    /// </summary>
    /// <param name="component"></param>
    /// <param name="loop"></param>
    public void DisableAmbAnimacio(Component component, ref Coroutine loop, ref Coroutine idle, bool performDisable = true)
    {
        loop = CorrutineStop(loop);
        idle = CorrutineStop(idle);
        DisableAmbAnimacio(component, performDisable);
    }
    void DisableAmbAnimacio(Component component, bool performDisable)
    {
        if (onDestroyOrDisable) onDestroyOrDisable.Play(component);

        if (performDisable)
        {
            if (onDestroyOrDisable && onDestroyOrDisable.TeAnimacions) XS_Coroutine.StartCoroutine_Ending(onDestroyOrDisable.Temps, Disable);
            else Disable();
        }

        void Disable() => component.gameObject.SetActive(false);
    }





    Coroutine LoopPlayDelayed(Component component, float temps)
    {
        Coroutine corrutineLoop = XS_Coroutine.StartCoroutine_Ending(temps, LoopAfter);

        void LoopAfter() => loop.Play(component);
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
