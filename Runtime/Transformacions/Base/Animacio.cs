using UnityEngine;

[System.Serializable]
public class Animacio
{
    protected Lector lector;

    protected virtual void AddOrGet<T>(Transform transform) 
    {
        lector = (Lector)transform.GetComponent(typeof(T));
        if (lector == null) lector = (Lector)transform.gameObject.AddComponent(typeof(T)); ;
    }
    public virtual void Transformar(object objectiu, float frame) { }

    public void Play(Transform transform, float temps, Transicio transicio)
    {
        if (lector == null)
            transform.gameObject.AddComponent<Lector>().Setup(Transformar, temps, transicio).Play();
        else lector.Setup(Transformar, temps, transicio).Play();
    }

    public Lector Play_GetLector(Transform transform, float temps, Transicio transicio)
    {
        Play(transform, temps, transicio);
        return lector;
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
    public static AnimationCurve Linear() => new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });
}