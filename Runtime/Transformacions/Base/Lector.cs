using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using XS_Utils;


public class Lector : MonoBehaviour
{
    public virtual Lector Setup(System.Action<Component, float> animar, Component component, float temps, Transicio transicio)
    {
        this.animar = animar;
        this.temps = temps;
        SetTransicio(transicio);

        return this;
    }
    public void Add(System.Action<Component, float> animar) => this.animar += animar;
    public Lector Setup(Animacio[] animacions, float temps, Transicio transicio)
    {
        animar = null;
        for (int i = 0; i < animacions.Length; i++)
        {
            animar += animacions[i].Transformar;
        }
        this.temps = temps;
        SetTransicio(transicio);

        return this;
    }
    void SetTransicio(Transicio transicio)
    {
        switch (transicio)
        {
            case Transicio.clamp:
                pingPong = false;
                looping = false;
                invertit = false;
                break;
            case Transicio.pingpong:
                pingPong = true;
                looping = false;
                invertit = false;
                break;
            case Transicio.loop:
                pingPong = false;
                looping = true;
                invertit = false;
                break;
            case Transicio.loopPingpong:
                pingPong = true;
                looping = true;
                invertit = false;
                break;
            case Transicio.invertit:
                pingPong = false;
                looping = false;
                invertit = true;
                break;
        }
    }



    protected System.Action<Component, float> animar = null;

    float temps;
    bool pingPong;
    bool looping;
    bool invertit;
    float time;
    bool finalitzat;
    bool finalitzarAlFinalAnimacio;

    float TempsDesdePlay => Mathf.Clamp01((Time.unscaledTime - time) / temps);


  
    public void Play()
    {
        time = Time.unscaledTime;
        if (!gameObject.activeSelf)
        {
            Debugar.Log("No està activat el game object...");
            return;
        }
        StartCoroutine(ActualitzarCorrutina());
    }
    public void Stop(bool esperarFinalAnimacio)
    {
        if (esperarFinalAnimacio)
            finalitzarAlFinalAnimacio = true;
        else finalitzat = false;
    }
    public void Continue()
    {
        StartCoroutine(ActualitzarCorrutina());
    }


    protected virtual void Animar(float frame) => animar.Invoke(transform, frame);
    IEnumerator ActualitzarCorrutina()
    {
        finalitzat = true;
        while (finalitzat)
        {
            finalitzat = Actualitzar();
            yield return null;
        }
        animar = null;
        yield return null;
    }
    bool Actualitzar()
    {
        if (!pingPong)
        {
            if (!invertit) Animar(TempsDesdePlay);
            else Animar(1 - TempsDesdePlay);
        }
        else
        {
            if (TempsDesdePlay < 0.5f)
                Animar(TempsDesdePlay * 2);
            else Animar(2 - (TempsDesdePlay * 2));
        }

        if (TempsDesdePlay >= 1)
        {
            if (finalitzarAlFinalAnimacio)
            {
                finalitzarAlFinalAnimacio = false;
                finalitzat = true;
                if (!pingPong)
                {
                    if (!invertit)
                        Animar(TempsDesdePlay);
                    else Animar(1 - TempsDesdePlay);
                }
                else
                {
                    if (TempsDesdePlay < 0.5f)
                        Animar(TempsDesdePlay * 2);
                    else Animar(2 - (TempsDesdePlay * 2));
                }
            }
            else
            {
                if (looping)
                {
                    time = Time.unscaledTime;
                }
            }
        }
        return TempsDesdePlay < 1;
    }

}


public class LectorComponent : Lector
{
    [SerializeField] Component component;

    public override Lector Setup(System.Action<Component, float> animar, Component component, float temps, Transicio transicio)
    {
        //component = gameObject.GetComponent<Component>();
        this.component = component;
        return base.Setup(animar, component, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(component, frame);
}
/*
public class LectorRectTarnsform : Lector
{
    RectTransform rectTransform;

    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(rectTransform, frame);
}

public class LectorImage : Lector
{
    Image image;
    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        image = gameObject.GetComponent<Image>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(image, frame);
}

public class LectorText : Lector
{
    Text text;
    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        text = gameObject.GetComponent<Text>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(text, frame);
}

public class LectorSpriteRenderer : Lector
{
    SpriteRenderer spriteRenderer;
    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(spriteRenderer, frame);
}

public class LectorTMP_Text : Lector
{
    TMP_Text text;
    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        text = gameObject.GetComponent<TMP_Text>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(text, frame);
}

public class LectorToggle : Lector
{
    Toggle toggle;
    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        toggle = gameObject.GetComponent<Toggle>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(toggle, frame);
}

public class LectorMeshRenderer : Lector
{
    MeshRenderer meshRenderer;
    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(meshRenderer, frame);
}

public class LectorRectTransform : Lector
{
    RectTransform rectTransform;
    public override Lector Setup(System.Action<Component, float> animar, float temps, Transicio transicio)
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        return base.Setup(animar, temps, transicio);
    }
    protected override void Animar(float frame) => animar.Invoke(rectTransform, frame);
}
*/