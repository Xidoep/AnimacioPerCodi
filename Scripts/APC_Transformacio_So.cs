using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Transformacions/So", fileName = "[So]")]
public class APC_Transformacio_So : APC_Transformacio
{
    public override APC_Transformacio CreateInstance(APC_Transformacio transformacio)
    {
        var _so = ScriptableObject.CreateInstance<APC_Transformacio_So>();
        _so.play = play;
        _so.so = so;
        return _so;
    }

    [Range(0, 1)] public float play = 0.5f;
    bool played = false;

    [ContextMenuItem("Play","Play")]
    public So so;

    void Play()
    {
        GameObject _tmp = new GameObject();
        AudioSource _ass = so.Play_Referencia(_tmp.transform);
        DestroyImmediate(_tmp);
    }

    public override void Transformacio(Transform transform, float temps)
    {
        if(temps > play && !played)
        {
            played = true;
            so.Play_Referencia(transform);
        }
    }

    public override void Reset()
    {
        played = false;
    }
}
