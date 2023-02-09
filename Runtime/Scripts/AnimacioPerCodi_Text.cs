using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioText", fileName = "AnimacioText")]
public class AnimacioPerCodi_Text : ScriptableObject
{
    [SerializeField] AnimacioPerCodi.Interaccio onEnter;
    [SerializeField] AnimacioPerCodi.Interaccio onExit;

    public AnimacioPerCodi.Interaccio OnEnter => onEnter;
    public AnimacioPerCodi.Interaccio OnExit => onExit;

    Lector lector;

    public void PlayOnEnter(Component component) => onEnter.Play(component, Transicio.clamp, ref lector);
    public void PlayOnExit(Component component) => onExit.Play(component, Transicio.clamp, ref lector);

}
