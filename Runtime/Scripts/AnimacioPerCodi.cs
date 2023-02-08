using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Animacio", fileName = "Animacio")]
public class AnimacioPerCodi : ScriptableObject
{
    [SerializeField] Transicio transicio;
    [SerializeField] float temps = 1;

    [SerializeField] [SerializeReference] List<Animacio> animacions;

    Lector lector;

    public List<Animacio> Animacions => animacions;

    public void Play(Component component)
    {
        component.SetupAndPlay(ref lector, animacions, temps, transicio);
    }

    public void Continue(Transform transform) => transform.GetComponent<Lector>().Continue();
    public void Continue() => lector.Continue();



    public void Stop(Transform transform) => transform.GetComponent<Lector>().Stop(false);
    public void Stop() => lector.Stop(false);
    public void Stop_OnAnimationEnding(Transform transform) => transform.GetComponent<Lector>().Stop(true);
    public void Stop_OnAnimationEnding() => lector.Stop(true);
}






