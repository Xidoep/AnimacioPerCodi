using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimacioPerCodi_GameObject_Referencia : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum OnClickAction {res, destroy, disable }
    [SerializeField] AnimacioPerCodi_GameObject animacio;
    [SerializeField] OnClickAction onClick;

    Coroutine loop;
    Coroutine idle;



    void OnEnable() => animacio.PlayOnEnabled(transform, ref idle);
    public void OnPointerEnter(PointerEventData eventData) => animacio.PlayOnPointerEnter(transform, ref loop, ref idle);
    public void OnPointerDown(PointerEventData eventData) => animacio.PlayOnPointerDown(transform, ref loop);
    public void OnPointerUp(PointerEventData eventData) => animacio.PlayOnPointerUp(transform, ref loop, ref idle, onClick == OnClickAction.destroy, onClick == OnClickAction.disable);
    public void OnPointerExit(PointerEventData eventData) => animacio.PlayOnPointerExit(transform, ref loop, ref idle);


    [ContextMenu("Destroy")] public void Destroy() => animacio.DestroyAmbAnimacio(transform, ref loop, ref idle);
    [ContextMenu("Disable")] public void Disable() => animacio.DisableAmbAnimacio(transform, ref loop, ref idle);
}
