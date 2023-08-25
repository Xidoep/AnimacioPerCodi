using UnityEngine;
using XS_Utils;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_Audio : Animacio
{
    public Animacio_Audio() { }
    public Animacio_Audio(AudioClip[] audio, float delay = 0)
    {
        this.audio = audio;
        this.delay = delay;
    }

    [SerializeField, HideLabel] AudioClip[] audio;
    [HorizontalGroup("1"), SerializeField, LabelWidth(40), MinMaxSlider(0,1,true)] Vector2 volum;
    [HorizontalGroup("1", width: 100), SerializeField, LabelWidth(40), Range(0, 1.5f), ShowIf("@this.delay > 0")] float delay = 0;
    [HorizontalGroup("1", width: 100), HideIf("@this.delay > 0"), Button] void Delayed() => delay = 1;

    public override void Transformar(Component objectiu, float frame)
    {
        if (frame > 0)
            return;


        //AudioSource.PlayClipAtPoint(audio[Random.Range(0,audio.Length)], objectiu.transform.position, volum);
        if (delay < 0) AudioSource.PlayClipAtPoint(audio[Random.Range(0, audio.Length)], objectiu.transform.position, Random.Range(volum.x, volum.y));
        else XS_Coroutine.StartCoroutine_Ending_FrameDependant(delay, Play, objectiu);
    }

    void Play(Component objectiu) => AudioSource.PlayClipAtPoint(audio[Random.Range(0, audio.Length)], objectiu.transform.position, Random.Range(volum.x, volum.y));
}
