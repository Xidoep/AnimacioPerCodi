using UnityEngine;
using UnityEngine.UI;
using TMPro;

using XS_Utils;

[System.Serializable]
public class Animacio
{
    protected Lector lector;

    public virtual void Transformar(Component component, float frame) { }
    public virtual void Restore() { }

    //Pensat només per ser cridat quan es vol animar directament desde Script.
    public void Play(Component component, float temps, Transicio transicio, bool unescaled)
    {
        if (lector)
            lector.Setup(Transformar, component, temps, transicio, unescaled).Play();
        else component.gameObject.AddComponent<Lector>().Setup(Transformar,component, temps, transicio, unescaled).Play();
    }
    public void Play(Component component, float temps, float delay, Transicio transicio, bool unescaled)
    {
        if (lector)
            lector.Setup(Transformar, component, temps, delay, transicio, unescaled).Play();
        else component.gameObject.AddComponent<Lector>().Setup(Transformar, component, temps, delay, transicio, unescaled).Play();
    }

    public void Continue(Transform transform) => transform.GetComponent<Lector>().Continue();
    public void Continue() => lector.Continue();
    public void Stop(Transform transform) => transform.GetComponent<Lector>().Stop(false);
    public void Stop() => lector.Stop(false);
    public void Stop_OnAnimationEnding(Transform transform) => transform.GetComponent<Lector>().Stop(true);
    public void Stop_OnAnimationEnding() => lector.Stop(true);

    
}

public enum Transicio { clamp, loop, pingpong, loopPingpong, invertit }

public static class Corba
{
    public static AnimationCurve Linear => new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });
    public static AnimationCurve EasyInEasyOut => new AnimationCurve(new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 1, 1) });
}