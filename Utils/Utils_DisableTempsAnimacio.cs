using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class Utils_DisableTempsAnimacio : AnimacioPerCodi_Base
{
    //[SerializeField] float tempsDisable;
    //[SerializeField] [Tooltip("Animacio que fara mentre espera a Disoldres")] AnimacioPerCodi.Animacio animacio;
    Countdown countdownDisable;
    [SerializeField] Animacio_Multiple[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    private void OnEnable()
    {
        countdownDisable = new Countdown(GetTemps() + 0.01f, HideGameObject);

        if (!enEnable)
            return;

        if (GetTemps() == 0)
            return;

        Disable(GetTemps() + 0.01f);
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
        Play(0, temps);
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
