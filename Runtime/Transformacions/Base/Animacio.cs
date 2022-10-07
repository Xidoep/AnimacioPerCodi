using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void Play(GameObject gameObject, float temps, Transicio transicio)
    {
        if (lector == null) lector = gameObject.AddComponent<Lector>();
        SetupLector(temps, transicio);
    }
    public void Play(Image image, float temps, Transicio transicio)
    {
        if (lector == null) lector = image.gameObject.AddComponent<LectorImage>();
        SetupLector(temps, transicio);
    }
    public void Play(Text text, float temps, Transicio transicio)
    {
        if (lector == null) lector = text.gameObject.AddComponent<LectorText>();
        SetupLector(temps, transicio);
    }
    public void Play(SpriteRenderer spriteRenderer, float temps, Transicio transicio)
    {
        if (lector == null) lector = spriteRenderer.gameObject.AddComponent<LectorSpriteRenderer>();
        SetupLector(temps, transicio);
    }
    public void Play(TMP_Text text, float temps, Transicio transicio)
    {
        if (lector == null) lector = text.gameObject.AddComponent<LectorTMP_Text>();
        SetupLector(temps, transicio);
    }
    public void Play(Toggle toggle, float temps, Transicio transicio)
    {
        if (lector == null) lector = toggle.gameObject.AddComponent<LectorToggle>();
        SetupLector(temps, transicio);
    }




    void SetupLector(float temps, Transicio transicio) => lector.Setup(Transformar, temps, transicio).Play();

    public Lector Play_GetLector(GameObject gameObject, float temps, Transicio transicio)
    {
        Play(gameObject, temps, transicio);
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