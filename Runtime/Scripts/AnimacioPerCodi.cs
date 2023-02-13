using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Animacio", fileName = "Animacio")]
public class AnimacioPerCodi : ScriptableObject
{
    [SerializeField] Transicio transicio;
    [SerializeField] float temps = 1;

    [SerializeField] [SerializeReference] Animacio[] animacions;
    Lector lector;
    public Lector Lector(Component component)
    {
        if (lector != null)
            return lector;
        else 
        {
            if (component.gameObject.GetComponent<Lector>())
                return lector = component.gameObject.GetComponent<Lector>();
            else return lector = component.gameObject.AddComponent<Lector>();
            
            //return lector = component.gameObject.AddComponent<Lector>();
        }
    }

    public float Temps => temps;
    public Animacio[] Animacions => animacions;
    public bool TeAnimacions => animacions != null && animacions.Length > 0;




    public void Play(Component component) => component.SetupAndPlay(Lector(component), animacions, temps, transicio);

    public void Continue(Transform transform) => transform.GetComponent<Lector>().Continue();
    public void Continue() => lector.Continue();


    public void Stop(Component component) => component.GetComponent<Lector>().Stop(false);
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

        //public void Play(Component component, Transicio transicio, ref Lector lector) => component.SetupAndPlay(ref lector, animacions, temps, transicio);
    }
}






