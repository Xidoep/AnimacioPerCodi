using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioBoto", fileName = "AnimacioBoto")]
public class AnimacioPerCodi_Boto : ScriptableObject
{
    public AnimacioPerCodi onClick;
    public AnimacioPerCodi onEnter;
    public AnimacioPerCodi onExit;
    public AnimacioPerCodi loop;


    public Coroutine OnEnter(Component component) => component.Animacio_LoopDespres(onEnter, loop);
    public Coroutine OnClick(Component component, Coroutine corrutine) => component.StopAnterior_Animacio_LoopDespres(onClick, loop, corrutine, loop);
    public Coroutine OnExit(Component component, Coroutine corrutine) => component.StopAnterior_Animacio(onExit, loop, corrutine);

}