using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimacioPerCodi_Button : AnimacioPerCodi_Base, IPointerEnterHandler, IPointerExitHandler, ICancelHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    [System.Serializable]
    public struct Animacio
    {
        public AnimacioPerCodi_Base.Transicio_Tipus transicio;
        public float temps;
        public Transformacions[] transformacions;
    }

    [SerializeField] Animacio onSelect;
    [SerializeField] Animacio loopSelected;
    [SerializeField] Animacio onDeselect;
    [SerializeField] Animacio onClick;


    WaitForSecondsRealtime waitForSeconds;
    Coroutine coroutine;


    Transformacions[] transformacionsSeleccionades;
    internal override Transformacions[] GetTransformacions { get => transformacionsSeleccionades; set => transformacionsSeleccionades = value; }

    void Play(Animacio animacio)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        transformacionsSeleccionades = animacio.transformacions;
        Play(animacio.temps, animacio.transicio);
    }
    public void OnClick() => Play(onClick);


    //NAVIGATION

    public void OnSelect(BaseEventData eventData) 
    {
        Play(onSelect);
        coroutine = PlayDelayed(GetTemps(), loopSelected);
    } 
    public void OnDeselect(BaseEventData eventData) 
    {
        Play(onDeselect);
    } 
    public void OnSubmit(BaseEventData eventData) 
    {
        Play(onClick);
        coroutine = PlayDelayed(GetTemps(), loopSelected);
    }
    //POINTER
    public void OnPointerEnter(PointerEventData eventData) 
    {
        Play(onSelect);
        coroutine = PlayDelayed(GetTemps(), loopSelected);
    } 
    public void OnPointerExit(PointerEventData eventData) 
    {
        Play(onDeselect);
    }
    public void OnPointerClick(PointerEventData eventData) 
    {
        
        Play(onClick);
        coroutine = PlayDelayed(GetTemps(), loopSelected);
    } 

    //GENERIC
    public void OnCancel(BaseEventData eventData) 
    {
        Play(onDeselect);
        if (coroutine != null) StopCoroutine(coroutine);
    } 




    Coroutine PlayDelayed(float delay, Animacio animacio)
    {
        waitForSeconds = new WaitForSecondsRealtime(delay);
        return StartCoroutine(PlayDelayed(animacio));
    }
    IEnumerator PlayDelayed(Animacio animacio)
    {
        yield return waitForSeconds;
        Play(animacio);
    }
}
