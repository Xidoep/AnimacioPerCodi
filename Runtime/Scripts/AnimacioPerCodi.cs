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




    public void Play(Component component) => component.SetupAndPlay(ref lector, animacions, temps, transicio);

    public void Continue(Transform transform) => transform.GetComponent<Lector>().Continue();
    public void Continue() => lector.Continue();



    public void Stop(Transform transform) => transform.GetComponent<Lector>().Stop(false);
    public void Stop() => lector.Stop(false);
    public void Stop_OnAnimationEnding(Transform transform) => transform.GetComponent<Lector>().Stop(true);
    public void Stop_OnAnimationEnding() => lector.Stop(true);




    [System.Serializable]
    public class Interaccio
    {
        [SerializeField] float temps = 1;

        [SerializeReference] List<Animacio> animacions;

        public float Temps => temps;
        public List<Animacio> Animacions => animacions;
        public bool TeAnimacions => animacions != null && animacions.Count > 0;

        public void Play(Component component, Transicio transicio, ref Lector lector) => component.SetupAndPlay(ref lector, animacions, temps, transicio);

    }
}






