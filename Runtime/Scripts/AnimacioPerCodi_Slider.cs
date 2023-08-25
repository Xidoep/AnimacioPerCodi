using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioSlider", fileName = "AnimacioSlider")]
public class AnimacioPerCodi_Slider : ScriptableObject
{
    [SerializeField, SerializeScriptableObject, HorizontalGroup("onEnter")] AnimacioPerCodi onEnter;
    [SerializeField, SerializeScriptableObject, HorizontalGroup("onDown")] AnimacioPerCodi onDown;
    [SerializeField, SerializeScriptableObject, HorizontalGroup("loop")] AnimacioPerCodi loop;
    [SerializeField, SerializeScriptableObject, HorizontalGroup("modificar")] AnimacioPerCodi modificar;
    [SerializeField, SerializeScriptableObject, HorizontalGroup("onUp")] AnimacioPerCodi onUp;
    [SerializeField, SerializeScriptableObject, HorizontalGroup("onExit")] AnimacioPerCodi onExit;

    public Coroutine OnEnter(Component component) => component.Animacio_LoopDespres(onEnter, loop);
    public Coroutine OnDown(Component component, Coroutine corrutine) => component.StopAnterior_Animacio(onDown, loop, corrutine);
    public void Modificar(Component component) => modificar.Play(component);
    public Coroutine OnUp(Component component) => component.Animacio_LoopDespres(onUp, loop);
    public Coroutine OnExit(Component component, Coroutine corrutine) => component.StopAnterior_Animacio(onExit, loop, corrutine);



    [HorizontalGroup("onEnter", width: 70), Button(Name = "Add"), ShowIf("@this.onEnter == null")]
    void AddEnter() => onEnter = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", this, onEnter);

    [HorizontalGroup("onEnter", width: 70), Button(Name = "Remove"), ShowIf("@this.onEnter != null")]
    void RemoveEnter() => onEnter = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", this, onEnter);



    [SerializeField, HorizontalGroup("onDown", width: 70), Button(Name = "Add"), ShowIf("@this.onDown == null")]
    void AddOnDown() => onDown = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDown", this, onDown);

    [SerializeField, HorizontalGroup("onDown", width: 70), Button(Name = "Remove"), ShowIf("@this.onDown != null")]
    void RemoveOnDown() => onDown = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDown", this, onDown);



}
