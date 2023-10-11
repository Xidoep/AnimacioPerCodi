using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XS_Utils;

public class Lector : MonoBehaviour
{
    public virtual Lector Setup(System.Action<Component, float> animar, Component component, float temps, Transicio transicio, bool unscaled)
    {
        this.component = component;
        this.animacions = animar;
        this.temps = temps;
        this.unscaled = unscaled;
        SetTransicio(transicio);

        return this;
    }
    public virtual Lector Setup(System.Action<Component, float> animar, Component component, float temps, float delay, Transicio transicio, bool unscaled)
    {
        this.component = component;
        this.animacions = animar;
        this.temps = temps;
        this.delay = delay;
        this.unscaled = unscaled;
        SetTransicio(transicio);

        return this;
    }
    //public void Add(System.Action<Component, float> animar) => this.animar += animar;
    public virtual Lector Setup(Animacio[] animacions, Component component, float temps, Transicio transicio, bool unscaled)
    {
        if(coroutine != null) StopCoroutine(coroutine);

        //Debug.LogError($"3.- Setup {animacions.Length} animacions");
        this.component = component;
        this.animacions = null;
        for (int i = 0; i < animacions.Length; i++)
        {
            this.animacions += animacions[i].Transformar;
        }
        this.temps = temps;
        this.unscaled = unscaled;
        SetTransicio(transicio);

        return this;
    }
    public virtual Lector Setup(Animacio[] animacions, Component component, float temps, float delay, Transicio transicio, bool unscaled)
    {
        if (coroutine != null) StopCoroutine(coroutine);

        //Debug.LogError($"3.- Setup {animacions.Length} animacions");
        this.component = component;
        this.animacions = null;
        for (int i = 0; i < animacions.Length; i++)
        {
            this.animacions += animacions[i].Transformar;
        }
        this.temps = temps;
        this.delay = delay;
        this.unscaled = unscaled;
        SetTransicio(transicio);

        return this;
    }
    public virtual Lector Setup(AnimacioPerCodi animacioPerCodi, Component component, float temps, Transicio transicio, bool unscaled)
    {
        if (coroutine != null) StopCoroutine(coroutine);

        //Debug.LogError($"3.- Setup {animacions.Length} animacions");
        this.component = component;
        this.animacions = null;
        for (int i = 0; i < animacioPerCodi.Animacions.Length; i++)
        {
            this.animacions += animacioPerCodi.Animacions[i].Transformar;
        }
        this.temps = temps;
        this.unscaled = unscaled;
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


    [SerializeField] Component component;
    protected System.Action<Component, float> animacions = null;

    float temps;
    float delay = 0;
    bool unscaled;
    bool pingPong;
    bool looping;
    bool invertit;
    float time;
    [SerializeField] bool animar;
    bool finalitzarAlFinalAnimacio;

    Coroutine coroutine;

    float TempsDesdePlay => Mathf.Clamp01((Time.unscaledTime - time - delay) / temps);


  
    public void Play()
    {
        time = Time.unscaledTime;
        if (!gameObject.activeSelf)
        {
            Debugar.Log("No està activat el game object...");
            return;
        }
        coroutine = StartCoroutine(ActualitzarCorrutina());
    }
    public void Stop(bool esperarFinalAnimacio = false)
    {
        if (esperarFinalAnimacio)
            finalitzarAlFinalAnimacio = true;
        else 
        {
            animar = false;
            animacions = null;
        } 
    }
    public void Continue()
    {
        coroutine = StartCoroutine(ActualitzarCorrutina());
    }


    protected virtual void Animar(float frame) => animacions.Invoke(component, frame);
    IEnumerator ActualitzarCorrutina()
    {
        animar = true;
        while (animar)
        {
            animar = Actualitzar();
            yield return null;
        }
        yield return null;
    }
    bool Actualitzar()
    {
        if (!animar)
            return false;

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
                animar = true;
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
                    if (unscaled)
                        time = Time.unscaledTime;
                    else time = Time.time;
                }
            }
        }
        return TempsDesdePlay < 1;
    }

}
