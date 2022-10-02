using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class Utils_DisableTempsAnimacio : AnimacioPerCodi_Base
{
    //[SerializeField] float tempsDisable;
    //[SerializeField] [Tooltip("Animacio que fara mentre espera a Disoldres")] AnimacioPerCodi.Animacio animacio;
    [SerializeField] Transformacions[] transformacions;
    internal override Transformacions[] GetTransformacions { get => GetInstancedTransformacions(transformacions); set => transformacions = value; }
    XS_Countdown countdownDisable;

    private void OnEnable()
    {
        countdownDisable = new XS_Countdown(GetTemps() + 0.01f, HideGameObject);
    }

    private void Update()
    {
        countdownDisable.Update();
    }

    public void Disable() 
    {
        countdownDisable.Start();
        Play();
    }
    public void Disable(float temps) 
    {
        countdownDisable.Start(temps);
        Play(temps);
    }

    /*public override void Transformar(float temps) 
    {
        if (GetTransformacions.Length == 0)
            return;

        GetTransformacions[0].Transformar(transform, temps);
    } */

    void HideGameObject() => gameObject.SetActive(false);

    private void OnDisable() => countdownDisable.Stop();
}
