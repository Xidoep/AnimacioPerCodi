using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioSlider", fileName = "AnimacioSlider")]
public class AnimacioPerCodi_Slider : ScriptableObject
{
    [SerializeField] AnimacioPerCodi onEnter;
    [SerializeField] AnimacioPerCodi onDown;
    [SerializeField] AnimacioPerCodi loop;
    [SerializeField] AnimacioPerCodi modificar;
    [SerializeField] AnimacioPerCodi onUp;
    [SerializeField] AnimacioPerCodi onExit;

    public Coroutine OnEnter(Component component) => component.Animacio_LoopDespres(onEnter, loop);
    public Coroutine OnDown(Component component, Coroutine corrutine) => component.StopAnterior_Animacio(onDown, loop, corrutine);
    public void Modificar(Component component) => modificar.Play(component);
    public Coroutine OnUp(Component component) => component.Animacio_LoopDespres(onUp, loop);
    public Coroutine OnExit(Component component, Coroutine corrutine) => component.StopAnterior_Animacio(onExit, loop, corrutine);
}
