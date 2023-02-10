using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class AnimacioPerCodi_GameObject : ScriptableObject
{




    [ContextMenuItem("AddPosicio", "AddPosicio")]
    [SerializeField] AnimacioPerCodi.Interaccio onEnabled;
    [SerializeField] AnimacioPerCodi.Interaccio onPointerEnter;
    [SerializeField] AnimacioPerCodi.Interaccio loop;
    [SerializeField] AnimacioPerCodi.Interaccio onPointerDown;
    [SerializeField] AnimacioPerCodi.Interaccio onPointerUp;
    [SerializeField] AnimacioPerCodi.Interaccio onPointerExit;
    [SerializeField] AnimacioPerCodi.Interaccio onDestroyOrDisable;

    public AnimacioPerCodi.Interaccio OnEnabled => onEnabled;
    public AnimacioPerCodi.Interaccio OnPointerEnter => onPointerEnter;
    public AnimacioPerCodi.Interaccio Loop => loop;
    public AnimacioPerCodi.Interaccio OnPointerDown => onPointerDown;
    public AnimacioPerCodi.Interaccio OnPointerUp => onPointerUp;
    public AnimacioPerCodi.Interaccio OnPointerExit => onPointerExit;
    public AnimacioPerCodi.Interaccio OnDestroyOrDisable => onDestroyOrDisable;

    Lector lector;


    private void OnEnable()
    {
        lector = null;
    }

    //En enable, ha de fer play enable.
    //En PointerEnter, ha de posar animacio PointerEnter, i fer animacio Hover amb delay o no depenent de si te animacio PointerEnter.
    //On pointer down(si hi es), atura animacio Hover(si hi es) i posa animacio PointerDown.
    //On Pointer up(si hi es), (si no hi ha pointerDown, atura animacio Hover(si hi es)) posa animacio pointer up i tria:
    //  -si DestroyOnClick: animacio de Destroy/Dissable amb delay de la anim de click
    //  -si no: Posar animacio de Hover amb delay d'animacio de click.

    /// <summary>
    /// Play a animacio apareixre
    /// </summary>
    /// <param name="component"></param>
    public void PlayOnEnabled(Component component) => onEnabled.Play(component, Transicio.clamp, ref lector);

    /// <summary>
    /// Play a animacio en Apuntar, i passa a la animacio Apuntat despres.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="corrutineLoop"></param>
    public void PlayOnPointerEnter(Component component, ref Coroutine corrutineLoop)
    {
        onPointerEnter.Play(component, Transicio.clamp, ref lector);

        if (onPointerEnter.TeAnimacions)
            corrutineLoop = LoopPlayDelayed(component, onPointerEnter.Temps);
        else loop.Play(component, Transicio.loop, ref lector);
    }

    /// <summary>
    /// Atura l'animacio Apuntat i fa Play animacio Clicar. Si no hi ha l'animacio Desclicar, torna a l'animacio Apuntat. 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="corrutineLoop"></param>
    public void PlayOnPointerDown(Component component, ref Coroutine corrutineLoop)
    {
        corrutineLoop = LoopStop(corrutineLoop);
        onPointerDown.Play(component, Transicio.clamp, ref lector);

        if (!onPointerUp.TeAnimacions)
        {
            if (onPointerDown.TeAnimacions)
                corrutineLoop = LoopPlayDelayed(component, onPointerDown.Temps);
            else loop.Play(component, Transicio.loop, ref lector);
        }
    }

    /// <summary>
    /// Atura l'animacio Apuntat si es que PlayOnPointerDown no ho ha fet. fa Play a animacio Desclicar i fa el seguent.
    /// Destrueix i amaga el gameObjecte del component si l'hi dius.
    /// o passa a l'animacio Apuntat, si no.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="corrutineLoop"></param>
    /// <param name="destroy"></param>
    /// <param name="disable"></param>
    public void PlayOnPointerUp(Component component, ref Coroutine corrutineLoop, bool destroy, bool disable)
    {
        corrutineLoop = LoopStop(corrutineLoop);

        onPointerUp.Play(component, Transicio.clamp, ref lector);

        if (destroy || disable)
        {
            if (destroy)
            {
                DestroyAmbAnimacio(component, ref corrutineLoop);
            }
            else
            {
                corrutineLoop = LoopStop(corrutineLoop);
                if (onPointerUp.TeAnimacions) 
                    XS_Coroutine.StartCoroutine_Ending(onPointerUp.Temps, Disable);
                else Disable();
            }
        }
        else
        {
            if (!onPointerUp.TeAnimacions)
                corrutineLoop = LoopPlayDelayed(component, onPointerUp.Temps);
            else loop.Play(component, Transicio.loop, ref lector);
        }

        void Disable() => DisableAmbAnimacio(component);
    }

    /// <summary>
    /// Atura l'animacio Apuntat, i fa Play a l'animacio Desapuntar.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="corrutineLoop"></param>
    public void PlayOnPointerExit(Component component, ref Coroutine corrutineLoop)
    {
        corrutineLoop = LoopStop(corrutineLoop);
        onPointerExit.Play(component, Transicio.clamp, ref lector);
    }

    /// <summary>
    /// Destrueix el gameObject del component despres de l'animacio de DestruirOrAmagar
    /// </summary>
    /// <param name="component"></param>
    /// <param name="corrutineLoop"></param>
    public void DestroyAmbAnimacio(Component component, ref Coroutine corrutineLoop)
    {
        corrutineLoop = LoopStop(corrutineLoop);
        DestroyAmbAnimacio(component);
    }
    void DestroyAmbAnimacio(Component component)
    {
        onDestroyOrDisable.Play(component, Transicio.clamp, ref lector);
        Destroy(component.gameObject, onDestroyOrDisable.TeAnimacions ? onDestroyOrDisable.Temps : 0);
    }

    /// <summary>
    /// Amaga el gameObject del component despres de l'animacio de DestruirOrAmagar
    /// </summary>
    /// <param name="component"></param>
    /// <param name="corrutineLoop"></param>
    public void DisableAmbAnimacio(Component component, ref Coroutine corrutineLoop)
    {
        corrutineLoop = LoopStop(corrutineLoop);
        DisableAmbAnimacio(component);
    }
    void DisableAmbAnimacio(Component component)
    {
        onDestroyOrDisable.Play(component, Transicio.clamp, ref lector);

        if (onDestroyOrDisable.TeAnimacions) XS_Coroutine.StartCoroutine_Ending(onDestroyOrDisable.Temps, Disable);
        else Disable();

        void Disable() => component.gameObject.SetActive(false);
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
