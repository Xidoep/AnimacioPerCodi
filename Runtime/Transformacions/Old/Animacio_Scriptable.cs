using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Animacio", fileName = "Animacio")]
public class Animacio_Scriptable : ScriptableObject
{
    [SerializeField] Transicio transicio;
    [SerializeField] float temps;

    [SerializeField] [SerializeReference] List<Animacio> animacions;

    //INTERN
    Lector lector;

    public List<Animacio> Animacions => animacions;

    public void Play(GameObject gameObject) { for (int i = 0; i < animacions.Count; i++){ animacions[i].Play(gameObject, temps, transicio); } }
    public void Play(Image image) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(image.gameObject, temps, transicio); } }
    public void Play(Text text) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(text.gameObject, temps, transicio); } }
    public void Play(SpriteRenderer spriteRenderer) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(spriteRenderer.gameObject, temps, transicio); } }
    public void Play(TMP_Text text) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(text.gameObject, temps, transicio); } }
    public void Play(Toggle toggle) { for (int i = 0; i < animacions.Count; i++) { animacions[i].Play(toggle.gameObject, temps, transicio); } }



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






