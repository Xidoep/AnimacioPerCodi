using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioText", fileName = "AnimacioText")]
public class AnimacioPerCodi_Text : ScriptableObject
{
    public AnimacioPerCodi onEnter;
    public AnimacioPerCodi onExit;

    public void PlayOnEnter(Component component) => onEnter.Play(component);
    public void PlayOnExit(Component component) => onExit.Play(component);

}
