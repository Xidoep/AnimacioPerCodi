using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LectorTransform : Lector
{
    protected override void Animar(float frame) => animar.Invoke(transform, frame);
}
public class LectorRectTarnsform: Lector
{
    RectTransform rectTransform;
    protected override void Animar(float frame) => animar.Invoke(rectTransform, frame);
}

public class Lector : MonoBehaviour
{
    public Lector Setup(System.Action<object, float> animar, float temps, Transicio transicio)
    {
        this.animar = animar;
        this.temps = temps;
        SetTransicio(transicio);

        return this;
    }
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



    protected System.Action<object, float> animar = null;

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
