using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Animacio_Esdeveniment;

[System.Serializable]
public class Animacio_Esdeveniment : Animacio
{
    [SerializeField] string nom = "Esdeveniment";
    public Animacio_Esdeveniment() { }
    public Animacio_Esdeveniment(UnityEvent esdeveniment, Moment moment = Moment.final)
    {
        this.esdeveniment = esdeveniment;
        this.moment = moment;
    }
    public Animacio_Esdeveniment(Action action, Moment moment = Moment.final)
    {
        this.action = action;
        this.moment = moment;
    }

    public enum Moment { inici, final}

    [SerializeField] UnityEvent esdeveniment;
    Action action;
    [SerializeField] Moment moment;



    public override void Transformar(Component objectiu, float frame)
    {
        switch (moment)
        {
            case Moment.inici:
                if (frame == 0)
                {
                    esdeveniment?.Invoke();
                    action?.Invoke();
                }
                break;
            case Moment.final:
                if (frame == 1)
                {
                    esdeveniment?.Invoke();
                    action?.Invoke();
                }
                break;
        }
    }
}
