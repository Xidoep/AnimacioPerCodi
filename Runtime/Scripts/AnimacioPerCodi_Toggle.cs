using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioToggle", fileName = "AnimacioToggle")]
public class AnimacioPerCodi_Toggle : ScriptableObject
{
    public AnimacioPerCodi onEnter;
    public AnimacioPerCodi onClick;
    public AnimacioPerCodi onExit;

    public void PlayOnEnter(Component component)
    {
        onEnter?.Play(component);
    }
    public void PlayOnClick(Component component)
    {
        onClick?.Play(component);
    }
    public void PlayOnExit(Component component)
    {
        onExit?.Play(component);
    }
}
