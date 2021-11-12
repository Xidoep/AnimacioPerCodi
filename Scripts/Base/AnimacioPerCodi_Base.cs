using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimacioPerCodi_Base : MonoBehaviour
{
    public enum Transicio_Tipus { clamp, loop, pingpong, loopPingpong, invertit }
    [SerializeField] Transicio_Tipus transicio;
    [Tooltip("Temps de l'animacio")]
    [SerializeField] float temps = 1;
    internal abstract Transformacions[] GetTransformacions { get; }

    bool pingPong;
    bool invertit;
    float time;
    float TempsDesdePlay { get => (Time.unscaledTime - time) / temps; set => time = value; }
    bool finalitzarAlFinalAnimacio;
    bool finalitzat;
    bool looping;

    internal int index;
    internal bool all;

    System.Action<float> transformacio;

    void Transformar(float temps)
    {
        transformacio.Invoke(temps);
         //for (int i = 0; i < GetTransformacions.Length; i++) GetTransformacions[i].Transformar(transform, temps);
    }

    void TransformarAll(float temps) { for (int i = 0; i < GetTransformacions.Length; i++) GetTransformacions[i].Transformar(transform, temps); }
    void TransformarOne(float temps) => GetTransformacions[index].Transformar(transform, temps);

    internal float GetTemps() => temps;
    public void Play()
    {
        if (!gameObject.activeSelf)
            return;

        all = true;
        index = 0;

        StartCoroutine(PlayCorrutina());
    }
    public void Play(int animacio)
    {
        if (!gameObject.activeSelf)
            return;

        all = false;
        index = animacio;

        StartCoroutine(PlayCorrutina());
    }
    public void Play(int animacio, float temps)
    {
        if (!gameObject.activeSelf)
            return;

        all = false;
        this.temps = temps;
        index = animacio;

        StartCoroutine(PlayCorrutina());
    }

    public IEnumerator PlayCorrutina()
    {
        if(all)
            transformacio = TransformarAll;
        else transformacio = TransformarOne;

        switch (transicio)
        {
            case Transicio_Tipus.clamp:
                pingPong = false;
                looping = false;
                invertit = false;
                break;
            case Transicio_Tipus.pingpong:
                pingPong = true;
                looping = false;
                invertit = false;
                break;
            case Transicio_Tipus.loop:
                pingPong = false;
                looping = true;
                invertit = false;
                break;
            case Transicio_Tipus.loopPingpong:
                pingPong = true;
                looping = true;
                invertit = false;
                break;
            case Transicio_Tipus.invertit:
                pingPong = false;
                looping = false;
                invertit = true;
                break;
        }
        time = Time.unscaledTime;
        return ActualitzarCorrutina();
    }


    public IEnumerator ActualitzarCorrutina()
    {
        finalitzat = true;
        while (finalitzat)
        {
            finalitzat = Actualitzar();
            yield return null;
        }
        //Destroy(this);
        yield return null;
    }
    public bool Actualitzar()
    {
        if (!pingPong)
        {
            if (!invertit)
                Transformar(Mathf.Clamp01(TempsDesdePlay));
            else Transformar(1 - Mathf.Clamp01(TempsDesdePlay));
        }
        else
        {
            if(TempsDesdePlay < 0.5f)
                Transformar(Mathf.Clamp01(TempsDesdePlay) * 2);
            else Transformar(2 - (Mathf.Clamp01(TempsDesdePlay) * 2));
        }

        if (TempsDesdePlay >= 1)
        {
            if (finalitzarAlFinalAnimacio)
            {
                finalitzarAlFinalAnimacio = false;
                finalitzat = true;
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




    public void StopInmediatament() => Stop(false);
    public void StopAlFinal() => Stop(true);

    void Stop(bool esperarFinalAnimacio = false)
    {
        if (esperarFinalAnimacio)
            finalitzarAlFinalAnimacio = true;
        else finalitzat = false;
    }



    [System.Serializable]
    public abstract class Transformacions
    {
        public abstract void Transformar(Transform transform, float temps);
    }
}

