using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioToggle", fileName = "AnimacioToggle")]
public class AnimacioPerCodi_Toggle : ScriptableObject
{
    [SerializeField] AnimacioPerCodi onEnter;
    [SerializeField] AnimacioPerCodi loop;
    [SerializeField] AnimacioPerCodi onClick;
    [SerializeField] AnimacioPerCodi onExit;

    public Coroutine OnEnter(Component component) => component.Animacio_LoopDespres(onEnter, loop);
    public Coroutine OnClick(Component component, Coroutine corrutine) => component.StopAnterior_Animacio_LoopDespres(onClick, loop, corrutine, loop);
    public Coroutine OnExit(Component component, Coroutine corrutine) => component.StopAnterior_Animacio(onExit, loop, corrutine);

}
