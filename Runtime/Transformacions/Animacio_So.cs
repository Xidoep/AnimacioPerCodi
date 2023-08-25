using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_So : Animacio
{
    public Animacio_So() { }
    public Animacio_So(So so, bool aPosicio = true, float delay = 0)
    {
        this.so = so;
        this.aPosicio = aPosicio;
        this.delay = delay;
    }

    [SerializeField, SerializeScriptableObject, HideLabel] So so;

    [HorizontalGroup("1"), SerializeField, LabelWidth(40), ToggleLeft] bool aPosicio;
    [HorizontalGroup("1"), SerializeField, LabelWidth(40), Range(0, 1.5f), ShowIf("@this.delay > 0")]  float delay = 0;
    [HorizontalGroup("1", width: 70), Button, HideIf("@this.delay > 0")] void Delayed() => delay = 1;   
    
    public override void Transformar(Component objectiu, float frame)
    {
        if (frame > 0)
            return;

        //Debug.LogError("Play!");

        if (!aPosicio)
        {
            if (delay <= 0) so.Play();
            else so.PlayDelayed(delay);
        }
        else
        {
            if (delay <= 0) so.Play(objectiu.transform);
            else so.PlayDelayed(objectiu.transform, delay);
        }
    }
}
