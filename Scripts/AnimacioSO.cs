using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

//[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Animacio", fileName = "Animacio")]
[System.Serializable]public class AnimacioSO
{
    /*public static AnimacioSO CreateInstance(AnimacioSO animacio, Transform transform)
    {
        var _animacio = ScriptableObject.CreateInstance<AnimacioSO>();

        _animacio.name = animacio.name;
        _animacio.transicio = animacio.transicio;
        _animacio.temps = animacio.temps;
        //_animacio.esdeveniment = animacio.esdeveniment;
        //_animacio.esdevenimentTemps = animacio.esdevenimentTemps;
        _animacio.transform = transform;
        _animacio.transformacions = animacio.transformacions;

        return _animacio;
    }*/

    public enum Transformacio_Tipus
    {
        nul, posicio, rotacio, escala, color, degradat, sprite
    }
    public enum Transicio_Tipus
    {
        clamp, loop, pingpong, loopPingpong, invertit
    }



    public Transicio_Tipus transicio;
    public float temps = 1;
    //[Range(0, 1)] public float esdevenimentTemps = 1;

    public APC_Transformacio[] transformacions;
    List<APC_Transformacio> transformacionsActuals;
    //[Range(0, 1)] public float tempsEsdeveniment = 0.8f;
    //bool llançat = false;



    //UnityEvent esdeveniment;
    Transform transform;
    float time;
    float TempsDesdePlay
    {
        get
        {
            return (Time.unscaledTime - time) / temps;
        }
        set
        {
            time = value;
        }
    }
    bool looping;
    bool pingPong;
    bool invertit;

    bool finalitzarAlFinalAnimacio;
    bool finalitzat;

    public IEnumerator Play(Transform _transform)
    {
        //AnimacioSO _animacio = CreateInstance(this, _transform);
        transform = _transform;
        Play();
        return ActualitzarCorrutina();
    }

    public IEnumerator Play(Transform _transform, APC_Transformacio_Esdeveniment.Esdeveniment esdeveniment)
    {
        //AnimacioSO _animacio = CreateInstance(this, _transform);
        transform = _transform;
        Play(esdeveniment);
        return ActualitzarCorrutina();
    }

    public IEnumerator Play(Transform _transform, ref AnimacioSO contenidor)
    {
        //contenidor = CreateInstance(this, _transform);
        transform = _transform;
        Play();
        return ActualitzarCorrutina();
    }

    public IEnumerator Play(Transform _transform, ref AnimacioSO contenidor, APC_Transformacio_Esdeveniment.Esdeveniment esdeveniment)
    {
        //contenidor = CreateInstance(this, _transform);
        transform = _transform;
        Play(esdeveniment);
        return ActualitzarCorrutina();
    }


    void Play(APC_Transformacio_Esdeveniment.Esdeveniment esdeveniment = null)
    {
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

        transformacionsActuals = new List<APC_Transformacio>();
        for (int i = 0; i < transformacions.Length; i++)
        {
            transformacionsActuals.Add(transformacions[i].CreateInstance(transformacions[i]));
            if(esdeveniment != null)
            {
                if (transformacionsActuals[i].GetType() == typeof(APC_Transformacio_Esdeveniment))
                {
                    ((APC_Transformacio_Esdeveniment)transformacionsActuals[i]).AfegirEsdeveniment(esdeveniment);
                }
            }
        }
    }

    

    public bool Actualitzar()
    {
        if (!pingPong)
        {
            if (!invertit)
            {
                Clamp(TempsDesdePlay, transform);
            }
            else
            {
                ClampInvertit(TempsDesdePlay, transform);
            }
        }
        else
        {
            PingPong(TempsDesdePlay, transform);
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
                    for (int i = 0; i < transformacionsActuals.Count; i++)
                    {
                        transformacionsActuals[i].Reset();
                    }
                }
            }
        }

        return TempsDesdePlay < 1;
    }
    public void Stop(bool esperarFinalAnimacio = false)
    {
        if (esperarFinalAnimacio)
        {
            finalitzarAlFinalAnimacio = true;
        }
        else
        {
            finalitzat = false;
        }
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

    void Clamp(float _t, Transform transform)
    {
        for (int i = 0; i < transformacionsActuals.Count; i++)
        {
            transformacionsActuals[i].Transformacio(transform, Mathf.Clamp01(_t));
        }
    }
    void PingPong(float _t, Transform transform)
    {
        if (_t < 0.5f)
        {
            for (int i = 0; i < transformacionsActuals.Count; i++)
            {
                transformacionsActuals[i].Transformacio(transform, Mathf.Clamp01(_t) * 2);
            }
        }
        if(_t > 0.5f)
        {
            for (int i = 0; i < transformacionsActuals.Count; i++)
            {
                transformacionsActuals[i].Transformacio(transform, 2 - (Mathf.Clamp01(_t) * 2));
            }
        }
    }
    void ClampInvertit(float _t, Transform transform)
    {
        for (int i = 0; i < transformacionsActuals.Count; i++)
        {
            transformacionsActuals[i].Transformacio(transform, 1 - Mathf.Clamp01(_t));
        }
    }


}
