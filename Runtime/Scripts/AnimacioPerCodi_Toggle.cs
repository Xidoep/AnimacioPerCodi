using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioToggle", fileName = "AnimacioToggle")]
public class AnimacioPerCodi_Toggle : ScriptableObject
{
    [SerializeScriptableObject][SerializeField] AnimacioPerCodi onEnter;
    [SerializeScriptableObject][SerializeField] AnimacioPerCodi loop;
    [SerializeScriptableObject][SerializeField] AnimacioPerCodi onClick;
    [SerializeScriptableObject][SerializeField] AnimacioPerCodi onExit;

    public Coroutine OnEnter(Component component) => component.Animacio_LoopDespres(onEnter, loop);
    public Coroutine OnClick(Component component, Coroutine corrutine) => component.StopAnterior_Animacio_LoopDespres(onClick, loop, corrutine, loop);
    public Coroutine OnExit(Component component, Coroutine corrutine) => component.StopAnterior_Animacio(onExit, loop, corrutine);

}
