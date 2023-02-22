using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimacioPerCodi_GameObject_Referencia : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum OnClickAction {res, destroy, disable }

    [SerializeField] Component component;
    [SerializeField] AnimacioPerCodi_GameObject animacio;

    [Apartat("Interaccions")]
    [SerializeField] bool interactuable = false;
    [SerializeField] OnClickAction onClick;

    [Apartat("Pare")]
    [SerializeField] AnimacioPerCodi_GameObject_Referencia pare;
    [SerializeField] AnimacioPerCodi_Boto boto;
    bool registrat = false;

    //Coroutine idle;
    Coroutine coroutine;

    #region ACTIONS

    
    System.Action onPointerEnterAction;
    System.Action onPointerDownAction;
    System.Action onPointerUpAction;
    System.Action onPointerExitAction;
    System.Action onDestroyAction;
    System.Action onDisableAction;

    public System.Action OnPointerEnterAction { get => onPointerEnterAction; set => onPointerEnterAction = value; }
    public System.Action OnPointerDownAction { get => onPointerDownAction; set => onPointerDownAction += value; }
    public System.Action OnPointerUpAction { get => onPointerUpAction; set => onPointerUpAction += value; }
    public System.Action OnPointerExitAction { get => onPointerExitAction; set => onPointerExitAction += value; }
    public System.Action OnDestroyAction { get => onDestroyAction; set => onDestroyAction += value; }
    public System.Action OnDisableAction { get => onDisableAction; set => onDisableAction += value; }
    #endregion

    void OnEnable() 
    {
        if (animacio) coroutine = animacio.OnEnabled(component);

        if (!pare)
            return;

        if (registrat)
            return;

        registrat = true;

        pare.OnPointerEnterAction += PointerEnter;
        pare.OnPointerDownAction += PointerDown;
        pare.OnPointerUpAction += PointerUp;
        pare.OnPointerExitAction += PointerExit;
        pare.OnDestroyAction += Destroy;
        pare.OnDisableAction += Disable;
        
    }


    public void OnPointerEnter(PointerEventData eventData) 
    {
        if (!interactuable)
            return;

        if (pare) 
            return;

        PointerEnter();
    }
    public void OnPointerDown(PointerEventData eventData) 
    {
        if (!interactuable)
            return;

        if (pare) 
            return;

        PointerDown();
    }
    public void OnPointerUp(PointerEventData eventData) 
    {
        if (!interactuable)
            return;

        if (pare) 
            return;

        PointerUp();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactuable)
            return;

        if (pare) 
            return;

        PointerExit();
    }



    public void PointerEnter() 
    {
        if (animacio) coroutine = animacio.OnPointerEnter(component, coroutine);
        onPointerEnterAction?.Invoke();
    }
    public void PointerDown() 
    {
        if (animacio) coroutine = animacio.OnPointerDown(component, coroutine);
        onPointerDownAction?.Invoke();
    }
    public void PointerUp()
    {
        if (animacio) coroutine = animacio.OnPointerUp(component, coroutine, onClick == OnClickAction.destroy, onClick == OnClickAction.disable);
        onPointerUpAction?.Invoke();
    }
    public void PointerExit() 
    {
        if (animacio) coroutine = animacio.OnPointerExit(component, coroutine);
        onPointerExitAction?.Invoke();
    }


    [ContextMenu("Destroy")] 
    public void Destroy() 
    {
        if (animacio) animacio.Destroy(component, ref coroutine, pare ? false : onClick == OnClickAction.destroy);
        onDestroyAction?.Invoke();
    }
    [ContextMenu("Disable")] 
    public void Disable() 
    {
        if (animacio) animacio.Disable(component, ref coroutine, pare ? false : onClick == OnClickAction.disable);
        onDisableAction?.Invoke();
    }

    private void OnDisable()
    {
        if (!pare)
            return;

        
    }

    void OnDestroy()
    {
        if (!registrat)
            return;

        pare.OnPointerEnterAction -= PointerEnter;
        pare.OnPointerDownAction -= PointerDown;
        pare.OnPointerUpAction -= PointerUp;
        pare.OnPointerExitAction -= PointerExit;
        pare.OnDestroyAction -= Destroy;
        pare.OnDisableAction -= Disable;
    }
}
