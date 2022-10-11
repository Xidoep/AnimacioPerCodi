using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] So so;
    [Space(10)]
    [Tooltip("It just have a range to limit it's amount. It's not related to the animation time")]
    [SerializeField] bool aPosicio;
    [SerializeField] [Range(0, 1.5f)] float delay = 0;
 
    
    
    public override void Transformar(object objectiu, float frame)
    {
        if (frame > 0)
            return;

        if (!aPosicio)
        {
            if (delay < 0) so.Play();
            else so.Play(delay);
        }
        else
        {
            if (delay < 0) so.Play(((Transform)objectiu));
            else so.Play(((Transform)objectiu), delay);
        }
    }

}
