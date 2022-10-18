using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Animacio
{
    protected Lector lector;



    public virtual void Transformar(object objectiu, float frame) { }



    public void Play(GameObject gameObject, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != gameObject) lector = gameObject.AddComponent<Lector>();
        SetupLector(lector, temps, transicio);
    }
    public void Play(Transform transform, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != transform.gameObject) lector = transform.gameObject.AddComponent<Lector>();
        SetupLector(lector, temps, transicio);
    }
    public void Play(Image image, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != image.gameObject) lector = image.gameObject.AddComponent<LectorImage>();
        SetupLector(lector, temps, transicio);
    }
    public void Play(Text text, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != text.gameObject) lector = text.gameObject.AddComponent<LectorText>();
        SetupLector(lector, temps, transicio);
    }
    public void Play(SpriteRenderer spriteRenderer, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != spriteRenderer.gameObject) lector = spriteRenderer.gameObject.AddComponent<LectorSpriteRenderer>();
        SetupLector(lector, temps, transicio);
    }
    public void Play(TMP_Text text, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != text.gameObject) lector = text.gameObject.AddComponent<LectorTMP_Text>();
        SetupLector(lector, temps, transicio);
    }
    public void Play(Toggle toggle, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != toggle.gameObject) lector = toggle.gameObject.AddComponent<LectorToggle>();
        SetupLector(lector, temps, transicio);
    }
    public void Play(MeshRenderer meshRenderer, float temps, Transicio transicio)
    {
        if (lector == null || lector.gameObject != meshRenderer.gameObject) lector = meshRenderer.gameObject.AddComponent<LectorMeshRenderer>();
        SetupLector(lector, temps, transicio);
    }


    void SetupLector(Lector lector, float temps, Transicio transicio) => lector.Setup(Transformar, temps, transicio).Play();



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