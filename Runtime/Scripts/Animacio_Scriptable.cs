using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Animacio", fileName = "Animacio")]
public class Animacio_Scriptable : ScriptableObject
{
    [SerializeField] Transicio transicio;
    [SerializeField] float temps = 1;

    [SerializeField] [SerializeReference] List<Animacio> animacions;

    //INTERN
    Lector lector;

    public List<Animacio> Animacions => animacions;

    public void Play(Component component)
    {
        for (int i = 0; i < animacions.Count; i++)
        {
            lector = component.gameObject.GetComponent<LectorComponent>();
            if (!lector) lector = component.gameObject.AddComponent<LectorComponent>();

            if (i == 0) lector.Setup(animacions[i].Transformar, component, temps, transicio);
            else lector.Add(animacions[i].Transformar);
        }
        lector.Play();
    }
    /*public void Play(GameObject gameObject) 
    { 
        for (int i = 0; i < animacions.Count; i++)
        {
            lector = gameObject.GetComponent<Lector>();
            if (!lector) lector = gameObject.AddComponent<Lector>();

            if (i == 0) lector.Setup(animacions[i].Transformar, temps, transicio);
            else lector.Add(animacions[i].Transformar);
        }
        lector.Play();
    }
    public void Play(Transform transform) 
    {
        for (int i = 0; i < animacions.Count; i++)
        {
            lector = transform.gameObject.GetComponent<Lector>();
            if (!lector) lector = transform.gameObject.AddComponent<Lector>();

            if (i == 0) lector.Setup(animacions[i].Transformar, temps, transicio);
            else lector.Add(animacions[i].Transformar);
        }
        lector.Play();
    }
    public void Play(Image image) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(image, temps, transicio); } }
    public void Play(Text text) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(text, temps, transicio); } }
    public void Play(SpriteRenderer spriteRenderer) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(spriteRenderer, temps, transicio); } }
    public void Play(TMP_Text text) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(text, temps, transicio); } }
    public void Play(Toggle toggle) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(toggle, temps, transicio); } }
    public void Play(MeshRenderer meshRenderer) 
    {
        for (int i = 0; i < animacions.Count; i++)
        {
            lector = meshRenderer.gameObject.GetComponent<LectorMeshRenderer>();
            if (!lector) lector = meshRenderer.gameObject.AddComponent<LectorMeshRenderer>();

            if (i == 0) lector.Setup(animacions[i].Transformar, temps, transicio);
            else lector.Add(animacions[i].Transformar);
        }
        lector.Play();
    }
    public void Play(RectTransform rectTransform)
    {
        for (int i = 0; i < animacions.Count; i++)
        {
            lector = rectTransform.gameObject.GetComponent<LectorRectTransform>();
            if (!lector) lector = rectTransform.gameObject.AddComponent<LectorRectTransform>();

            if (i == 0) lector.Setup(animacions[i].Transformar, temps, transicio);
            else lector.Add(animacions[i].Transformar);
        }
        lector.Play();
    }
    */

    public void Continue(Transform transform) => transform.GetComponent<Lector>().Continue();
    public void Continue() => lector.Continue();



    public void Stop(Transform transform) => transform.GetComponent<Lector>().Stop(false);
    public void Stop() => lector.Stop(false);
    public void Stop_OnAnimationEnding(Transform transform) => transform.GetComponent<Lector>().Stop(true);
    public void Stop_OnAnimationEnding() => lector.Stop(true);
}






