using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioSlider", fileName = "AnimacioSlider")]
public class AnimacioPerCodi_Slider : ScriptableObject
{
    public AnimacioPerCodi onEnter;
    public AnimacioPerCodi onDown;
    public AnimacioPerCodi modificar;
    public AnimacioPerCodi onUp;
    public AnimacioPerCodi onExit;

    public void PlayOnEnter(Component component)
    {
        onEnter?.Play(component);
    }
    public void PlayOnDown(Component component)
    {
        onDown?.Play(component);
    }
    public void PlayModificar(Component component)
    {
        modificar?.Play(component);
    }
    public void PlayOnUp(Component component)
    {
        onUp?.Play(component);
    }
    public void PlayOnExit(Component component)
    {
        onExit?.Play(component);
    }
}
