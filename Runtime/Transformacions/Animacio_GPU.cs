using UnityEngine;
using XS_Utils;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_GPU : Animacio
{
    public Animacio_GPU() { }
    public Animacio_GPU(Accio accio)
    {
        this.accio = accio;
    }

    public enum Accio { add, remove}

    [Title("GPU", horizontalLine: false), SerializeField, HideLabel] Accio accio;

    public override void Transformar(Component objectiu, float frame)
    {
        switch (accio)
        {
            case Accio.add:
                if (frame == 1)
                {
                    ((Transform)objectiu).gameObject.AddGrafics();
                }
                break;
            case Accio.remove:
                if (frame == 0)
                {
                    ((Transform)objectiu).gameObject.RemoveGrafic();
                }
                break;
        }
    }
}
