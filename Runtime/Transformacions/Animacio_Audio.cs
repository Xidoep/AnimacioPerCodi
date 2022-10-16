using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animacio_Audio : Animacio
{
    [SerializeField] string nom = "Audio";
    public Animacio_Audio() { }
    public Animacio_Audio(AudioClip audio, float delay = 0)
    {
        this.audio = audio;
        this.delay = delay;
    }

    [SerializeField] AudioClip audio;

    [Tooltip("It just have a range to limit it's amount. It's not related to the animation time")]
    [SerializeField] [Range(0,1.5f)] float delay = 0;



    public override void Transformar(object objectiu, float frame)
    {
        if (frame > 0)
            return;

        ((AudioSource)objectiu).clip = audio;

        if (delay < 0) ((AudioSource)objectiu).Play();
        else ((AudioSource)objectiu).PlayDelayed(delay);
    }


}
