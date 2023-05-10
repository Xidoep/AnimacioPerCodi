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
    [SerializeField] float delay = 0;

    [SerializeField] [SerializeReference] Animacio[] animacions;
    Component component;
    Lector lector;
    public Lector Lector(Component component)
    {
        if(this.component == component)
        {
            if (lector != null)
                return lector;

            if (component.GetComponent<Lector>())
                return lector = component.GetComponent<Lector>();
            else
                return lector = component.gameObject.AddComponent<Lector>();
        }
        else
        {
            this.component = component;
            if (component.GetComponent<Lector>())
                return lector = component.GetComponent<Lector>();
            else
                return lector = component.gameObject.AddComponent<Lector>();
        }

        /*if (lector != null)
            return lector;
        else 
        {
            if (component.gameObject.GetComponent<Lector>())
                return lector = component.gameObject.GetComponent<Lector>();
            else return lector = component.gameObject.AddComponent<Lector>();
            
            //return lector = component.gameObject.AddComponent<Lector>();
        }*/
    }

    private void OnEnable()
    {
        lector = null;
    }

    public float Temps => temps;
    public Transicio Transicio => transicio;
    public Animacio[] Animacions { get => animacions; set => animacions = value; }
    public bool TeAnimacions => animacions != null && animacions.Length > 0;




    /// <summary>
    /// Util per ser cridat desde un Unityevent
    /// </summary>
    /// <param name="component"></param>
    public void Play(Component component) => component.SetupAndPlay(Lector(component), animacions, temps, delay, transicio);

    /// <summary>
    /// Pensat per ser cridat desde codi.
    /// Si el Lector es Null en crea un de noi i el retorna.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="lector"></param>
    /// <returns></returns>
    public Lector Play(Component component, Lector lector) => component.SetupAndPlay(lector ? lector : Lector(component), animacions, temps, delay, transicio);

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






