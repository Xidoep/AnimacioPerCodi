using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioBoto", fileName = "AnimacioBoto")]
public class AnimacioPerCodi_Boto : ScriptableObject
{
    [ContextMenuItem("AddPosicio", "AddPosicio")]
    [SerializeField] AnimacioPerCodi.Interaccio onClick;
    [SerializeField] AnimacioPerCodi.Interaccio onEnter;
    [SerializeField] AnimacioPerCodi.Interaccio onExit;
    [SerializeField] AnimacioPerCodi.Interaccio loop;

    public AnimacioPerCodi.Interaccio OnClick => onClick;
    public AnimacioPerCodi.Interaccio OnEnter => onEnter;
    public AnimacioPerCodi.Interaccio OnExit => onExit;
    public AnimacioPerCodi.Interaccio Loop => loop;

    Lector lector;



    public void PlayOnClick(Component component) => onClick.Play(component, Transicio.clamp, ref lector);
    public void PlayOnEnter(Component component) => onEnter.Play(component, Transicio.clamp, ref lector);
    public void PlayOnExit(Component component) => onExit.Play(component, Transicio.clamp, ref lector);
    public void PlayLoop(Component component) => loop.Play(component, Transicio.loop, ref lector);








}
