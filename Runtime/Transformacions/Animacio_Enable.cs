using UnityEngine;
using Sirenix.OdinInspector;

public class Animacio_Enable : Animacio
{
    public enum Moment { Inici, Final}
    public enum Accio { Enable, Disable}

    public Animacio_Enable() { }
    public Animacio_Enable(Moment moment, Accio accio) 
    {
        this.moment = moment;
        this.accio = accio;
    }

    [SerializeField, HorizontalGroup("1", Title = "Blend Shape"), LabelWidth(45), HideLabel] Moment moment;
    [SerializeField, HorizontalGroup("1", Title = "Blend Shape"), HideLabel] Accio accio;

    public override void Transformar(Component component, float frame)
    {
        if(frame == (moment == Moment.Inici ? 0 : 1))
            ((Behaviour)component).enabled = accio == Accio.Enable;
    }

}
