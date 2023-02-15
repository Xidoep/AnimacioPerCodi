using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimacioPerCodi_GameObject_Referencia : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum OnClickAction {res, destroy, disable }

    [SerializeField] Component component;
    [SerializeField] OnClickAction onClick;
    [SerializeField] AnimacioPerCodi_GameObject animacio;
    [Space(10)]
    [SerializeField] AnimacioPerCodi_GameObject_Referencia pare;

    Coroutine loop;
    Coroutine idle;

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
        if (animacio) animacio.PlayOnEnabled(component, ref idle);

        if (!pare)
            return;

        pare.OnPointerEnterAction += PointerEnter;
        pare.OnPointerDownAction += PointerDown;
        pare.OnPointerUpAction += PointerUp;
        pare.OnPointerExitAction += PointerExit;
        pare.OnDestroyAction += Destroy;
        pare.OnDisableAction += Disable;
    }


    public void OnPointerEnter(PointerEventData eventData) 
    {
      
        if (pare) 
            return;

        PointerEnter();
    }
    public void OnPointerDown(PointerEventData eventData) 
    {
        if (pare) 
            return;

        PointerDown();
    }
    public void OnPointerUp(PointerEventData eventData) 
    {
        if (pare) 
            return;

        PointerUp();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (pare) 
            return;

        PointerExit();
    }



    public void PointerEnter() 
    {
        Debug.LogError("Enter", this.gameObject);
        if (animacio) animacio.PlayOnPointerEnter(component, ref loop, ref idle);
        onPointerEnterAction?.Invoke();
    }
    public void PointerDown() 
    {
        if (animacio) animacio.PlayOnPointerDown(component, ref loop);
        onPointerDownAction?.Invoke();
    }
    public void PointerUp()
    {
        if (animacio) animacio.PlayOnPointerUp(component, ref loop, ref idle, onClick == OnClickAction.destroy, onClick == OnClickAction.disable);
        onPointerUpAction?.Invoke();
    }
    public void PointerExit() 
    {
        if (animacio) animacio.PlayOnPointerExit(component, ref loop, ref idle);
        onPointerExitAction?.Invoke();
    }


    [ContextMenu("Destroy")] 
    public void Destroy() 
    {
        if (animacio) animacio.DestroyAmbAnimacio(component, ref loop, ref idle, pare ? false : onClick == OnClickAction.destroy);
        onDestroyAction?.Invoke();
    }
    [ContextMenu("Disable")] 
    public void Disable() 
    {
        if (animacio) animacio.DisableAmbAnimacio(component, ref loop, ref idle, pare ? false : onClick == OnClickAction.disable);
        onDisableAction?.Invoke();
    }

    private void OnDisable()
    {
        if (!pare)
            return;

        pare.OnPointerEnterAction -= PointerEnter;
        pare.OnPointerDownAction -= PointerDown;
        pare.OnPointerUpAction -= PointerUp;
        pare.OnPointerExitAction -= PointerExit;
        pare.OnDestroyAction -= Destroy;
        pare.OnDisableAction -= Disable;
    }
}
