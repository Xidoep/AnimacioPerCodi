using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

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
    [SerializeField, Range(0, 1)] float volum = 1;



    public override void Transformar(Component objectiu, float frame)
    {
        if (frame > 0)
            return;

        AudioSource.PlayClipAtPoint(audio, objectiu.transform.position, volum);

        if (delay < 0) AudioSource.PlayClipAtPoint(audio, objectiu.transform.position, volum);
        else XS_Coroutine.StartCoroutine_Ending_FrameDependant<Component>(delay, Play, objectiu);
    }

    void Play(Component objectiu) => AudioSource.PlayClipAtPoint(audio, objectiu.transform.position, volum);
}
