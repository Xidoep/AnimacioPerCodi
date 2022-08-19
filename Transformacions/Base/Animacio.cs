using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animacio : ScriptableObject
{
    public enum Transicio { clamp, loop, pingpong, loopPingpong, invertit }
    [SerializeField] protected Transicio transicio;

    [SerializeField] protected float temps = 1;
    [SerializeField] protected AnimationCurve corba = new AnimationCurve();

    //INTERN
    protected Transform transform;
    protected Animacio_Lector lector;



    public void Stop() => lector.Stop(false);
    public void Stop_OnAnimationEnding() => lector.Stop(true);
}
