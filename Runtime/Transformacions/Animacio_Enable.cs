using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacio_Enable : Animacio
{
    public enum Moment { Inici, Final}
    public enum Accio { Enable, Disable}

    [SerializeField] string nom = "Activar BotoFisic";

    public Animacio_Enable() { }
    public Animacio_Enable(Moment moment, Accio accio) 
    {
        this.moment = moment;
        this.accio = accio;
    }

    [SerializeField] Moment moment;
    [SerializeField] Accio accio;

    public override void Transformar(Component component, float frame)
    {
        if(frame == (moment == Moment.Inici ? 0 : 1))
            ((Behaviour)component).enabled = accio == Accio.Enable;
    }

}
