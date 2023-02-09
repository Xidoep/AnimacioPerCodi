using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class AnimacioPerCodi_GameObject : MonoBehaviour
{




    [ContextMenuItem("AddPosicio", "AddPosicio")]
    [SerializeField] AnimacioPerCodi.Interaccio onEnabled;
    [SerializeField] AnimacioPerCodi.Interaccio enabledLoop;
    [SerializeField] AnimacioPerCodi.Interaccio onClick;
    [SerializeField] AnimacioPerCodi.Interaccio onPointerEnter;
    [SerializeField] AnimacioPerCodi.Interaccio onPointerExit;
    [SerializeField] AnimacioPerCodi.Interaccio onDestroyOrDisable;

    public AnimacioPerCodi.Interaccio OnEnabled => onEnabled;
    public AnimacioPerCodi.Interaccio OnClick => onClick;
    public AnimacioPerCodi.Interaccio OnEnter => onPointerEnter;
    public AnimacioPerCodi.Interaccio OnExit => onPointerExit;
    public AnimacioPerCodi.Interaccio OnDestroyOrDisable => onDestroyOrDisable;
    public AnimacioPerCodi.Interaccio EnabledLoop => enabledLoop;

    Lector lector;

    Coroutine coroutineLoop;
    WaitForSeconds waitForSeconds;

    private void OnEnable()
    {
        if (onEnabled != null)
            waitForSeconds = new WaitForSeconds(onEnabled.Temps);

    }

    public void PlayEnEnable(Component component)
    {
        if (onEnabled != null)
        {
            onEnabled.Play(component, Transicio.clamp, ref lector);
            coroutineLoop = XS_Coroutine.StartCoroutine(StartEnabledLoop(component));
        }
        else
        {
            enabledLoop.Play(component, Transicio.loop, ref lector);
        }
    }
    IEnumerator StartEnabledLoop(Component component)
    {
        yield return waitForSeconds;
        enabledLoop.Play(component, Transicio.loop, ref lector);
    }
    public void PlayOnClick(Component component) => onClick.Play(component, Transicio.clamp, ref lector);
    public void PlayOnEnter(Component component)
    {
        onPointerEnter.Play(component, Transicio.clamp, ref lector);
        if (coroutineLoop != null)
        {
            XS_Coroutine.StopCoroutine(coroutineLoop);
            coroutineLoop = null;
        }
    }
    public void PlayOnExit(Component component)
    {
        onPointerExit.Play(component, Transicio.clamp, ref lector);
    }

    public void Destroy(Component component)
    {

    }
    public void Disable(Component component)
    {

    }
}
