using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioBoto", fileName = "AnimacioBoto")]

public class AnimacioPerCodi_Boto : ScriptableObject
{

    [SerializeField] Interaccio onClick;
    [SerializeField] Interaccio onEnter;
    [SerializeField] Interaccio onExit;
    [SerializeField] Interaccio loop;

    public Interaccio OnClick => onClick;
    public Interaccio OnEnter => onEnter;
    public Interaccio OnExit => onExit;
    public Interaccio Loop => loop;

    Lector lector;

    [System.Serializable]
    public struct Interaccio
    {
        [SerializeField] float temps;
        [SerializeField] [SerializeReference] List<Animacio> animacions;
        [SerializeField] Component component;
        public List<Animacio> Animacions => animacions;

        public void Play(Transicio transicio, ref Lector lector) => component.SetupAndPlay(ref lector, animacions, temps, transicio);
    }

    public void PlayOnClick() => onClick.Play(Transicio.clamp, ref lector);
    public void PlayOnEnter() => onEnter.Play(Transicio.clamp, ref lector);
    public void PlayOnExit() => onExit.Play(Transicio.clamp, ref lector);
}
