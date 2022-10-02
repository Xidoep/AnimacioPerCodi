using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Scriptable", fileName = "Scriptable")]
public class Animacio_Scriptable : ScriptableObject
{
    [SerializeField] Transicio transicio;
    [SerializeField] float temps;

    [SerializeField] [SerializeReference] List<Animacio> animacions;

    //INTERN
    Lector lector;

    public List<Animacio> Animacions => animacions;

    public void Play(Transform transform)
    {
        for (int i = 0; i < animacions.Count; i++)
        {
            animacions[i].Play(transform, temps, transicio);
        }
    }
    public Lector Play_GetLector(Transform transform)
    {
        lector = animacions[0].Play_GetLector(transform, temps, transicio);
        for (int i = 1; i < animacions.Count; i++)
        {
            animacions[i].Play(transform, temps, transicio);
        }
        return lector;
    }
    public void Continue(Transform transform) 
    {
        transform.GetComponent<Lector>().Continue();
    } 
    public void Continue() => lector.Continue();
    public void Stop(Transform transform) => transform.GetComponent<Lector>().Stop(false);
    public void Stop() => lector.Stop(false);
    public void Stop_OnAnimationEnding(Transform transform) => transform.GetComponent<Lector>().Stop(true);
    public void Stop_OnAnimationEnding() => lector.Stop(true);
}






