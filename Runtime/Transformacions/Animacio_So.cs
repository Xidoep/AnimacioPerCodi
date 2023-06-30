using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_So : Animacio
{
    string nom = "So";
    public Animacio_So() { }
    public Animacio_So(So so, bool aPosicio = true, float delay = 0)
    {
        this.so = so;
        this.aPosicio = aPosicio;
        this.delay = delay;
    }

    [BoxGroup("So"), SerializeField, InlineEditor, LabelText("Scriptable")] So so;

    [Tooltip("It just have a range to limit it's amount. It's not related to the animation time")]
    [BoxGroup("So"), PropertyOrder(1), SerializeField, HorizontalGroup("So/o"), LabelWidth(60)] 
    bool aPosicio;

    [BoxGroup("So"), PropertyOrder(2), SerializeField, HorizontalGroup("So/o"), LabelWidth(40), Range(0, 1.5f), ShowIf("@this.delay > 0")] 
    float delay = 0;


    [BoxGroup("So"), PropertyOrder(2), HorizontalGroup("So/o", width: 70), Button, HideIf("@this.delay > 0")] void Delayed() => delay = 1;   
    
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
