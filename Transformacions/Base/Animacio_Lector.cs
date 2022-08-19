using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Animacio_Lector : MonoBehaviour
{
    protected void Setup(float temps, Animacio.Transicio transicio)
    {
        this.temps = temps;
        SetTransmicio(transicio);
    }

    protected float temps;
    bool pingPong;
    bool looping;
    bool invertit;

    //INTERN
    float time;
    bool finalitzat;
    bool finalitzarAlFinalAnimacio;

    float TempsDesdePlay => Mathf.Clamp01((Time.unscaledTime - time) / temps);

    protected abstract void Animar(float frame);
    





    public void Play()
    {
        time = Time.unscaledTime;
        StartCoroutine(ActualitzarCorrutina());
    }
    public IEnumerator ActualitzarCorrutina()
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

    public void Stop(bool esperarFinalAnimacio)
    {
        if (esperarFinalAnimacio)
            finalitzarAlFinalAnimacio = true;
        else finalitzat = false;
    }

















    protected void SetTransmicio(Animacio.Transicio transicio)
    {
        switch (transicio)
        {
            case Animacio.Transicio.clamp:
                pingPong = false;
                looping = false;
                invertit = false;
                break;
            case Animacio.Transicio.pingpong:
                pingPong = true;
                looping = false;
                invertit = false;
                break;
            case Animacio.Transicio.loop:
                pingPong = false;
                looping = true;
                invertit = false;
                break;
            case Animacio.Transicio.loopPingpong:
                pingPong = true;
                looping = true;
                invertit = false;
                break;
            case Animacio.Transicio.invertit:
                pingPong = false;
                looping = false;
                invertit = true;
                break;
        }
    }
}
